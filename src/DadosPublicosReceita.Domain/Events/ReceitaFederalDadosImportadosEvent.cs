using DadosPublicosReceita.Domain.Enums;

namespace DadosPublicosReceita.Domain.Events
{
    public class ReceitaFederalDadosImportadosEvent
    {
        public DateTime DataImportacao { get; private set; }
        public EReceitaFederalArquivoType TipoArquivo { get; private set; }
        public int QuantidadeRegistros { get; private set; }
        public bool Sucesso { get; private set; }

        public ReceitaFederalDadosImportadosEvent(
            DateTime dataImportacao,
            EReceitaFederalArquivoType tipoArquivo,
            int quantidadeRegistros,
            bool sucesso)
        {
            DataImportacao = dataImportacao;
            TipoArquivo = tipoArquivo;
            QuantidadeRegistros = quantidadeRegistros;
            Sucesso = sucesso;
        }
    }
}