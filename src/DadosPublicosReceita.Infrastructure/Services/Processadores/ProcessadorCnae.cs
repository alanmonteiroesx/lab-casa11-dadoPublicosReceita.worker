using CsvHelper;
using DadosPublicosReceita.Domain.Entities;
using DadosPublicosReceita.Domain.Interfaces.Services;
using DadosPublicosReceita.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace DadosPublicosReceita.Infrastructure.Services.Processadores
{
    public class ProcessadorCnae : ProcessadorCsvBase, IProcessadorCsv<Cnae>
    {

        public ProcessadorCnae(AppDbContext contexto, ILogger logger, ImportacaoControle controle)
            : base(contexto, logger, controle) { }

        public async Task<int> ProcessarAsync(StreamReader reader, CancellationToken cancellationToken)
        {
            using var csv = new CsvReader(reader, ObterConfiguracaoPadrao());
            var registros = await PularRegistrosProcessados(csv);
            var cnaes = new List<Cnae>();

            while (await csv.ReadAsync())
            {
                try
                {
                    var codigo = ObterValorCampo(csv, 0);
                    var descricao = ObterValorCampo(csv, 1);

                    if (string.IsNullOrEmpty(codigo)) continue;

                    cnaes.Add(new Cnae(codigo, descricao));
                    registros++;

                    if (cnaes.Count >= TamanhoDoLote)
                        await ProcessarLote(cnaes, SalvarCnaesAsync, cancellationToken);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Erro ao processar linha de CNAE");
                }
            }

            if (cnaes.Any())
                await ProcessarLote(cnaes, SalvarCnaesAsync, cancellationToken);

            FinalizarProcessamento(registros);
            return registros;
        }

        private async Task SalvarCnaesAsync(List<Cnae> cnaes, CancellationToken cancellationToken)
        {
            await Task.Run(() => {
                Contexto.BulkInsert(cnaes, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = c => c.Codigo;
                });
            }, cancellationToken);
        }
    }
}