using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PropertiesWebService.DatabaseMigrator;

internal class Program
{
    private static IConfiguration Configuration { get; set; } = null!;
    private static void Main(string[] args)
    {
        var builder = FunctionsApplication.CreateBuilder(args);

        builder.ConfigureFunctionsWebApplication();

        builder.Services
            .AddApplicationInsightsTelemetryWorkerService()
            .ConfigureFunctionsApplicationInsights();

        Task.Run(MigrationHandler.Migrate).GetAwaiter().GetResult();

        builder.Build().Run();
    }
}
