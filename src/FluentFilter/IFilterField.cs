using System;
using System.Linq.Expressions;

namespace FluentFilter
{
    public interface IFilterField
    {
        bool IsSatisfy();
    }
}
