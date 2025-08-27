using LinqKit;

using PropertiesWebService.DAL.Entities;
using PropertiesWebService.Models.SearchCriteria;
using PropertiesWebService.Services.Interfaces;

using System.Linq.Expressions;

namespace PropertiesWebService.Services.Services
{
    public class SpaceSearchCriteriaService : ISearchCriteriaService<Space, SpaceSearchCriteria>
    {
        public Expression<Func<Space, bool>> BuildExpression(SpaceSearchCriteria searchCriteria)
        {
            var filter = PredicateBuilder.New<Space>(true);

            if (searchCriteria == null)
            {
                return filter;
            }

            if (searchCriteria.PropertyId.HasValue)
            {
                filter = filter.And(p => p.PropertyId == searchCriteria.PropertyId.Value);
            }

            if (searchCriteria.SpaceTypeId.HasValue)
            {
                filter = filter.And(p => p.Id == searchCriteria.SpaceTypeId.Value);
            }

            if (searchCriteria.SizeMin.HasValue)
            {
                filter = filter.And(p => p.Size >= searchCriteria.SizeMin.Value);
            }
            if (searchCriteria.SizeMax.HasValue)
            {
                filter = filter.And(p => p.Size <= searchCriteria.SizeMax.Value);
            }

            return filter;
        }

        public Expression<Func<Space, object>> BuildSortExpression(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                return p => p.PropertyId;
            }

            switch (expression)
            {
                case nameof(Space.Size):
                    return p => p.Size;
                case "SpaceTypeName":
                    return p => p.Type.Name;
                case nameof(Space.PropertyId):
                default:
                    return p => p.PropertyId;
            }
        }
    }
}
