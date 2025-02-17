using DadosPublicosReceita.Domain.Entities;
using DadosPublicosReceita.Domain.Enums;
using DadosPublicosReceita.Domain.Interfaces.Services;
using DadosPublicosReceita.Infrastructure.Data;
using DadosPublicosReceita.Infrastructure.Services.Processadores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.IO.Compression;
using System.Text;

namespace DadosPublicosReceita.Infrastructure.Services
{
    public class ReceitaFederalProcessamentoService : IReceitaFederalProcessamentoService
    {
        private readonly AppDbContext _contexto;
        private readonly ILogger<ReceitaFederalProcessamentoService> _logger;

        private const string ENCODING_PADRAO = "ISO-8859-1";
        private const string ERRO_ZIP_VAZIO = "Arquivo ZIP vazio";
        private const string ERRO_PROCESSADOR_NAO_IMPLEMENTADO = "Processador não implementado para o tipo {0}";
        private const string MENSAGEM_ERRO_PROCESSAMENTO = "Erro ao processar arquivo {Arquivo} do tipo {Tipo}";

        public ReceitaFederalProcessamentoService(
            AppDbContext contexto,
            ILogger<ReceitaFederalProcessamentoService> logger)
        {
            _contexto = contexto;
            _logger = logger;
        }

        public async Task<int> ProcessarArquivoAsync(
            Stream streamArquivo,
            string nomeArquivo,
            string pasta,
            string versao,
            EReceitaFederalArquivoType tipo,
            CancellationToken cancellationToken)
        {
            try
            {
                var controleExistente = await _contexto.ImportacoesControle
                    .FirstOrDefaultAsync(x =>
                        x.AnoMes == pasta &&
                        x.NomeArquivo == nomeArquivo &&
                        x.Status != EStatusImportacaoType.Concluido,
                        cancellationToken);

                ImportacaoControle controle;
                if (controleExistente == null)
                {
                    controle = new ImportacaoControle(pasta, nomeArquivo, versao);
                    _contexto.BulkInsert(new List<ImportacaoControle> { controle }, options =>
                    {
                        options.InsertIfNotExists = true;
                        options.ColumnPrimaryKeyExpression = x => new { x.AnoMes, x.NomeArquivo };
                    });
                }
                else
                {
                    controle = controleExistente;
                }

                using var archive = new ZipArchive(streamArquivo);
                var entry = archive.Entries.FirstOrDefault()
                    ?? throw new InvalidOperationException(ERRO_ZIP_VAZIO);

                using var streamConteudo = entry.Open();
                using var reader = new StreamReader(streamConteudo, Encoding.GetEncoding(ENCODING_PADRAO));

                return tipo switch
                {
                    EReceitaFederalArquivoType.Cnae => await new ProcessadorCnae(_contexto, _logger, controle).ProcessarAsync(reader, cancellationToken),
                    EReceitaFederalArquivoType.Empresa => await new ProcessadorEmpresa(_contexto, _logger, controle).ProcessarAsync(reader, cancellationToken),
                    EReceitaFederalArquivoType.Estabelecimento => await new ProcessadorEstabelecimento(_contexto, _logger, controle).ProcessarAsync(reader, cancellationToken),
                    EReceitaFederalArquivoType.Municipio => await new ProcessadorMunicipio(_contexto, _logger, controle).ProcessarAsync(reader, cancellationToken),
                    EReceitaFederalArquivoType.Pais => await new ProcessadorPais(_contexto, _logger, controle).ProcessarAsync(reader, cancellationToken),
                    _ => throw new NotImplementedException(string.Format(ERRO_PROCESSADOR_NAO_IMPLEMENTADO, tipo))
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MENSAGEM_ERRO_PROCESSAMENTO, nomeArquivo, tipo);
                throw;
            }
        }
    }
}