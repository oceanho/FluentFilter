
using System;
using FluentFilter.Extensions;

namespace FluentFilter
{
    /// <summary>
    /// This is any filter field
    /// </summary>
    /// <typeparam name="TField"></typeparam>
    public class AnyField<TField> : FilterField<TField>
        where TField : struct, IComparable
    {
        public TField[] Values { get; set; }

        public override bool IsSatisfy()
        {
            return Values?.Length > 0;
        }
    }
}
