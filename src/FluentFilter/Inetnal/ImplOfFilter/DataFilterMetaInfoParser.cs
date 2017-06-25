
using System;
using System.Linq;
using System.Linq.Expressions;

namespace FluentFilter.Inetnal.ImplOfFilter
{
    using FluentFilter.Inetnal.ImplOfFilterField;
    using FluentFilter.Inetnal.ImplOfFilterField.Utils;

    internal class DataFilterMetaInfoParser
    {
        public static Expression<Func<TEntity, bool>> ToExpression<TEntity>(IDataFilter dataFilter)
        {
            var fieldFilters = FilterFieldMetaInfoHelper.GetFilterFields(dataFilter)
                .Where(filter => filter.FilterFieldInstace != null && filter.FilterFieldInstace.IsSatisfy()).ToArray();

            Expression<Func<TEntity, bool>> _entityAllwaysTureExpresionn = (TEntity entity) => true;
            var _body = Expression.Lambda<Func<TEntity, bool>>(_entityAllwaysTureExpresionn, Expression.Parameter(typeof(TEntity)));
            if (fieldFilters.Length == 0)
            {
                return _body;
            }

            Expression<Func<TEntity, bool>> left = (TEntity t) => true;
            Expression<Func<TEntity, bool>> right = (TEntity t) => true;

            var context = new FilterFieldVisitorContext(left, right);
            foreach (var fieldFilter in fieldFilters)
            {
                // DefaultDataFilterStaticObject.Execute(context, fieldFilter);
            }

            // throw new NotImplementedException();

            // TODO: how to wirte code in here?

            left = Expression.Lambda<Func<TEntity, bool>>(context.Left, Expression.Parameter(typeof(TEntity)));
            right = Expression.Lambda<Func<TEntity, bool>>(context.Right, Expression.Parameter(typeof(TEntity)));

            var body = Expression.MakeBinary(ExpressionType.AndAlso, _body, Expression.AndAlso(left, right));

            return Expression.Lambda<Func<TEntity, bool>>(body);
        }
    }
}
