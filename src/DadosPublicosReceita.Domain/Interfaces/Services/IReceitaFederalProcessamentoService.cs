using DadosPublicosReceita.Domain.Enums;

namespace DadosPublicosReceita.Domain.Interfaces.Services
{
    public interface IReceitaFederalProcessamentoService
    {
        Task<int> ProcessarArquivoAsync(
            Stream streamArquivo,
            string nomeArquivo,
            string pasta,
            string versao,
            EReceitaFederalArquivoType tipo,
            CancellationToken cancellationToken);
    }
}