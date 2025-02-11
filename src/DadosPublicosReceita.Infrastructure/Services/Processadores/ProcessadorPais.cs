using CsvHelper;
using DadosPublicosReceita.Domain.Entities;
using DadosPublicosReceita.Domain.Interfaces.Services;
using DadosPublicosReceita.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace DadosPublicosReceita.Infrastructure.Services.Processadores
{
    public class ProcessadorPais : ProcessadorCsvBase, IProcessadorCsv<Pais>
    {
        public ProcessadorPais(AppDbContext contexto, ILogger logger, ImportacaoControle controle)
            : base(contexto, logger, controle) { }

        public async Task<int> ProcessarAsync(StreamReader reader, CancellationToken cancellationToken)
        {
            using var csv = new CsvReader(reader, ObterConfiguracaoPadrao());
            var registros = await PularRegistrosProcessados(csv);
            var paises = new List<Pais>();

            while (await csv.ReadAsync())
            {
                try
                {
                    var codigo = ObterValorCampo(csv, 0);
                    var nome = ObterValorCampo(csv, 1);

                    if (string.IsNullOrEmpty(codigo)) continue;

                    paises.Add(new Pais(codigo, nome));
                    registros++;

                    if (paises.Count >= TamanhoDoLote)
                        await ProcessarLote(paises, SalvarPaisesAsync, cancellationToken);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Erro ao processar linha de país");
                }
            }

            if (paises.Count > 0)
                await ProcessarLote(paises, SalvarPaisesAsync, cancellationToken);

            FinalizarProcessamento(registros);
            return registros;
        }

        private async Task SalvarPaisesAsync(List<Pais> paises, CancellationToken cancellationToken)
        {
            await Task.Run(() => {
                Contexto.BulkInsert(paises, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = c => c.Codigo;
                });
            }, cancellationToken);
        }
    }
}
