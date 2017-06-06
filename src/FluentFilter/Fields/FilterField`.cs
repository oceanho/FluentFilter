using System;
using System.Linq.Expressions;

namespace FluentFilter
{
    public abstract class FilterField<TField> : FilterField, IFilterField<TField>
    {
    }
}
