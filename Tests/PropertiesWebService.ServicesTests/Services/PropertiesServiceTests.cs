using FluentAssertions;

using Newtonsoft.Json;

using PropertiesWebService.DAL;
using PropertiesWebService.DAL.Entities;
using PropertiesWebService.Models.Models;
using PropertiesWebService.Models.SearchCriteria;
using PropertiesWebService.Services.MappingProfiles;
using PropertiesWebService.Services.Services;
using PropertiesWebService.ServicesTests.TestFixtures;

using System.ComponentModel;

namespace PropertiesWebService.ServicesTests.Services
{
    public class PropertiesServiceTests : IClassFixture<TestDbFixture>
    {
        private readonly PropertySearchCriteriaService _searchCriteriaService;
        private readonly PropertiesWebServiceDemoDbContext _dbContext;
        private readonly PropertiesService _propertiesService;

        public PropertiesServiceTests(TestDbFixture fixture)
        {
            _searchCriteriaService = new PropertySearchCriteriaService();
            _dbContext = fixture.CreateContext();
            _propertiesService = new PropertiesService(_searchCriteriaService, _dbContext);

            MapsterConfig.RegisterMappings();
        }

        [Fact]
        public async Task Should_Return_Properties_In_Order_To_QueryFilter()
        {
            // setup mock data
            var searchQuery = new Query<PropertySearchCriteria>
            {
                SearchCriteria = new PropertySearchCriteria
                {
                    PriceMax = 1300,
                },
                Page = 0,
                PageSize = 5,
                SortMember = nameof(Property.Address),
                SortDirection = ListSortDirection.Ascending
            };

            var str = JsonConvert.SerializeObject(searchQuery, Formatting.Indented);

            // Act
            var result = await _propertiesService.GetAsync(searchQuery);

            result.Should().NotBeNull();
            result.Results.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Should_Add_Property_To_Database()
        {
            // setup
            var item = new PropertyModel
            {
                Address = "New Property",
                Price = 500.0M,
                TypeId = 2,
                Spaces =
                [
                    new SpaceModel { Size = 50.0M, TypeId = 2 },
                    new SpaceModel { Size = 30.0M, TypeId = 4 }
                ]
            };

            var str = JsonConvert.SerializeObject(item, Formatting.Indented);

            var result = await _propertiesService.AddAsync(item);

            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);

            if (result.Id > 0)
            {
                await _propertiesService.DeleteAsync(result.Id);
            }

        }
        [Fact]
        public async Task Should_Return_Property_Data()
        {
            // setup mock data
            var id = 3;
            // Act
            var result = await _propertiesService.GetAsync(id);
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
        }

    }
}
