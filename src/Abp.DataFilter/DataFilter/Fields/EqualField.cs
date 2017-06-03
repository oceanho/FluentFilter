using System;
using System.Linq.Expressions;

namespace Abp.DataFilter.DataFilter.Fields
{
    public class EqualField<TFieldType> :AbstractFilterField<TFieldType>
    {
        public TFieldType Value { get; set; }

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
