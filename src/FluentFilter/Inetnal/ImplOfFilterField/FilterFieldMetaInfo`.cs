using System;
using System.Reflection;

namespace FluentFilter.Inetnal.ImplOfFilterField
{
    internal class FilterFieldMetaInfo<TFilterField> : FilterFieldMetaInfo
        where TFilterField : IFilterField
    {
        private Type _fieldType;
        private Type _fieldFilterType;
        private TFilterField _fieldFilterInstace;

        public FilterFieldMetaInfo(TFilterField fieldFilterInstace)
        {
            _fieldFilterType = typeof(TFilterField);
            _fieldFilterInstace = fieldFilterInstace;
            if (_fieldFilterType.GetTypeInfo().IsGenericType)
            {
                _fieldType = _fieldFilterType.GetTypeInfo().GetGenericArguments()[0];
            }
            if (_fieldType == null)
            {
                throw new ArgumentException($"{nameof(fieldFilterInstace)} Should be an genericType.");
            }
        }

        public TFilterField Instance
        {
            get => _fieldFilterInstace;
            set => _fieldFilterInstace = value;
        }

        public override Type FilterFieldType
        {
            get => _fieldFilterType;
        }

        public override IFilterField FilterFieldInstace
        {
            get => Instance;
        }

        public override Type FieldType => _fieldType;
    }
}
