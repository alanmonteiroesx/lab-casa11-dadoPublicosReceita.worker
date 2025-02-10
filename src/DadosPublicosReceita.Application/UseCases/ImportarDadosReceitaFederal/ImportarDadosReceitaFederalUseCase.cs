using DadosPublicosReceita.Domain.Enums;
using DadosPublicosReceita.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;


namespace DadosPublicosReceita.Application.UseCases.ImportarDadosReceitaFederal
{
    public class ImportarDadosReceitaFederalUseCase
    {
        private readonly IReceitaFederalDownloadService _downloadService;
        private readonly IReceitaFederalProcessamentoService _processamentoService;
        private readonly ILogger<ImportarDadosReceitaFederalUseCase> _logger;

        public ImportarDadosReceitaFederalUseCase(
            IReceitaFederalDownloadService downloadService,
            IReceitaFederalProcessamentoService processamentoService,
            ILogger<ImportarDadosReceitaFederalUseCase> logger)
        {
            _downloadService = downloadService;
            _processamentoService = processamentoService;
            _logger = logger;
        }

        public async Task<ImportarDadosReceitaFederalResponse> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new ImportarDadosReceitaFederalResponse
            {
                RegistrosImportadosPorTipo = new Dictionary<EReceitaFederalArquivoType, int>(),
                DataProcessamento = DateTime.Now
            };

            try
            {
                if (!await _downloadService.ExisteNovosDadosAsync(cancellationToken))
                {
                    response.Sucesso = true;
                    response.Mensagem = "Não existem novos dados para importar";
                    return response;
                }

                var arquivosDisponiveis = await _downloadService.ObterArquivosDisponiveisAsync(cancellationToken);

                foreach (var arquivo in arquivosDisponiveis)
                {
                    if (cancellationToken.IsCancellationRequested)
                        break;

                    var downloadResult = await _downloadService.DownloadArquivoAsync(arquivo, cancellationToken);
                    using (downloadResult.Stream)
                    {
                        var tipo = ObterTipoArquivo(arquivo);

                        var registrosImportados = await _processamentoService.ProcessarArquivoAsync(
                            downloadResult.Stream,
                            arquivo,
                            downloadResult.Pasta,
                            downloadResult.Versao,
                            tipo,
                            cancellationToken);

                        response.RegistrosImportadosPorTipo[tipo] = registrosImportados;
                    }
                }

                response.Sucesso = true;
                response.Mensagem = "Importação concluída com sucesso";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao importar dados da Receita Federal");
                response.Sucesso = false;
                response.Mensagem = $"Erro na importação: {ex.Message}";
            }

            return response;
        }

        private EReceitaFederalArquivoType ObterTipoArquivo(string nomeArquivo)
        {
            if (nomeArquivo.StartsWith("Cnaes")) return EReceitaFederalArquivoType.Cnae;
            if (nomeArquivo.StartsWith("Empresas")) return EReceitaFederalArquivoType.Empresa;
            if (nomeArquivo.StartsWith("Estabelecimentos")) return EReceitaFederalArquivoType.Estabelecimento;
            if (nomeArquivo.StartsWith("Municipios")) return EReceitaFederalArquivoType.Municipio;
            if (nomeArquivo.StartsWith("Paises")) return EReceitaFederalArquivoType.Pais;

            throw new ArgumentException($"Tipo de arquivo não reconhecido: {nomeArquivo}");
        }
    }
}