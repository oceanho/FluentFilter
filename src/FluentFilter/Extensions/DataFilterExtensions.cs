using FluentFilter.Inetnal.ExprTreeVisitors;
using FluentFilter.Inetnal.ImplOfFilter;
using System;
using System.Linq;
using System.Linq.Expressions;

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
