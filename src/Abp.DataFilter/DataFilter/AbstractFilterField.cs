using System;
using System.Linq.Expressions;

namespace Abp.DataFilter.DataFilter
{
    public abstract class AbstractFilterField <TFieldType> : IFiledFilter<TFieldType>
    {
        public abstract Expression<Func<T, bool>> GetExpression<T>();

        public virtual bool IsSatisfy()
        {
            return false;
        }
    }
}
