using System;
using System.Linq.Expressions;

namespace Abp.DataFilter.DataFilter.Fields
{
    /// <summary>
    /// This is equals field
    /// </summary>
    /// <typeparam name="TField"></typeparam>
    public class EqualField<TField> :AbstractFilterField<TField>
    {
        public TField Value { get; set; }

        public override bool IsSatisfy()
        {
            return Value == null;
        }
        public override Expression<Func<T, bool>> GetExpression<T>()
        {
            throw new NotImplementedException();
        }
    }
}
