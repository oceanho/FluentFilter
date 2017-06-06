using System;

namespace FluentFilter
{

    /// <summary>
    /// This is contains field
    /// </summary>
    /// <typeparam name="TField"></typeparam>
    public class ContainsField<TField> : FilterField<TField>
        where TField : IConvertible, IComparable<TField>, IEquatable<TField>
    {
        public TField[] Values { get; set; }

        public override bool IsSatisfy()
        {
            return Values != null && Values.Length > 0;
        }
    }
}
