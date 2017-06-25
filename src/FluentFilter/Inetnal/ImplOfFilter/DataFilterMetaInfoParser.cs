
using System;
using System.Linq;
using System.Linq.Expressions;

namespace FluentFilter.Inetnal.ImplOfFilter
{
    using OhPrimitives;
    using FluentFilter.Inetnal.ImplOfFilter.Utils;
    using FluentFilter.Inetnal.ImplOfFilterField.Handlers;

    internal class DataFilterMetaInfoParser
    {
        public static Expression<Func<TEntity, bool>> ToExpression<TEntity>(IGroupFilter dataFilter)
        {
            throw new NotImplementedException();
        }

        public static Expression<Func<TEntity, bool>> ToExpression<TEntity>(IDataFilter dataFilter)
        {
            var filterMetaInfo = DataFilterMetaInfoHelper.GetFilterMeatInfo(dataFilter);

            var filterFields = filterMetaInfo.FilterFieldList;

            Expression<Func<TEntity, bool>> _entityAllwaysTureExpresionn = (TEntity entity) => true;
            var _body = Expression.Lambda<Func<TEntity, bool>>(_entityAllwaysTureExpresionn, Expression.Parameter(typeof(TEntity)));
            if (filterFields.Count == 0)
            {
                return _body;
            }

            Expression<Func<TEntity, bool>> left = (TEntity t) => true;
            Expression<Func<TEntity, bool>> right = (TEntity t) => true;

            // condition
            var _body2 = (Expression)_body;
            foreach (var filterField in filterFields)
            {
                var handler = default(IFilterFieldHandler);
                if (filterField is ILikeField)
                {
                    // ILikeField - > LikeField<T>
                    handler = new LikeFilterFieldHandler();
                }
                else if (filterField is IRangeField)
                {
                    // IRangeField - > RangeField<T>
                    handler = new LikeFilterFieldHandler();
                }
                else if (filterField is IBetweenField)
                {
                    // IBetweenField - > IBetweenField<T>
                    handler = new BetweenFilterFieldHandler();
                }
                else if (filterField is ICompareField)
                {
                    // ICompareField - > CompareField<T>
                    handler = new CompareFilterFieldHandler();
                }
                else if (filterField is IFreeDomRangeField)
                {
                    // IFreeDomRangeField - > FreeDomRangeField<T>
                    handler = new FreeDomRangeFilterFieldHandler();
                }
                else
                {
                    // Custom Filter's field. There is should be get User's custom filter from some where !
                    handler = null;// new EmptyFilterFieldHandler();
                }

                if (handler == null)
                {
                    handler = new EmptyFilterFieldHandler();
                }
                _body2 = handler.Handle(_body2, filterField);
            }

            // order

            // TODO: how to wirte code in here?

            //left = Expression.Lambda<Func<TEntity, bool>>(context.Left, Expression.Parameter(typeof(TEntity)));
            //right = Expression.Lambda<Func<TEntity, bool>>(context.Right, Expression.Parameter(typeof(TEntity)));

            var body = Expression.MakeBinary(ExpressionType.AndAlso, _body, Expression.AndAlso(left, right));

            return Expression.Lambda<Func<TEntity, bool>>(body);
        }
    }
}
