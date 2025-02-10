using DadosPublicosReceita.Domain.Enums;

namespace DadosPublicosReceita.Application.UseCases.ImportarDadosReceitaFederal
{
    public class ImportarDadosReceitaFederalResponse
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public Dictionary<EReceitaFederalArquivoType, int> RegistrosImportadosPorTipo { get; set; }
        public DateTime DataProcessamento { get; set; }
    }
}