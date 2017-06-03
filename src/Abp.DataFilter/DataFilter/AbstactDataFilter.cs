using System;
using System.Linq.Expressions;

namespace Abp.DataFilter.DataFilter
{
	[Serializable]
    public abstract class AbstactDataFilter<TEntity> : IDataFilter<TEntity>
    {
        public virtual Expression<Func<TEntity, bool>> GetExpression()
        {
            var val = new object();
            // return Expression.Constant(val);

            return null;
        }
    }
}
