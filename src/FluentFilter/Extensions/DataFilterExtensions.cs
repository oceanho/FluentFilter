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
            // TODO: how to implement ?
            throw new NotImplementedException();
            // return dataFilter.ToExpressionFromQuery(null);
        }
        public static Expression ToExpressionFromQuery(this IDataFilter dataFilter, IQueryable query)
        {
            return new DataFilterExpressionEvaluator(query, dataFilter).Eval();
        }
    }
}
