using Abp.DataFilter.DataFilter.Extensions;
using System;
using System.Linq.Expressions;

namespace Abp.DataFilter.DataFilter.Fields
{
    /// <summary>
    /// This is equals field
    /// </summary>
    /// <typeparam name="TField"></typeparam>
    public struct EqualField<TField> : IFiledFilter<TField>
        where TField : IConvertible, IComparable<TField>, IEquatable<TField>
    {
        public EqualField(int value)
        {
            Value = value.As<TField>();
        }

        public TField Value { get; set; }

        public bool IsSatisfy()
        {
            return Value == null;
        }

        public static implicit operator int(EqualField<TField> value)
        {
            return value.Value.As<Int32>();
        }

        public static implicit operator long(EqualField<TField> value)
        {
            return value.Value.As<Int64>();
        }

        public static implicit operator decimal(EqualField<TField> value)
        {
            return value.Value.As<Decimal>();
        }

        public static implicit operator EqualField<TField>(int value)
        {
            return new EqualField<TField>(value);
        }
    }
}
