using System;
using System.Linq.Expressions;

namespace Fluent.DataFilter
{
    public interface IFilterField
    {
        bool IsSatisfy();

        /// <summary>
        /// Get an type of field
        /// </summary>
        Type FieldType { get; }

        /// <summary>
        /// Get an type of filter field element
        /// </summary>
        Type FilterFieldType { get; }
    }
}
