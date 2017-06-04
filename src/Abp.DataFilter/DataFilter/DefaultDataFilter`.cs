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

        public virtual TEntity Entity
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

        public virtual Expression<Func<TEntity, bool>> ToExpression()
        {
            return _allwaysTrueExpression;
        }
    }
}
