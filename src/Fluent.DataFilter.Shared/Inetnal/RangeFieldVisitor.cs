﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Fluent.DataFilter.Inetnal
{
    internal partial class FilterFieldVisitor
    {
        public void VisitRangeField<TField>(RangeField<TField> filter)
            where TField : struct, IComparable
        {
        }
    }
}
