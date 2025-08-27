using LinqKit;

using Microsoft.EntityFrameworkCore;

using PropertiesWebService.DAL.Entities;
using PropertiesWebService.Models.SearchCriteria;
using PropertiesWebService.Services.Interfaces;

using System.Linq.Expressions;

namespace PropertiesWebService.Services.Services
{
    public class PropertySearchCriteriaService : ISearchCriteriaService<Property, PropertySearchCriteria>
    {
        public Expression<Func<Property, bool>> BuildExpression(PropertySearchCriteria searchCriteria)
        {
            var filter = PredicateBuilder.New<Property>(true);

            if (searchCriteria == null)
            {
                return filter;
            }

            if (searchCriteria.PropertyTypeId.HasValue)
            {
                filter = filter.And(p => p.TypeId == searchCriteria.PropertyTypeId.Value);
            }

            if (searchCriteria.PriceMin.HasValue)
            {
                filter = filter.And(p => p.Price >= searchCriteria.PriceMin.Value);
            }
            if (searchCriteria.PriceMax.HasValue)
            {
                filter = filter.And(p => p.Price <= searchCriteria.PriceMax.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchCriteria.Address))
            {
                filter = filter.And(p => EF.Functions.Like(p.Address.ToLower(), $"%{searchCriteria.Address.ToLower()}%"));
            }

            return filter;
        }

        public Expression<Func<Property, object>> BuildSortExpression(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                return p => p.Address;
            }

            switch(expression.ToLower())
            {
                case "price":
                    return p => p.Price;
                case "typeName":
                    return p => p.Type.Name;
                case "address":
                default:
                    return p => p.Address;
            }
        }
    }
}
