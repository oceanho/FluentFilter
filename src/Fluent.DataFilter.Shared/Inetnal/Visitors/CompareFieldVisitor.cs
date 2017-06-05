using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Fluent.DataFilter.Inetnal.Visitors
{
    internal partial class FilterFieldVisitor
    {
        public Expression VisitCompareField<TField>(CompareField<TField> filter)
            where TField : struct, IConvertible, IComparable
        {
            return Expression.Constant(true);
        }
    }
}
