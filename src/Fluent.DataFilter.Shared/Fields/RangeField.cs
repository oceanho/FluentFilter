using System;
using System.Linq.Expressions;

namespace Fluent.DataFilter
{
    /// <summary>
    /// This is Range field
    /// </summary>
    /// <typeparam name="TField"></typeparam>
    public class RangeField<TField> : FilterField<TField>
        where TField : struct, IComparable
    {
        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        public TField? Min { get; set; }

        /// <summary>
        /// Gets or sets the max.
        /// </summary>
        /// <value>The max.</value>
        public TField? Max { get; set; }

        public override bool IsSatisfy()
        {
            return (Min != null || Max != null) ? (_IsSatisfy(Min, Max)) : false;
        }

        private static Func<TField?, TField?, bool> _IsSatisfy = (min, max) =>
        {
            if (min != null && max != null)
            {
                return min.Value.CompareTo(max.Value) < 0;
            }
            return true;
        };
    }
}
