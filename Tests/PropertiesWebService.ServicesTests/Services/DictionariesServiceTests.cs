using FluentAssertions;

using PropertiesWebService.DAL;
using PropertiesWebService.DAL.Entities.Dictionaries;
using PropertiesWebService.Services.MappingProfiles;
using PropertiesWebService.Services.Services;
using PropertiesWebService.ServicesTests.TestFixtures;

namespace PropertiesWebService.ServicesTests.Services
{
    public class DictionariesServiceTests : IClassFixture<TestDbFixture>
    {
        private readonly PropertiesWebServiceDemoDbContext _dbContext;
        private readonly DictionariesService _dictionaryService;

        public DictionariesServiceTests(TestDbFixture fixture)
        {
            _dbContext = fixture.CreateContext();
            _dictionaryService = new DictionariesService(_dbContext);

            MapsterConfig.RegisterMappings();
        }

        [Fact]
        public async Task Should_Return_List_Of_PropertyTypes()
        {
            var items = await _dictionaryService.GetAsync(nameof(DictPropertyType));
            items.Should().NotBeNull();
            items.Should().HaveCountGreaterThan(1);
        }

        [Fact]
        public async Task Should_Return_List_Of_SpaceTypes()
        {
            var items = await _dictionaryService.GetAsync(nameof(DictSpaceType));
            items.Should().NotBeNull();
            items.Should().HaveCountGreaterThan(1);
        }
    }
}
