using Microsoft.EntityFrameworkCore;

using PropertiesWebService.DAL;
using PropertiesWebService.DAL.Entities;
using PropertiesWebService.Models.Models;
using PropertiesWebService.Models.SearchCriteria;
using PropertiesWebService.Services.Interfaces;
using PropertiesWebService.Services.MappingProfiles;
using PropertiesWebService.Services.Services;

namespace PropertiesWebService.WebAPI.Extensions
{
    internal static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<PropertiesWebServiceDemoDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddTransient<IDictionariesService, DictionariesService>();
            services.AddTransient<ISearchCriteriaService<Property, PropertySearchCriteria>, PropertySearchCriteriaService>();
            services.AddTransient<ISearchCriteriaService<Space, SpaceSearchCriteria>, SpaceSearchCriteriaService>();
            services.AddTransient<ISearchService<PropertyModel, PropertySearchCriteria>, PropertiesService>();
            services.AddTransient<ISearchService<SpaceModel, SpaceSearchCriteria>, SpacesService>();

            MapsterConfig.RegisterMappings();

            return services;
        }
    }
}
