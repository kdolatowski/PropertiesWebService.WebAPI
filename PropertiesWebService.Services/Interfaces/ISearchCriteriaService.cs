using System.Linq.Expressions;

namespace PropertiesWebService.Services.Interfaces
{
    public interface ISearchCriteriaService<TEntity, TSearchCriteria>
    {
        Expression<Func<TEntity, bool>> BuildExpression(TSearchCriteria searchCriteria);

        Expression<Func<TEntity, object>> BuildSortExpression(string expression);
    }
}
