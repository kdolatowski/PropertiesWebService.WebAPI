using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;

namespace PropertiesWebService.DatabaseMigrator
{
    internal class FunctionHandler
    {
        public FunctionHandler()
        {
        }
        [Function("RunMigrations")]
        public static async Task Run([HttpTrigger] HttpRequest req)
        {
            await MigrationHandler.Migrate();
        }
    }
}
