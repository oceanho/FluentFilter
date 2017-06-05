using System;
using System.Linq.Expressions;

namespace Fluent.DataFilter
{
    public interface IFilterField
    {
        bool IsSatisfy();

        Type FilterFieldType { get; }
    }
}
