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
    public class SpacesServiceTests : IClassFixture<TestDbFixture>
    {
        private readonly SpaceSearchCriteriaService _searchCriteriaService;
        private readonly PropertiesWebServiceDemoDbContext _dbContext;
        private readonly SpacesService _spacesService;

        public SpacesServiceTests(TestDbFixture fixture)
        {
            _searchCriteriaService = new SpaceSearchCriteriaService();
            _dbContext = fixture.CreateContext();
            _spacesService = new SpacesService(_searchCriteriaService, _dbContext);

            MapsterConfig.RegisterMappings();
        }

        [Fact]
        public async Task Should_Return_Spaces_In_Order_To_QueryFilter()
        {
            // setup mock data
            var searchQuery = new Query<SpaceSearchCriteria>
            {
                SearchCriteria = new SpaceSearchCriteria
                {
                    SizeMax = 1300,
                },
                Page = 0,
                PageSize = 5,
                SortMember = nameof(Space.Size),
                SortDirection = ListSortDirection.Descending
            };

            var str = JsonConvert.SerializeObject(searchQuery, Formatting.Indented);

            // Act
            var result = await _spacesService.GetAsync(searchQuery);

            result.Should().NotBeNull();
            result.Results.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Should_Add_Space_To_Database()
        {
            // setup
            var item = new SpaceModel
            {
                PropertyId = 3,
                Size = 200.0M,
                TypeId = 2,
            };

            var str = JsonConvert.SerializeObject(item, Formatting.Indented);

            var result = await _spacesService.AddAsync(item);

            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);

            if (result.Id > 0)
            {
                await _spacesService.DeleteAsync(result.Id);
            }

        }
        [Fact]
        public async Task Should_Return_Space_Data()
        {
            // setup mock data
            var id = 105;
            // Act
            var result = await _spacesService.GetAsync(id);
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
        }
    }
}
