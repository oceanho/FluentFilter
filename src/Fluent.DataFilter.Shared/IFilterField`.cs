using System;
using System.Linq.Expressions;

namespace Fluent.DataFilter
{
    public interface IFilterField<TField>: IFilterField
        //where TField : struct, IComparable<TField>, IEquatable<TField>
    {
        
    }
}
