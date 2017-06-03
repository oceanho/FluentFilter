using System;
using System.Linq.Expressions;

namespace Abp.DataFilter.DataFilter
{
    public interface IFiledFilter<TFieldType>
    {
        bool IsSatisfy();

        Expression<Func<T, bool>> GetExpression<T>();
    }
}
