using OhPrimitives;
using System;
using System.Reflection;

namespace FluentFilter.Inetnal.ImplOfFilterField
{
    internal class FilterFieldSortMetaInfo : FilterFieldMetaInfo
    {
        private readonly Type _fieldPrimitiveType;
        private readonly Type _fieldFilterFieldType;
        private readonly IHasSortField _fieldFilterInstace;

        public FilterFieldSortMetaInfo(IHasSortField fieldFilterInstace, Type fieldFilterFieldType, string fieldExprName)
            : base(fieldFilterInstace, fieldExprName)
        {
            _fieldFilterInstace = fieldFilterInstace;
            _fieldFilterFieldType = fieldFilterFieldType;
            if (_fieldFilterFieldType.GetTypeInfo().IsGenericType)
            {
                _fieldPrimitiveType = _fieldFilterFieldType.GetTypeInfo().GetGenericArguments()[0];
            }
            _fieldPrimitiveType = (_fieldFilterFieldType == typeof(LikeField)) ? typeof(String) : _fieldPrimitiveType;
            if (_fieldPrimitiveType == null)
            {
                throw new ArgumentException($"{nameof(fieldFilterInstace)} Should be an genericType Or LikeField.");
            }
        }

        public new IHasSortField FilterFieldInstace
        {
            get => _fieldFilterInstace;
        }

        public override Type PrimitiveType => _fieldPrimitiveType;

        public override Type FilterFieldType => _fieldFilterFieldType;
    }
}
