
using System;
using System.Linq;
using System.Linq.Expressions;

using OhDotNetLib.Extension;

namespace FluentFilter.Inetnal.ImplOfFilter
{
    using OhDotNetLib.Linq;
    using OhDotNetLib.Utils;

    using FluentFilter.Inetnal.ImplOfFilterField;
    using FluentFilter.Inetnal.ImplOfFilter.Utils;

    internal class DataFilterMetaInfoParser
    {
        public static Expression<Func<TEntity, bool>> Parse<TEntity>(IGroupFilter dataFilter)
        {
            throw new NotImplementedException();
        }

        public static Expression<Func<TEntity, bool>> Parse<TEntity>(IDataFilter dataFilter, string paramName)
        {
            return Parse<TEntity>(dataFilter, DataFilterMetaInfoHelper.GetFilterMeatInfo(dataFilter), paramName);
        }

        public static Expression<Func<TEntity, bool>> Parse<TEntity>(IDataFilter dataFilter, DataFilterMetaInfo filterInfo, string paramName)
        {
            var body = Expression.Constant(true, typeof(bool));
            var parameter = paramName.IsEmpty() ? PredicateBuilder.Paramter<TEntity>() : PredicateBuilder.Paramter<TEntity>(paramName);
            var lambdaExpr = Expression.Lambda<Func<TEntity, bool>>(body, parameter);
            return Parse<TEntity>(dataFilter, filterInfo, lambdaExpr);
        }

        public static Expression<Func<TEntity, bool>> Parse<TEntity>(IDataFilter dataFilter, DataFilterMetaInfo filterInfo, Expression node)
        {
            ObjectChecker.CheckNotNull(filterInfo);
            var filterFields = filterInfo.FilterFields;
            if (filterFields.Count == 0)
            {
                return (Expression<Func<TEntity, bool>>)node;
            }

            // filter
            var body = node;// (Expression)lambda;
            foreach (var filterField in filterFields)
            {
                var handler = FilterFieldHandlerFactory.GetHandler(filterField.FilterFieldType);
                if (handler != null)
                {
                    // call FilterFieldHandler
                    body = handler.Handle(body, filterField);
                }
            }
            return body as Expression<Func<TEntity, bool>>;
        }
    }
}
