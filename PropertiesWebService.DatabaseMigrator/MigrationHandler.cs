using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using PropertiesWebService.DAL;

namespace PropertiesWebService.DatabaseMigrator
{
    internal class MigrationHandler
    {

        public static async Task Migrate()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables()
            #if (DEBUG)
                .AddJsonFile("appsettings.json")
            #endif
                .Build();

            var connectionString = configuration.GetConnectionString("Default");

            var optionsBuilder = new DbContextOptionsBuilder<PropertiesWebServiceDemoDbContext>();
            optionsBuilder
                .UseNpgsql(connectionString, x => x.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));
            var dbContext = new PropertiesWebServiceDemoDbContext(optionsBuilder.Options);

            var pending = (await dbContext.Database.GetPendingMigrationsAsync()).ToArray();
            var allMigrations = dbContext.Database.GetMigrations().ToArray();

            Console.WriteLine($"{allMigrations.Length - pending.Length}/{allMigrations.Length} migrations already applied.");

            if (pending.Length == 0)
            {
                Console.WriteLine("No migrations pending. All done.");
                return;
            }

            Console.WriteLine("Pending migrations:\n" + string.Join("\n", pending));
            Console.WriteLine("Applying migrations...");

            try
            {
                await dbContext.Database.MigrateAsync();
                Console.WriteLine("Migrations applied. All done.");
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }
        }
    }
}
