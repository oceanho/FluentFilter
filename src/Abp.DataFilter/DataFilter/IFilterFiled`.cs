using System;
using System.Linq.Expressions;

namespace Abp.DataFilter.DataFilter
{
    public interface IFiledFilter<TField>: IFiledFilter
    {
        bool IsSatisfy();

        void Visit<T>(Expression<Func<T, bool>> whereExpression);
    }
}
