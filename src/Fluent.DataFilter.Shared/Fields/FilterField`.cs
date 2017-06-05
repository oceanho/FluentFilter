using System;
using System.Linq.Expressions;

namespace Fluent.DataFilter
{
    public abstract class FilterField<TFiled> : FilterField, IFilterField<TFiled>, IFilterField
    {
    }
}
