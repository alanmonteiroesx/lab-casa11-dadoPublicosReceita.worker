namespace DadosPublicosReceita.Domain.Interfaces.Services
{
    public interface IProcessadorCsv<T> where T : class
    {
        Task<int> ProcessarAsync(StreamReader reader, CancellationToken cancellationToken);
    }
}
