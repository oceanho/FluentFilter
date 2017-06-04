using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abp.DataFilter.DataFilter.Fields
{

    /// <summary>
    /// This is contains field
    /// </summary>
    /// <typeparam name="TField"></typeparam>
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
