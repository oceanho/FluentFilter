using OhPrimitives;
using System;
using System.Reflection;

namespace FluentFilter.Inetnal.ImplOfFilterField
{
    internal class FilterFieldSortMetaInfo : FilterFieldMetaInfo
    {
        private Type _fieldType;
        private Type _fieldFilterType;
        private IHasSortField _fieldFilterInstace;

        public FilterFieldSortMetaInfo(IHasSortField fieldFilterInstace)
            : base(fieldFilterInstace)
        {
            _fieldFilterType = fieldFilterInstace.GetType();
            if (_fieldFilterType.GetTypeInfo().IsGenericType)
            {
                _fieldType = _fieldFilterType.GetTypeInfo().GetGenericArguments()[0];
            }
            if (_fieldType == null)
            {
                throw new ArgumentException($"{nameof(fieldFilterInstace)} Should be an genericType.");
            }
            _fieldFilterInstace = base.FilterFieldInstace as IHasSortField;
        }

        public new IHasSortField FilterFieldInstace
        {
            get => _fieldFilterInstace;
            set => _fieldFilterInstace = value;
        }

        public override Type PrimitiveType => _fieldType;
        public override Type FilterFieldType => _fieldFilterType;
    }
}
