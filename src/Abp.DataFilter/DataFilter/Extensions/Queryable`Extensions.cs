using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abp.DataFilter.DataFilter.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> ApplyDataFilter<TEntity>(this IQueryable<TEntity> source, IDataFilter<TEntity> filter)
        {
            return source.Where(filter.ToExpression());
        }
    }
}
