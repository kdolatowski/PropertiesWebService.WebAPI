using Mapster;

using PropertiesWebService.DAL.Entities;
using PropertiesWebService.Models.Models;

using System.Reflection;

namespace PropertiesWebService.Services.MappingProfiles
{
    public static class MapsterConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<Space, SpaceModel>
                .NewConfig()
                .Map(dst => dst.SpaceType, src => src.Type.Name);

            TypeAdapterConfig<Property, PropertyModel>
                .NewConfig()
                .Map(dst => dst.TypeName, src => src.Type.Name)
                .Map(dst => dst.Spaces, src => src.Spaces.Adapt<List<SpaceModel>>());

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

        }
    }
}
