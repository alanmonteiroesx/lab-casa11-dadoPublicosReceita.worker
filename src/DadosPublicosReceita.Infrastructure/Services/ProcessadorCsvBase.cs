using CsvHelper;
using CsvHelper.Configuration;
using DadosPublicosReceita.Domain.Entities;
using DadosPublicosReceita.Infrastructure.Data;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Globalization;

namespace DadosPublicosReceita.Infrastructure.Services
{
    public abstract class ProcessadorCsvBase
    {
        protected readonly AppDbContext Contexto;
        protected readonly ILogger Logger;
        protected readonly ImportacaoControle Controle;
        protected const int TamanhoDoLote = 30000;

        protected ProcessadorCsvBase(AppDbContext contexto, ILogger logger)
        {
            Contexto = contexto;
            Logger = logger;
        }

        protected ProcessadorCsvBase(AppDbContext contexto, ILogger logger, ImportacaoControle controle)
        {
            Contexto = contexto;
            Logger = logger;
            Controle = controle;
        }

        protected CsvConfiguration ObterConfiguracaoPadrao() =>
            new(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                HasHeaderRecord = false,
                Quote = '"',
                TrimOptions = TrimOptions.Trim,
                MissingFieldFound = null
            };

        protected string ObterValorCampo(CsvReader csv, int index) =>
            csv.GetField(index)?.Trim('"') ?? string.Empty;

        protected async Task ProcessarLote<T>(
            List<T> itens,
            Func<List<T>, CancellationToken, Task> funcaoSalvar,
            CancellationToken cancellationToken) where T : class
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                await funcaoSalvar(itens, cancellationToken);

                if (Controle != null)
                {
                    Controle.RegistrarProgresso(itens.Count);
                    Contexto.BulkUpdate(new List<ImportacaoControle> { Controle }, options => {
                        options.ColumnPrimaryKeyExpression = x => x.Id;
                    });
                }

                itens.Clear();
                sw.Stop();
                Logger.LogInformation("Tempo para processamento de lote " + sw.Elapsed.TotalSeconds.ToString());
            }
            catch (Exception ex)
            {
                if (Controle != null)
                {
                    Controle.RegistrarErro(ex.Message);
                    Contexto.BulkUpdate(new List<ImportacaoControle> { Controle }, options => {
                        options.ColumnPrimaryKeyExpression = x => x.Id;
                    });
                }
                Logger.LogError(ex, $"Erro ao salvar lote de {typeof(T).Name}");
                throw;
            }
        }

        protected async Task<int> PularRegistrosProcessados(CsvReader csv)
        {
            if (Controle == null)
                return 0;

            var registrosParaPular = Controle.UltimoRegistroProcessado;
            var registroAtual = 0;

            while (registroAtual < registrosParaPular && await csv.ReadAsync())
            {
                registroAtual++;
            }

            return registroAtual;
        }

        protected void FinalizarProcessamento(int totalRegistros)
        {
            if (Controle != null)
            {
                Controle.Concluir(totalRegistros);
                Contexto.BulkUpdate(new List<ImportacaoControle> { Controle }, options => {
                    options.ColumnPrimaryKeyExpression = x => x.Id;
                });
            }
        }

        protected DateOnly? ConverterData(string? valor)
        {
            if (string.IsNullOrEmpty(valor))
                return null;

            try
            {
                if (valor.Length != 8)
                {
                    return null;
                }

                return DateOnly.ParseExact(valor, "yyyyMMdd", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Erro ao converter data: {Data}", valor);
                return null;
            }
        }

        protected decimal ConverterDecimal(string? valor)
        {
            if (string.IsNullOrEmpty(valor))
                return 0;

            valor = valor.Replace(".", "").Replace(",", ".");
            return decimal.TryParse(valor, NumberStyles.Any, CultureInfo.InvariantCulture, out var resultado)
                ? resultado
                : 0;
        }
    }
}
