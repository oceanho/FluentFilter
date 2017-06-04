using System;
using System.Linq.Expressions;

namespace Abp.DataFilter.DataFilter
{
    public abstract class AbstractFilterField<TField> : IFiledFilter<TField>
    {
        public abstract Expression<Func<T, bool>> GetExpression<T>();

        public virtual bool IsSatisfy()
        {
            return false;
        }

        public void Visit<T>(Expression<Func<T, bool>> whereExpression)
        {
            // Todo: here code should be fixed,current is test only
            whereExpression = Expression.Lambda<Func<T, bool>>(whereExpression.Body);
        }
    }
}
