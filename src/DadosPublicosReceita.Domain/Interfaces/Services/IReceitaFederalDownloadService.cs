namespace DadosPublicosReceita.Domain.Interfaces.Services
{
    public interface IReceitaFederalDownloadService
    {
        Task<bool> ExisteNovosDadosAsync(CancellationToken cancellationToken);
        Task<(Stream Stream, string Pasta, string Versao)> DownloadArquivoAsync(string nomeArquivo, CancellationToken cancellationToken);
        Task<IEnumerable<string>> ObterArquivosDisponiveisAsync(CancellationToken cancellationToken);
    }
}
