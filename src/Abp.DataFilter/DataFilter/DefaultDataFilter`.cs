using System;
using System.Linq.Expressions;

namespace Abp.DataFilter.DataFilter
{
    [Serializable]
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

        public virtual TEntity Filter
        {
            get
            {
                return _entityOfElement;
            }
            set
            {
                _entityOfElement = value;
            }
        }

        public virtual IFiledFilter[] GetFieldFilters()
        {
            throw new NotImplementedException();
        }

        public virtual Expression<Func<TEntity, bool>> ToExpression()
        {
            //Expression<Func<TEntity, bool>> left = (TEntity t) => true;
            //Expression<Func<TEntity, bool>> right = (TEntity t) => true;
            //if (Min != null)
            //{
            //    left = left.Update(Expression.LessThanOrEqual(
            //        Expression.Property(Expression.Parameter(typeof(T)
            //        ));
            //}
            //if (Min != null)
            //{
            //    left = left.Update(Expression.LessThanOrEqual());
            //}
            //var body = Expression.MakeBinary(ExpressionType.AndAlso, node.Body, Expression.AndAlso(left, right));
            //node = Expression.Lambda<Func<T, bool>>(body);

            return _allwaysTrueExpression;
        }
    }
}
