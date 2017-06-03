using System;
using System.Linq.Expressions;

namespace Abp.DataFilter.DataFilter.Fields
{
    public class RangeField<TFieldType> : AbstractFilterField<TFieldType>
        where TFieldType : IConvertible
    {

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        public TFieldType Min { get; set; }

        /// <summary>
        /// Gets or sets the max.
        /// </summary>
        /// <value>The max.</value>
        public TFieldType Max { get; set; }

        public override bool IsSatisfy()
        {
            return Min != null || Max != null;
        }
        public override Expression<Func<T, bool>> GetExpression<T>()
        {
            throw new NotImplementedException();
        }
    }
}
