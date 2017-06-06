
using System;
using FluentFilter.Extensions;

namespace FluentFilter
{
    /// <summary>
    /// This is equals field
    /// </summary>
    /// <typeparam name="TField"></typeparam>
    public class CompareField<TField> : FilterField<TField>
        where TField : struct, IConvertible, IComparable
    {
        public CompareField()
            : this(default(TField))
        {
        }
        public CompareField(TField value)
            : this(value, CompareKind.Equals)
        {
        }
        public CompareField(TField value, CompareKind opeator)
        {
            Value = value;
            Opeator = opeator;
        }

        public TField? Value { get; set; }

        public CompareKind Opeator { get; set; }

        public override bool IsSatisfy()
        {
            return Value != null;
        }

        public static implicit operator int(CompareField<TField> value)
        {
            if (value == null || value.Value == null)
            {
                return 0;
            }
            return value.Value.Value.As<Int32>();
        }

        public static implicit operator long(CompareField<TField> value)
        {
            if (value == null || value.Value == null)
            {
                return 0;
            }
            return value.Value.Value.As<Int64>();
        }

        public static implicit operator decimal(CompareField<TField> value)
        {
            if (value == null || value.Value == null)
            {
                return 0;
            }
            return value.Value.Value.As<Decimal>();
        }

        public static implicit operator DateTime(CompareField<TField> value)
        {
            if (value == null || value.Value == null)
            {
                return DateTime.MinValue;
            }
            return value.Value.Value.As<DateTime>();
        }

        public static implicit operator CompareField<TField>(int value)
        {
            return new CompareField<TField>(value.As<TField>());
        }

        public static implicit operator CompareField<TField>(DateTime value)
        {
            return new CompareField<TField>(value.As<TField>());
        }
    }
}
