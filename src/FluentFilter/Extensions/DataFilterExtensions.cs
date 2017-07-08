using FluentFilter.Inetnal.ExprTreeVisitors;
using System.Linq;

namespace FluentFilter
{
    public static class DataFilterExtensions
    {
        public static IQueryable<TEntity> VisitAndAccept<TEntity>(this IDataFilter dataFilter, IQueryable<TEntity> queryable)
        {
            return new FluentFilterQueryableBuilder<TEntity>(queryable,dataFilter).Build();
        }
    }
}
