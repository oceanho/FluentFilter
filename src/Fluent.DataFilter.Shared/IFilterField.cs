using System;
using System.Linq.Expressions;

namespace Fluent.DataFilter
{
    public interface IFilterField
    {
        bool IsSatisfy();
        Type FieldType { get; }
        Type FilterFieldType { get; }
    }
}
