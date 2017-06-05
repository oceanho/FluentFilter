
using System.Linq;
using System.Linq.Expressions;

namespace Fluent.DataFilter
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> ApplyDataFilter<TEntity>(this IQueryable<TEntity> source, IDataFilter<TEntity> filter)
        {
            var left = source.Expression;
            var right = filter.ToExpression();
#if NET
            var where = source.Where(filter.ToExpression());
#else
            var where = source.Where(filter.ToExpression().Compile());
#endif
            return source.Provider.CreateQuery<TEntity>(Expression.AndAlso(left, right));
        }
    }
}
