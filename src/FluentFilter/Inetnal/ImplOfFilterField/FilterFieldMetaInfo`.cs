using OhPrimitives;
using System;

namespace FluentFilter.Inetnal.ImplOfFilterField
{
    internal class FilterFieldMetaInfo<TFilterField> : FilterFieldSortMetaInfo
        where TFilterField : IField
    {
        private readonly TFilterField _fieldFilterInstace;

        public FilterFieldMetaInfo(TFilterField fieldFilterInstace, string fieldExprName,Type filterFieldOfElementBinderType)
            : base(fieldFilterInstace as IHasSortField, typeof(TFilterField), fieldExprName, filterFieldOfElementBinderType)
        {
            _fieldFilterInstace = fieldFilterInstace;
            
        }

        public new TFilterField FilterFieldInstace
        {
            get => _fieldFilterInstace;
        }
    }
}
