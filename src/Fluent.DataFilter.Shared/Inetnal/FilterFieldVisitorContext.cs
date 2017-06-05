using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Fluent.DataFilter.Inetnal
{
    internal class FilterFieldVisitorContext
    {
        public FilterFieldVisitorContext(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }
        public Expression Left { get; set; }
        public Expression Right { get; set; }
    }
}
