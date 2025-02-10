using CsvHelper;
using DadosPublicosReceita.Domain.Entities;
using DadosPublicosReceita.Domain.Enums;
using DadosPublicosReceita.Domain.Interfaces.Services;
using DadosPublicosReceita.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace DadosPublicosReceita.Infrastructure.Services.Processadores
{
    public class ProcessadorEstabelecimento : ProcessadorCsvBase, IProcessadorCsv<Estabelecimento>
    {
        private class DadosEstabelecimento
        {
            public Estabelecimento Estabelecimento { get; set; }
            public Endereco Endereco { get; set; }
            public List<Telefone> Telefones { get; set; }

            public DadosEstabelecimento(Estabelecimento estabelecimento, Endereco endereco, List<Telefone> telefones)
            {
                Estabelecimento = estabelecimento;
                Endereco = endereco;
                Telefones = telefones;
            }
        }

        public ProcessadorEstabelecimento(AppDbContext contexto, ILogger logger, ImportacaoControle controle)
            : base(contexto, logger, controle) { }

        public async Task<int> ProcessarAsync(StreamReader reader, CancellationToken cancellationToken)
        {
            using var csv = new CsvReader(reader, ObterConfiguracaoPadrao());
            var registros = await PularRegistrosProcessados(csv);
            var estabelecimentos = new List<DadosEstabelecimento>();

            while (await csv.ReadAsync())
            {
                try
                {
                    var linha = new Dictionary<int, string>();

                    for (int i = 0; i < 30; i++)
                    {
                        linha[i] = ObterValorCampo(csv, i);
                    }

                    var dados = ProcessarLinha(csv, linha);
                    if (dados == null) continue;

                    estabelecimentos.Add(dados);
                    registros++;

                    if (estabelecimentos.Count >= TamanhoDoLote)
                        await ProcessarLote(estabelecimentos, SalvarEstabelecimentosAsync, cancellationToken);
                }
                catch (Exception ex)
                {
                    var detalhesErro = new
                    {
                        CnpjBasico = ObterValorCampo(csv, 0),
                        CnpjOrdem = ObterValorCampo(csv, 1),
                        CnpjDv = ObterValorCampo(csv, 2),
                        NomeFantasia = ObterValorCampo(csv, 4),
                        DataSituacaoCadastral = ObterValorCampo(csv, 6),
                        Email = ObterValorCampo(csv, 27),
                        Endereco = new
                        {
                            Logradouro = ObterValorCampo(csv, 14),
                            Numero = ObterValorCampo(csv, 15),
                            Bairro = ObterValorCampo(csv, 17),
                            Cep = ObterValorCampo(csv, 18),
                            Municipio = ObterValorCampo(csv, 20)
                        }
                    };

                    Logger.LogError(ex, "Erro ao processar estabelecimento: {@Estabelecimento}", detalhesErro);
                }
            }

            if (estabelecimentos.Any())
                await ProcessarLote(estabelecimentos, SalvarEstabelecimentosAsync, cancellationToken);

            FinalizarProcessamento(registros);
            return registros;
        }

        private DadosEstabelecimento? ProcessarLinha(CsvReader csv, Dictionary<int, string> linha)
        {
            try
            {
                var estabelecimento = CriarEstabelecimento(csv);
                if (estabelecimento == null) return null;

                var endereco = CriarEndereco(csv, estabelecimento);
                var telefones = CriarTelefones(csv, estabelecimento);

                return new DadosEstabelecimento(estabelecimento, endereco, telefones);
            }
            catch (Exception ex)
            {
                var detalhesLinha = linha.ToDictionary(
                    k => $"Campo_{k.Key}",
                    v => v.Value
                );

                Logger.LogError(ex, "Erro ao processar linha do estabelecimento. Dados: {@DadosLinha}", detalhesLinha);
                throw;
            }
        }

        private Estabelecimento? CriarEstabelecimento(CsvReader csv)
        {
            var cnpjBasico = ObterValorCampo(csv, 0);
            if (string.IsNullOrEmpty(cnpjBasico)) return null;

            return new Estabelecimento(
                cnpjBasico: cnpjBasico,
                cnpjOrdem: ObterValorCampo(csv, 1),
                cnpjDv: ObterValorCampo(csv, 2),
                tipoDoEstabelecimento: (EEstabelecimentoType)byte.Parse(ObterValorCampo(csv, 3)),
                nomeFantasia: ObterValorCampo(csv, 4),
                dataInicioAtividade: ConverterData(ObterValorCampo(csv, 10)) ?? DateOnly.FromDateTime(DateTime.Today),
                situacaoCadastral: (ESituacaoCadastralType)byte.Parse(ObterValorCampo(csv, 5)),
                dataSituacaoCadastral: ConverterData(ObterValorCampo(csv, 6)) ?? DateOnly.FromDateTime(DateTime.Today),
                motivoSituacaoCadastral: ObterValorCampo(csv, 7),
                situacaoEspecial: ObterValorCampo(csv, 28),
                dataSituacaoEspecial: ConverterData(ObterValorCampo(csv, 29)),
                email: ObterValorCampo(csv, 27),
                cnaePrincipalId: ObterValorCampo(csv, 11)
            );
        }

        private Endereco CriarEndereco(CsvReader csv, Estabelecimento estabelecimento)
        {
            return new Endereco(
                tipoLogradouro: ObterValorCampo(csv, 13),
                logradouro: ObterValorCampo(csv, 14),
                numero: ObterValorCampo(csv, 15),
                complemento: ObterValorCampo(csv, 16),
                bairro: ObterValorCampo(csv, 17),
                cep: ObterValorCampo(csv, 18),
                uf: ObterValorCampo(csv, 19),
                municipioId: ObterValorCampo(csv, 20),
                paisId: ObterValorCampo(csv, 9),
                estabelecimentoCnpjBasico: estabelecimento.CnpjBasico,
                estabelecimentoCnpjOrdem: estabelecimento.CnpjOrdem,
                estabelecimentoCnpjDv: estabelecimento.CnpjDv
            );
        }

        private List<Telefone> CriarTelefones(CsvReader csv, Estabelecimento estabelecimento)
        {
            var telefones = new List<Telefone>();

            for (int i = 0; i < 3; i++)
            {
                var ddd = ObterValorCampo(csv, 21 + (i * 2));
                var numero = ObterValorCampo(csv, 22 + (i * 2));

                if (!string.IsNullOrEmpty(ddd) && !string.IsNullOrEmpty(numero))
                {
                    telefones.Add(new Telefone(
                        ddd,
                        numero,
                        estabelecimento.CnpjBasico,
                        estabelecimento.CnpjOrdem,
                        estabelecimento.CnpjDv
                    ));
                }
            }

            return telefones;
        }

        private async Task SalvarEstabelecimentosAsync(List<DadosEstabelecimento> items, CancellationToken cancellationToken)
        {
            var estabelecimentos = items.Select(item =>
            {
                item.Estabelecimento.SetEndereco(item.Endereco);
                foreach (var telefone in item.Telefones)
                {
                    item.Estabelecimento.AdicionarTelefone(telefone);
                }
                return item.Estabelecimento;
            }).ToList();

            var enderecos = items.Select(x => x.Endereco).ToList();
            var telefones = items.SelectMany(x => x.Telefones).ToList();

            try
            {
                await Contexto.BulkInsertAsync(estabelecimentos, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = e => new { e.CnpjBasico, e.CnpjOrdem, e.CnpjDv };
                    options.BatchSize = 5000;
                }, cancellationToken);

                await Contexto.BulkInsertAsync(enderecos, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = e => new {
                        e.EstabelecimentoCnpjBasico,
                        e.EstabelecimentoCnpjOrdem,
                        e.EstabelecimentoCnpjDv
                    };
                    options.BatchSize = 5000;
                }, cancellationToken);

                await Contexto.BulkInsertAsync(telefones, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = t => new {
                        t.EstabelecimentoCnpjBasico,
                        t.EstabelecimentoCnpjOrdem,
                        t.EstabelecimentoCnpjDv,
                        t.Codigo,
                        t.Numero
                    };
                    options.BatchSize = 5000;
                }, cancellationToken);
            }
            catch (SqlException sqlEx)
            {
                foreach (SqlError ex in sqlEx.Errors)
                {
                    Logger.LogError(ex.Message + ex.Source);
                };
            }
        }
    }
}