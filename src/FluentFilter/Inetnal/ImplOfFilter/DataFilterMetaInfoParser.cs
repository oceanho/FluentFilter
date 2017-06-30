
using System;
using System.Linq;
using System.Linq.Expressions;

namespace FluentFilter.Inetnal.ImplOfFilter
{
    using OhPrimitives;
    using FluentFilter.Inetnal.ImplOfFilter.Utils;
    using FluentFilter.Inetnal.ImplOfFilterField.Handlers;
    using FluentFilter.Inetnal.ImplOfFilterField;
    using OhDotNetLib.Linq;

    internal class DataFilterMetaInfoParser
    {
        public static Expression<Func<TEntity, bool>> ToExpression<TEntity>(IGroupFilter dataFilter)
        {
            throw new NotImplementedException();
        }

        public static Expression<Func<TEntity, bool>> ToExpression<TEntity>(IDataFilter dataFilter)
        {
            var filterInfo = DataFilterMetaInfoHelper.GetFilterMeatInfo(dataFilter);

            var filterFields = filterInfo.FilterFieldList;

            var predicateBody = Expression.Constant(true, typeof(bool));

            var parameter = PredicateBuilder.Paramters<TEntity>();
            var lambda = Expression.Lambda<Func<TEntity, bool>>(predicateBody, parameter);
            if (filterFields.Count == 0)
            {
                return lambda;
            }

            // filter
            var body = (Expression)lambda;
            foreach (var filterField in filterFields)
            {
                var handler = FilterFieldHandlerFactory.GetHandler(filterField.FilterFieldType);
                if (handler == null)
                {
                    handler = new EmptyFilterFieldHandler();
                }
                body = handler.Handle(body, filterField, false);
            }

            // sort

            return body as Expression<Func<TEntity, bool>>;
        }
    }
}
