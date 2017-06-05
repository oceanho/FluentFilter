using System;
using System.Linq.Expressions;

namespace Fluent.DataFilter
{
    public abstract class FilterField<TFiled> : FilterField, IFilterField<TFiled>, IFilterField
    {
        private Type _fieldType = typeof(TFiled);
        public override Type FieldType => _fieldType;
    }
}
