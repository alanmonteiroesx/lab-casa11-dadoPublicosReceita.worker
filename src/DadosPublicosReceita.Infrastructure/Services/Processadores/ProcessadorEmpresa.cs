using CsvHelper;
using DadosPublicosReceita.Domain.Entities;
using DadosPublicosReceita.Domain.Enums;
using DadosPublicosReceita.Domain.Interfaces.Services;
using DadosPublicosReceita.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace DadosPublicosReceita.Infrastructure.Services.Processadores
{
    public class ProcessadorEmpresa : ProcessadorCsvBase, IProcessadorCsv<Empresa>
    {
        public ProcessadorEmpresa(AppDbContext contexto, ILogger logger, ImportacaoControle controle)
            : base(contexto, logger, controle) { }

        public async Task<int> ProcessarAsync(StreamReader reader, CancellationToken cancellationToken)
        {
            using var csv = new CsvReader(reader, ObterConfiguracaoPadrao());
            var registros = await PularRegistrosProcessados(csv);
            var empresas = new List<Empresa>();

            while (await csv.ReadAsync())
            {
                try
                {
                    var empresa = CriarEmpresa(csv);
                    if (empresa == null) continue;

                    empresas.Add(empresa);
                    registros++;

                    if (empresas.Count >= TamanhoDoLote)
                        await ProcessarLote(empresas, SalvarEmpresasAsync, cancellationToken);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Erro ao processar linha de empresa");
                }
            }

            if (empresas.Any())
                await ProcessarLote(empresas, SalvarEmpresasAsync, cancellationToken);

            FinalizarProcessamento(registros);
            return registros;
        }

        private Empresa? CriarEmpresa(CsvReader csv)
        {
            var cnpjBasico = ObterValorCampo(csv, 0);
            var razaoSocial = ObterValorCampo(csv, 1);
            var porteEmpresaStr = ObterValorCampo(csv, 5);

            if (string.IsNullOrEmpty(cnpjBasico) || string.IsNullOrEmpty(razaoSocial))
                return null;

            var porteEmpresa = EPorteEmpresaType.NaoInformado;
            if (!string.IsNullOrEmpty(porteEmpresaStr) && int.TryParse(porteEmpresaStr, out int porte))
            {
                porteEmpresa = (EPorteEmpresaType)porte;
            }

            return new Empresa(
                cnpjBasico: cnpjBasico,
                razaoSocial: razaoSocial,
                naturezaJuridica: ObterValorCampo(csv, 2),
                qualificacaoResponsavel: ObterValorCampo(csv, 3),
                capitalSocial: ConverterDecimal(ObterValorCampo(csv, 4)),
                enteFederativoResp: ObterValorCampo(csv, 6),
                porteEmpresa: porteEmpresa
            );
        }

        private async Task SalvarEmpresasAsync(List<Empresa> empresas, CancellationToken cancellationToken)
        {
            Contexto.BulkInsert(empresas, options => {
                options.InsertIfNotExists = true;
                options.ColumnPrimaryKeyExpression = e => e.CnpjBasico;
            });
        }
    }
}
