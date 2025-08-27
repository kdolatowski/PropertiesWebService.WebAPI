using Mapster;

using Microsoft.EntityFrameworkCore;

using PropertiesWebService.DAL;
using PropertiesWebService.DAL.Entities.Base;
using PropertiesWebService.DAL.Entities.Dictionaries;
using PropertiesWebService.Models.Models.Base;
using PropertiesWebService.Services.Interfaces;

namespace PropertiesWebService.Services.Services
{
    public class DictionariesService(PropertiesWebServiceDemoDbContext dbContext) : IDictionariesService
    {
        public async Task<IList<DictionaryModelBase>> GetAsync(string dictionaryName, bool includeInactive = false)
        {
            switch (dictionaryName)
            {
                case nameof(DictPropertyType):
                    return await GetDictionaryAsync<DictPropertyType>(includeInactive);
                case nameof(DictSpaceType):
                    return await GetDictionaryAsync<DictSpaceType>(includeInactive);
                default:
                    throw new NotSupportedException($"Dictionary '{dictionaryName}' is not supported.");
            }
        }

        private async Task<IList<DictionaryModelBase>> GetDictionaryAsync<TEntity>(bool includeInactive) where TEntity : DictionaryBase
        {
            var query = dbContext.Set<TEntity>().AsQueryable();
            if (!includeInactive)
            {
                query = query.Where(x => x.IsActive);
            }
            var items = await query.OrderBy(x => x.Name).ToListAsync();
            return items.Adapt<IList<DictionaryModelBase>>();
        }
    }
}
