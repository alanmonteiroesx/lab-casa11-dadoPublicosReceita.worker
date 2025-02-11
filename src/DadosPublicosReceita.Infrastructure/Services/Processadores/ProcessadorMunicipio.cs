
using CsvHelper;
using DadosPublicosReceita.Domain.Entities;
using DadosPublicosReceita.Domain.Interfaces.Services;
using DadosPublicosReceita.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace DadosPublicosReceita.Infrastructure.Services.Processadores
{
    public class ProcessadorMunicipio : ProcessadorCsvBase, IProcessadorCsv<Municipio>
    {
        public ProcessadorMunicipio(AppDbContext contexto, ILogger logger, ImportacaoControle controle)
            : base(contexto, logger, controle) { }

        public async Task<int> ProcessarAsync(StreamReader reader, CancellationToken cancellationToken)
        {
            using var csv = new CsvReader(reader, ObterConfiguracaoPadrao());
            var registros = await PularRegistrosProcessados(csv);
            var municipios = new List<Municipio>();

            while (await csv.ReadAsync())
            {
                try
                {
                    var codigo = ObterValorCampo(csv, 0);
                    var nome = ObterValorCampo(csv, 1);

                    if (string.IsNullOrEmpty(codigo)) continue;

                    municipios.Add(new Municipio(codigo, nome));
                    registros++;

                    if (municipios.Count >= TamanhoDoLote)
                        await ProcessarLote(municipios, SalvarMunicipiosAsync, cancellationToken);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Erro ao processar linha de município");
                }
            }

            if (municipios.Count > 0)
                await ProcessarLote(municipios, SalvarMunicipiosAsync, cancellationToken);

            FinalizarProcessamento(registros);
            return registros;
        }

        private async Task SalvarMunicipiosAsync(List<Municipio> municipios, CancellationToken cancellationToken)
        {
            await Task.Run(() => {
                Contexto.BulkInsert(municipios, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = c => c.Codigo;
                });
            }, cancellationToken);
        }
    }

}
