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
    public class SpacesService(
        ISearchCriteriaService<Space, SpaceSearchCriteria> searchCriteriaService,
        PropertiesWebServiceDemoDbContext dbContext
        ) : ISearchService<SpaceModel, SpaceSearchCriteria>
    {
        public async Task<SpaceModel> AddAsync(SpaceModel model)
        {
            var item = model.Adapt<Space>();
            dbContext.Spaces.Add(item);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
            return item.Adapt<SpaceModel>();
        }

        public Task<bool> DeleteAsync(int id)
        {
            var item = dbContext.Spaces.Find(id) ?? throw new KeyNotFoundException($"Property with id {id} not found.");
            dbContext.Spaces.Remove(item);
            return dbContext.SaveChangesAsync().ContinueWith(t => t.Result > 0);
        }

        public async Task<PagedResult<SpaceModel>> GetAsync(Query<SpaceSearchCriteria> query)
        {
            var predicate = searchCriteriaService.BuildExpression(query.SearchCriteria);
            var sortPredicate = searchCriteriaService.BuildSortExpression(query.SortMember);

            var result = dbContext.Spaces
                            .Include(d => d.Property)
                            .Where(predicate);


            var items = await result
                .SortBy(sortPredicate, query.SortDirection)
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync()
                .ConfigureAwait(false);


            var pagedResults = new PagedResult<SpaceModel>
            {
                Results = items.Adapt<List<SpaceModel>>(),
                TotalCount = await result.CountAsync().ConfigureAwait(false),
                Page = query.Page,
                PageSize = query.PageSize
            };

            return pagedResults;
        }

        public async Task<SpaceModel?> GetAsync(int id)
        {
            var item = await dbContext.Spaces
                .Include(d => d.Property)
                .FirstOrDefaultAsync(p => p.Id == id)
                .ConfigureAwait(false);

            return item?.Adapt<SpaceModel>();
        }

        public Task<SpaceModel?> UpdateAsync(int id, SpaceModel model)
        {
            throw new NotImplementedException();
        }
    }
}
