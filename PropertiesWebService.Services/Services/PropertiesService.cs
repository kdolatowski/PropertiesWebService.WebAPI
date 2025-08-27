using Mapster;

using Microsoft.EntityFrameworkCore;

using PropertiesWebService.DAL;
using PropertiesWebService.DAL.Entities;
using PropertiesWebService.Models.Models;
using PropertiesWebService.Models.Results;
using PropertiesWebService.Models.SearchCriteria;
using PropertiesWebService.Services.Helpers;
using PropertiesWebService.Services.Interfaces;

namespace PropertiesWebService.Services.Services
{
    public class PropertiesService(
            ISearchCriteriaService<Property, PropertySearchCriteria> searchCriteriaService,
            PropertiesWebServiceDemoDbContext dbContext
        ) : ISearchService<PropertyModel, PropertySearchCriteria>
    {

        public async Task<PropertyModel> AddAsync(PropertyModel model)
        {
            var item = model.Adapt<Property>();
            dbContext.Properties.Add(item);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
            return item.Adapt<PropertyModel>();
        }

        public async Task<PagedResult<PropertyModel>> GetAsync(Query<PropertySearchCriteria> query)
        {
            try
            {
                var pagedResults = new PagedResult<PropertyModel>();

                var predicate = searchCriteriaService.BuildExpression(query.SearchCriteria);
                var sortPredicate = searchCriteriaService.BuildSortExpression(query.SortMember);

                var result = dbContext.Properties
                                .Include(d => d.Spaces)
                                .Where(predicate);


                var items = await result
                    .SortBy(sortPredicate, query.SortDirection)
                    .Skip((query.Page - 1) * query.PageSize)
                    .Take(query.PageSize)
                    .ToListAsync()
                    .ConfigureAwait(false);


                pagedResults = new PagedResult<PropertyModel>
                {
                    Results = items.Adapt<List<PropertyModel>>(),
                    TotalCount = await result.CountAsync().ConfigureAwait(false),
                    Page = query.Page,
                    PageSize = query.PageSize
                };

                return pagedResults;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving properties.", ex);
            }
        }

        public async Task<PropertyModel?> GetAsync(int id)
        {
            var item = await dbContext.Properties
                            .Include(d => d.Spaces)
                            .FirstOrDefaultAsync(p => p.Id == id)
                            .ConfigureAwait(false);

            return item?.Adapt<PropertyModel>();
        }

        public Task<PropertyModel?> UpdateAsync(int id, PropertyModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            var item = dbContext.Properties.Find(id) ?? throw new KeyNotFoundException($"Property with id {id} not found.");
            dbContext.Properties.Remove(item);
            return dbContext.SaveChangesAsync().ContinueWith(t => t.Result > 0);
        }
    }
}
