using System;
using System.Linq.Expressions;

namespace Abp.DataFilter.DataFilter.Fields
{
    /// <summary>
    /// This is Between And field
    /// </summary>
    /// <typeparam name="TField"></typeparam>
    public struct BetweenAndField<TField> : IFiledFilter<TField>
        where TField : IConvertible, IComparable<TField>, IEquatable<TField>
    {

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        public TField Start { get; set; }

        /// <summary>
        /// Gets or sets the max.
        /// </summary>
        /// <value>The max.</value>
        public TField Finish { get; set; }

        public bool IsSatisfy()
        {
            return (Start != null || Finish != null) ? (_IsSatisfy(Start, Finish)) : false;
        }

        private static Func<TField, TField, bool> _IsSatisfy = (start, finish) =>
        {
            if (start != null && finish != null)
            {
                return start.CompareTo(finish) > -1;
            }
            return true;
        };
    }
}
