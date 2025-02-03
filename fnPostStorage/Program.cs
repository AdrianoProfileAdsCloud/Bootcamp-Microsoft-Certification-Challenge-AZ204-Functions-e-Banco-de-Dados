using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

// Configuração de Application Insights (opcional)
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

// Se quiser aumentar o limite do request body, precisa verificar como a função recebe os dados
// O KestrelServerOptions não funciona diretamente em Azure Functions

var host = builder.Build();
host.Run();
