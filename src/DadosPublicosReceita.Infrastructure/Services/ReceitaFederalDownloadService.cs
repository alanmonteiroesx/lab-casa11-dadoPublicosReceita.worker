using DadosPublicosReceita.Domain.Enums;
using DadosPublicosReceita.Domain.Interfaces.Services;
using DadosPublicosReceita.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace DadosPublicosReceita.Infrastructure.Services
{
    public class ReceitaFederalDownloadService : IReceitaFederalDownloadService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ReceitaFederalDownloadService> _logger;
        private readonly AppDbContext _contexto;

        private const string BASE_URL = "https://arquivos.receitafederal.gov.br/dados/cnpj/dados_abertos_cnpj/";
        private const string PADRAO_PASTA = @"\d{4}-\d{2}/";
        private const string FORMATO_VERSAO = "yyyyMMddHHmmss";
        private const string NOME_CLIENTE_HTTP = "ReceitaFederal";
        private const string MENSAGEM_ERRO_VERIFICACAO = "Erro ao verificar novos dados";
        private const string MENSAGEM_ERRO_DOWNLOAD = "Erro ao baixar arquivo {0}";
        private const string MENSAGEM_PROGRESSO_DOWNLOAD = "Download {Arquivo}: {Progresso}% - Baixado: {Baixado:N2}MB de {Total:N2}MB - Velocidade: {Velocidade:N2}MB/s";
        private const string MENSAGEM_ARQUIVOS_PENDENTES = "Arquivos pendentes para a pasta {Pasta}: {Arquivos}";
        private const string MENSAGEM_ARQUIVOS_INTERROMPIDOS = "Arquivos interrompidos que serão reprocessados: {Arquivos}";
        private const int BUFFER_SIZE = 32768;

        private string _pastaAtual = string.Empty;

        public ReceitaFederalDownloadService(
            IHttpClientFactory httpClientFactory,
            AppDbContext contexto,
            ILogger<ReceitaFederalDownloadService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _contexto = contexto;
            _logger = logger;
        }

        public async Task<bool> ExisteNovosDadosAsync(CancellationToken cancellationToken)
        {
            try
            {
                var client = _httpClientFactory.CreateClient(NOME_CLIENTE_HTTP);
                var response = await client.GetAsync(BASE_URL, cancellationToken);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                var matches = Regex.Matches(content, PADRAO_PASTA);

                if (!matches.Any())
                    return false;

                var folders = matches
                    .Select(m => m.Value.TrimEnd('/'))
                    .OrderByDescending(x => x)
                    .ToList();

                var ultimaImportacaoCompleta = await _contexto.ImportacoesControle
                    .Where(x => x.Status == EStatusImportacaoType.Concluido)
                    .GroupBy(x => x.AnoMes)
                    .Select(g => new {
                        AnoMes = g.Key,
                        Total = g.Count()
                    })
                    .OrderByDescending(x => x.AnoMes)
                    .FirstOrDefaultAsync(cancellationToken);

                _pastaAtual = folders.First();

                if (ultimaImportacaoCompleta == null || ultimaImportacaoCompleta.AnoMes != _pastaAtual)
                    return true;

                var arquivosDisponiveis = await ObterArquivosDisponiveisAsync(cancellationToken);
                return arquivosDisponiveis.Any();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MENSAGEM_ERRO_VERIFICACAO);
                throw;
            }
        }

        public async Task<(Stream Stream, string Pasta, string Versao)> DownloadArquivoAsync(
            string nomeArquivo,
            CancellationToken cancellationToken)
        {
            try
            {
                var client = _httpClientFactory.CreateClient(NOME_CLIENTE_HTTP);
                var url = $"{BASE_URL}/{_pastaAtual}/{nomeArquivo}";
                var memoryStream = new MemoryStream();

                using var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
                response.EnsureSuccessStatusCode();

                var version = response.Headers.ETag?.Tag ?? DateTime.Now.ToString(FORMATO_VERSAO);
                using var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken);

                var buffer = new byte[BUFFER_SIZE];
                int bytesRead;
                var totalBytesRead = 0L;
                var contentLength = response.Content.Headers.ContentLength ?? -1L;
                var ultimoLog = DateTime.MinValue;

                while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
                {
                    await memoryStream.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken);
                    totalBytesRead += bytesRead;

                    if (contentLength > 0 && DateTime.Now.Subtract(ultimoLog).TotalSeconds >= 5)
                    {
                        LogarProgresso(nomeArquivo, totalBytesRead, contentLength, ultimoLog);
                        ultimoLog = DateTime.Now;
                    }
                }

                memoryStream.Position = 0;
                return (memoryStream, _pastaAtual, version);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format(MENSAGEM_ERRO_DOWNLOAD, nomeArquivo));
                throw;
            }
        }

        private void LogarProgresso(string arquivo, long bytesLidos, long total, DateTime ultimoLog)
        {
            var progresso = (int)((bytesLidos * 100) / total);
            var velocidade = bytesLidos / (1024 * 1024 * DateTime.Now.Subtract(ultimoLog).TotalSeconds);

            _logger.LogInformation(
                MENSAGEM_PROGRESSO_DOWNLOAD,
                arquivo,
                progresso,
                bytesLidos / (1024 * 1024),
                total / (1024 * 1024),
                velocidade);
        }

        public async Task<IEnumerable<string>> ObterArquivosDisponiveisAsync(CancellationToken cancellationToken)
        {
            var arquivosBase = new List<string>
            {
                "Cnaes.zip",
                "Municipios.zip",
                "Paises.zip"
            };

            for (int i = 0; i < 10; i++)
            {
                arquivosBase.Add($"Empresas{i}.zip");
            }

            for (int i = 0; i < 10; i++)
            {
                arquivosBase.Add($"Estabelecimentos{i}.zip");
            }

            if (string.IsNullOrEmpty(_pastaAtual))
                return arquivosBase;

            var arquivosProcessados = await _contexto.ImportacoesControle
                .Where(x => x.AnoMes == _pastaAtual && x.Status == EStatusImportacaoType.Concluido)
                .Select(x => x.NomeArquivo)
                .ToListAsync(cancellationToken);

            var arquivosInterrompidos = await _contexto.ImportacoesControle
                .Where(x => x.AnoMes == _pastaAtual &&
                       (x.Status == EStatusImportacaoType.Erro || x.Status == EStatusImportacaoType.EmAndamento))
                .Select(x => x.NomeArquivo)
                .ToListAsync(cancellationToken);

            var arquivosDisponiveis = arquivosBase.Except(arquivosProcessados).ToList();

            _logger.LogInformation(
                MENSAGEM_ARQUIVOS_PENDENTES,
                _pastaAtual,
                string.Join(", ", arquivosDisponiveis));

            if (arquivosInterrompidos.Any())
            {
                _logger.LogInformation(
                    MENSAGEM_ARQUIVOS_INTERROMPIDOS,
                    string.Join(", ", arquivosInterrompidos));
            }

            return arquivosDisponiveis;
        }
    }
}