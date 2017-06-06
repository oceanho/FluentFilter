using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

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
