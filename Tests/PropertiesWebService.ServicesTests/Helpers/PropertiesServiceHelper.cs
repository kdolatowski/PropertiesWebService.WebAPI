using PropertiesWebService.DAL.Entities;
using PropertiesWebService.DAL.Entities.Dictionaries;

namespace PropertiesWebService.ServicesTests.Helpers
{
    internal static class PropertiesServiceHelper
    {
        public static List<Property> GetTestProperties(int propertiesCount, int spacesCount)
        {
            var properties = new List<Property>();
            for (int i = 0; i < propertiesCount; i++)
            {
                var property = new Property
                {
                    Address = $"Test Address {i + 1}",
                    Description = $"Test Description {i + 1}",
                    TypeId = 1,
                    Price = 1000 + i * 100,
                    Spaces = GetTestSpaces(i + 1, spacesCount)
                };
                properties.Add(property);
            }

            return properties;
        }

        public static List<Space> GetTestSpaces(int propertyId, int count)
        {
            var spaces = new List<Space>();
            for (int i = 0; i < count; i++)
            {
                spaces.Add(new Space
                {
                    Size = 100 + i * 10,
                    TypeId = 1,
                    PropertyId = propertyId,
                });
            }
            return spaces;
        }

        public static List<DictPropertyType> GetPropertyTypes()
                => new()
                {
                    new DictPropertyType { Id = 1, Name = "Apartment", IsActive = true },
                    new DictPropertyType { Id = 2, Name = "House", IsActive = true },
                    new DictPropertyType { Id = 3, Name = "Condo", IsActive = true },
                    new DictPropertyType { Id = 4, Name = "Villa", IsActive = true },
                    new DictPropertyType { Id = 5, Name = "Cottage", IsActive = true }
                };

        public static List<DictSpaceType> GetSpaceTypes()
            => new()
                {
                    new DictSpaceType { Id = 1, Name = "Living Room", IsActive = true },
                    new DictSpaceType { Id = 2, Name = "Bedroom", IsActive = true },
                    new DictSpaceType { Id = 3, Name = "Kitchen", IsActive = true },
                    new DictSpaceType { Id = 4, Name = "Office room", IsActive = true },
                    new DictSpaceType { Id = 5, Name = "Garage", IsActive = true },
                    new DictSpaceType { Id = 6, Name = "Garden", IsActive = true },
                    new DictSpaceType { Id = 7, Name = "Balcony", IsActive = true }
                };
    }
}
