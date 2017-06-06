using System;
using System.Linq.Expressions;

namespace FluentFilter
{
    public interface IFilterField<TField>: IFilterField
        //where TField : struct, IComparable<TField>, IEquatable<TField>
    {
        
    }
}
