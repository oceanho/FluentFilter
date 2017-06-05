using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Fluent.DataFilter.Inetnal.Visitors
{
    internal partial class FilterFieldVisitor
    {
        public Expression VisitCompareField<TField>(Expression node, CompareField<TField> filter)
            where TField : struct, IConvertible, IComparable
        {
            var whereExpression = GetInnerMostWhere(node);            
            return Expression.Constant(true);
        }
    }
}
