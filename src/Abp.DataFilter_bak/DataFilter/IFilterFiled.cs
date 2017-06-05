using System;
using System.Linq.Expressions;

namespace Fluent.DataFilter
{
    public interface IFiledFilter
    {
        bool IsSatisfy();
    }
}
