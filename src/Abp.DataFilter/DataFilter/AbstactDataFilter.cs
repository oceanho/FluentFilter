using System;
using System.Linq.Expressions;

namespace Abp.DataFilter.DataFilter
{
	[Serializable]
    public abstract class AbstactDataFilter<TEntity> : IDataFilter<TEntity>
    {
        private Type _elementType;

        public AbstactDataFilter()
        {
            _elementType = typeof(TEntity);
        }
                
        public virtual Type ElementType => _elementType;

        public virtual TEntity Entity { get; set; }

        public virtual Expression<Func<TEntity, bool>> GetExpression()
        {
            var val = new object();
            // return Expression.Constant(val);
            // LambdaExpression
            return null;
        }
    }
}
