using FluentFilter.Inetnal.ImplOfFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FluentFilter
{
    public static class DataFilterExtensions
    {
        public static Expression ToExpression(this IDataFilter dataFilter, IQueryable querySrc)
        {
            return new DataFilterExpressionBuilder(querySrc, dataFilter).Build();
        }
        public static Expression ToExpression<TEntity>(this IDataFilter dataFilter, IQueryable<TEntity> querySrc)
        {
            return dataFilter.ToExpression(querySrc);
        }
    }
}
