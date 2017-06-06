using System;
using System.Linq.Expressions;

namespace FluentFilter
{
    public abstract class FilterField : IFilterField
    {
        public abstract bool IsSatisfy();
    }
}
