using FluentFilter.Inetnal.ImplOfFilter;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace FluentFilter
{
    public static class DataFilterExtensions
    {
        public static Expression ToExpression<TEntity>(this IDataFilter dataFilter)
        {
            return DataFilterMetaInfoParser.ToExpression<TEntity>(dataFilter);
        }
    }
}
