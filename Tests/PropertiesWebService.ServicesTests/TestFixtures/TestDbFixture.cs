using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PropertiesWebService.DAL;
using PropertiesWebService.ServicesTests.Helpers;

namespace PropertiesWebService.ServicesTests.TestFixtures
{
    public class TestDbFixture
    {
        private const string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=TestDbFixture;Trusted_Connection=True;ConnectRetryCount=0";

        private static readonly object _lock = new();
        private static bool _databaseInitialized;

        public TestDbFixture()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        var properties = PropertiesServiceHelper.GetTestProperties(10, 10);
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();
                        context.AddRange(properties);
                        context.SaveChanges();
                    }

                    _databaseInitialized = true;
                }
            }
        }

        public PropertiesWebServiceDemoDbContext CreateContext()
            => new(
                new DbContextOptionsBuilder<PropertiesWebServiceDemoDbContext>()
                    .UseSqlServer(ConnectionString)
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                    .Options);
    }
}
