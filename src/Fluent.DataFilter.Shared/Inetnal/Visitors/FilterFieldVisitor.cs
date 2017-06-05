using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Fluent.DataFilter.Inetnal.Visitors
{
    /// <summary>
    /// All visitor method should be start with visit
    /// </summary>
    internal partial class FilterFieldVisitor
    {
        public Expression Visit(IFilterField filter)
        {
            throw new NotImplementedException();
        }
    }
}
