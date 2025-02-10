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
                    ?? throw new InvalidOperationException("Arquivo ZIP vazio");

                using var streamConteudo = entry.Open();
                using var reader = new StreamReader(streamConteudo, Encoding.GetEncoding("ISO-8859-1"));

                return tipo switch
                {
                    EReceitaFederalArquivoType.Cnae => await new ProcessadorCnae(_contexto, _logger, controle).ProcessarAsync(reader, cancellationToken),
                    EReceitaFederalArquivoType.Empresa => await new ProcessadorEmpresa(_contexto, _logger, controle).ProcessarAsync(reader, cancellationToken),
                    EReceitaFederalArquivoType.Estabelecimento => await new ProcessadorEstabelecimento(_contexto, _logger, controle).ProcessarAsync(reader, cancellationToken),
                    EReceitaFederalArquivoType.Municipio => await new ProcessadorMunicipio(_contexto, _logger, controle).ProcessarAsync(reader, cancellationToken),
                    EReceitaFederalArquivoType.Pais => await new ProcessadorPais(_contexto, _logger, controle).ProcessarAsync(reader, cancellationToken),
                    _ => throw new NotImplementedException($"Processador não implementado para o tipo {tipo}")
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar arquivo {Arquivo} do tipo {Tipo}", nomeArquivo, tipo);
                throw;
            }
        }
    }
}