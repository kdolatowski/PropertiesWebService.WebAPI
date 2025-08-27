using System.ComponentModel;
using System.Linq.Expressions;
using PropertiesWebService.Services.Helpers;

namespace PropertiesWebService.Services.Helpers
{
    internal static class SortExpressionParser
    {
        public static IOrderedQueryable<TEntity> SortBy<TEntity>(this IQueryable<TEntity> source, Expression<Func<TEntity, object>> order, ListSortDirection sortDirection)
        {
            return source.OrderingHelper(order, sortDirection);
        }

        private static IOrderedQueryable<T> OrderingHelper<T>(this IQueryable<T> source, Expression<Func<T, object>> keySelector, ListSortDirection sortDirection)
        {
            Expression keySelectorBody = keySelector.Body;

            while ((keySelectorBody.NodeType == ExpressionType.Convert || keySelectorBody.NodeType == ExpressionType.ConvertChecked) && keySelectorBody.Type == typeof(object))
            {
                keySelectorBody = ((UnaryExpression)keySelectorBody).Operand;
            }

            var sortExpression = Expression.Lambda(keySelectorBody, keySelector.Parameters);

            var call = Expression.Call(
                typeof(Queryable),
                "OrderBy" + (sortDirection == ListSortDirection.Descending ? "Descending" : string.Empty),
                [typeof(T), sortExpression.ReturnType],
                source.Expression,
                Expression.Quote(sortExpression)
            );

            return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
        }
    }
}
