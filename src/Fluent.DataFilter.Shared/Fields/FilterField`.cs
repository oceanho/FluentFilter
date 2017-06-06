using System;
using System.Linq.Expressions;

namespace Fluent.DataFilter
{
    public abstract class FilterField<TField> : FilterField, IFilterField<TField>
    {
    }
}
