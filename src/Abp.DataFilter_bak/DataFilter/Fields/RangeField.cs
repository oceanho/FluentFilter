using System;
using System.Linq.Expressions;

namespace Fluent.DataFilter.Fields
{
    /// <summary>
    /// This is Range field
    /// </summary>
    /// <typeparam name="TField"></typeparam>
    public struct RangeField<TField> : IFiledFilter<TField>
        where TField : IConvertible, IComparable<TField>, IEquatable<TField>
    {

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        public TField Min { get; set; }

        /// <summary>
        /// Gets or sets the max.
        /// </summary>
        /// <value>The max.</value>
        public TField Max { get; set; }

        public bool IsSatisfy()
        {
            return (Min != null || Max != null) ? (_IsSatisfy(Min, Max)) : false;
        }

        private static Func<TField, TField, bool> _IsSatisfy = (min, max) =>
        {
            if (min != null && max != null)
            {
                return min.CompareTo(max) > -1;
            }
            return true;
        };
    }
}
