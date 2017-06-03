using System;
using System.Linq.Expressions;

namespace Abp.DataFilter.DataFilter.Fields
{
    public class EqualField<TFieldType> :AbstractFilterField<TFieldType>
    {
        public TFieldType Field { get; set; }

        public override bool IsSatisfy()
        {
            return Field == null;
        }
        public override Expression<Func<T, bool>> GetExpression<T>()
        {
            throw new NotImplementedException();
        }
    }
}
