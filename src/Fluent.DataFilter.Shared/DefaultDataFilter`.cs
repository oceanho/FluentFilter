
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using Fluent.DataFilter.Utils;
using Fluent.DataFilter.Relection;
using Fluent.DataFilter.Inetnal;

namespace Fluent.DataFilter
{
#if NET
    [Serializable]
#endif
    public class DefaultDataFilter<TEntity> : IDataFilter<TEntity>
    {
        private Type _elementType;
        private TEntity _entityOfElement;
        private Expression<Func<TEntity, bool>> _allwaysTrueExpression;

        public DefaultDataFilter()
        {
            _elementType = typeof(TEntity);
            _entityOfElement = default(TEntity);
            _allwaysTrueExpression = (TEntity entity) => true;
        }

        public virtual Type ElementType => _elementType;

        //public virtual TEntity EntityOfFilter
        //{
        //    get => _entityOfElement;
        //    set => _entityOfElement = value;
        //}

        public virtual FieldFilterInfo[] GetFieldFilters()
        {
            // TODO: This is should cache all property for every IDataFilter
            var properties = ReflectionHelper.GetProperties(this, typeof(IFilterField));

            var fieldFilterIndex = 0;
            object fieldFilterValue = null;
            var fieldFilterInfos = new FieldFilterInfo[properties.Count()];

            foreach (var property in properties)
            {
                fieldFilterValue = property.GetValue(this, null);
                fieldFilterInfos[fieldFilterIndex++] = FieldFilterInfoHelper.CreateFieldFilterInfoByType(property.PropertyType, fieldFilterValue);
            }

            return fieldFilterInfos;
        }

        public virtual Expression<Func<TEntity, bool>> ToExpression()
        {
            var fieldFilters = GetFieldFilters()
                .Where(filter => filter.FilterFieldInstace != null && filter.FilterFieldInstace.IsSatisfy()).ToArray();
            var _body = Expression.Lambda<Func<TEntity, bool>>(_allwaysTrueExpression.Body, Expression.Parameter(typeof(TEntity)));
            if (fieldFilters.Length == 0)
            {
                return _body;
            }

            Expression<Func<TEntity, bool>> left = (TEntity t) => true;
            Expression<Func<TEntity, bool>> right = (TEntity t) => true;

            var context = new FilterFieldVisitorContext(left, right);
            foreach (var fieldFilter in fieldFilters)
            {
                DefaultDataFilterStaticObject.Execute(context, fieldFilter);
            }

            throw new NotImplementedException();

            // TODO: how to wirte code in here?

            left = Expression.Lambda<Func<TEntity, bool>>(context.Left, Expression.Parameter(typeof(TEntity)));
            right = Expression.Lambda<Func<TEntity, bool>>(context.Right, Expression.Parameter(typeof(TEntity)));

            var body = Expression.MakeBinary(ExpressionType.AndAlso, _body, Expression.AndAlso(left, right));

            return Expression.Lambda<Func<TEntity, bool>>(body);
        }
    }
}
