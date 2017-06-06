using System;
using System.Linq.Expressions;

namespace Fluent.DataFilter
{
    public abstract class FilterField : IFilterField
    {
        public abstract bool IsSatisfy();
    }
}
