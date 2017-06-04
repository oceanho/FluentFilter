using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Abp.DataFilter.DataFilter.Fields
{

    /// <summary>
    /// This is contains field
    /// </summary>
    /// <typeparam name="TField"></typeparam>
    public struct ContainsField<TField> : IFiledFilter<TField>
        where TField : IConvertible, IComparable<TField>, IEquatable<TField>
    {
        public TField[] Values { get; set; }

        public bool IsSatisfy()
        {
            return Values != null && Values.Length > 0;
        }
    }
}
