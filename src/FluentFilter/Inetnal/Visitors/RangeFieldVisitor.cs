using System;
using System.Linq.Expressions;

namespace FluentFilter.Inetnal.Visitors
{
    internal partial class FilterFieldVisitor
    {
        public void VisitRangeField<TField>(Expression node, RangeField<TField> filter)
            where TField : struct, IComparable
        {
        }
    }
}
