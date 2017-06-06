using System;
using System.Linq.Expressions;

namespace FluentFilter.Inetnal.Visitors
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
