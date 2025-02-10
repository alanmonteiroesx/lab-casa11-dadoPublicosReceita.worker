using DadosPublicosReceita.Application.UseCases.ImportarDadosReceitaFederal;
using DadosPublicosReceita.Domain.Interfaces.Services;
using DadosPublicosReceita.Infrastructure.Data;
using DadosPublicosReceita.Infrastructure.Services;
using DadosPublicosReceita.Worker;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Net;

var builder = Host.CreateApplicationBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("A string de conexão 'DefaultConnection' não foi encontrada.");
}

builder.Services.AddHttpClient("ReceitaFederal", client =>
{
    var config = builder.Configuration.GetSection("HttpClient:ReceitaFederal");
    client.BaseAddress = new Uri(config["BaseUrl"]);
    client.Timeout = Timeout.InfiniteTimeSpan;
})
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    AllowAutoRedirect = true,
    MaxAutomaticRedirections = 3,
    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
})
.AddTransientHttpErrorPolicy(policy =>
    policy.WaitAndRetryAsync(
        retryCount: 7,
        sleepDurationProvider: retryAttempt =>
            TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
    ));

builder.Services.AddHostedService<Worker>();
builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseSqlServer(connectionString));
builder.Services.AddScoped<IReceitaFederalDownloadService, ReceitaFederalDownloadService>();
builder.Services.AddScoped<IReceitaFederalProcessamentoService, ReceitaFederalProcessamentoService>();
builder.Services.AddScoped<ImportarDadosReceitaFederalUseCase>();
builder.Logging.ClearProviders();
builder.Logging.AddConsole().SetMinimumLevel(LogLevel.Error);

var host = builder.Build();
host.Run();