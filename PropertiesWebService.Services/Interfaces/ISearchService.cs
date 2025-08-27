using PropertiesWebService.Models.Results;
using PropertiesWebService.Models.SearchCriteria;

namespace PropertiesWebService.Services.Interfaces
{
    public interface ISearchService<TModel, TSearchCriteria>
    {
        Task<PagedResult<TModel>> GetAsync(Query<TSearchCriteria> query);

        Task<TModel?> GetAsync(int id);

        Task<TModel> AddAsync(TModel model);

        Task<TModel?> UpdateAsync(int id, TModel model);

        Task<bool> DeleteAsync(int id);
    }
}
