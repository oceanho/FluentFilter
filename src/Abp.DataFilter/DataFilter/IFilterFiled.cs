using System;
using System.Linq.Expressions;

namespace Abp.DataFilter.DataFilter
{
    public interface IFiledFilter<TField>
    {
        bool IsSatisfy();

        Expression<Func<T, bool>> GetExpression<T>();
    }
}
