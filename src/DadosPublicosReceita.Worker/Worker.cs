using DadosPublicosReceita.Application.UseCases.ImportarDadosReceitaFederal;

namespace DadosPublicosReceita.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<Worker> _logger;

        public Worker(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<Worker> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var useCase = scope.ServiceProvider.GetRequiredService<ImportarDadosReceitaFederalUseCase>();

                try
                {
                    var resultado = await useCase.ExecuteAsync(stoppingToken);

                    if (resultado.Sucesso)
                        _logger.LogInformation($"Importação concluída: {resultado.Mensagem}");
                    else
                        _logger.LogWarning($"Falha na importação: {resultado.Mensagem}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro na execução do worker");
                }

                var amanha = DateTime.Today.AddDays(1);
                var delay = amanha - DateTime.Now;
                await Task.Delay(delay, stoppingToken);
            }
        }
    }
}