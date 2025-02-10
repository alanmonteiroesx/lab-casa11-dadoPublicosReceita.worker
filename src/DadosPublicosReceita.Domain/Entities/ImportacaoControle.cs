using DadosPublicosReceita.Domain.Enums;
using DadosPublicosReceita.Shared.Domain;

namespace DadosPublicosReceita.Domain.Entities
{
    public class ImportacaoControle : Entity
    {
        public ImportacaoControle(
            string anoMes,
            string nomeArquivo,
            string versaoArquivo)
        {
            AnoMes = anoMes;
            NomeArquivo = nomeArquivo;
            Status = EStatusImportacaoType.EmAndamento;
            DataInicio = DateTime.Now;
            VersaoArquivo = versaoArquivo;
        }

        public string AnoMes { get; private set; }
        public string NomeArquivo { get; private set; }
        public EStatusImportacaoType Status { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime? DataConclusao { get; private set; }
        public int QuantidadeRegistros { get; private set; }
        public int UltimoRegistroProcessado { get; private set; }
        public string? Erro { get; private set; }
        public string VersaoArquivo { get; private set; }

        public void RegistrarProgresso(int registrosProcessados)
        {
            UltimoRegistroProcessado = registrosProcessados;
        }

        public void Concluir(int quantidadeTotal)
        {
            Status = EStatusImportacaoType.Concluido;
            DataConclusao = DateTime.Now;
            QuantidadeRegistros = quantidadeTotal;
            UltimoRegistroProcessado = quantidadeTotal;
        }

        public void RegistrarErro(string mensagemErro)
        {
            Status = EStatusImportacaoType.Erro;
            DataConclusao = DateTime.Now;
            Erro = mensagemErro;
        }

        public void Cancelar()
        {
            Status = EStatusImportacaoType.Cancelado;
            DataConclusao = DateTime.Now;
        }
    }
}
