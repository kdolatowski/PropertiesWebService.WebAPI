
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using PropertiesWebService.DAL;

using PropertiesWebService.DatabaseMigrator;

namespace PropertiesWebService.DatabaseMigrator
{
    internal class PropertySearchDemoDbContextFactory : IDesignTimeDbContextFactory<PropertiesWebServiceDemoDbContext>
    {
        private bool initialized;
        private string? connectionString;

        private void Initialize(string[] args)
        {
            if (initialized)
                return;

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddEnvironmentVariables()
                .AddCommandLine(args);

            var configuration = configurationBuilder.Build();

            connectionString = configuration.GetConnectionString("Default")
                               ?? throw new ApplicationException("Missing configured connection string!");

            initialized = true;
        }

        public PropertiesWebServiceDemoDbContext CreateDbContext(string[] args)
        {
            Initialize(args);

            var optionsBuilder = new DbContextOptionsBuilder<PropertiesWebServiceDemoDbContext>();
            optionsBuilder.UseNpgsql(connectionString, x => x.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));
            return new PropertiesWebServiceDemoDbContext(optionsBuilder.Options);
        }
    }
}
