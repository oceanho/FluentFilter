using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abp.DataFilter.DataFilter.Fields
{
    public class ContainsField<TField> : AbstractFilterField<TField>
        where TField : IConvertible
    {
        public TField[] Values { get; set; }

        public override bool IsSatisfy()
        {
            return Values != null && Values.Length > 0;
        }
        public override Expression<Func<T, bool>> GetExpression<T>()
        {
            throw new NotImplementedException();
        }
    }
}
