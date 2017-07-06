using OhPrimitives;
using System;
using System.Reflection;

namespace FluentFilter.Inetnal.ImplOfFilterField
{
    internal class FilterFieldMetaInfo<TFilterField> : FilterFieldSortMetaInfo
        where TFilterField : IField
    {
        private readonly TFilterField _fieldFilterInstace;

        public FilterFieldMetaInfo(TFilterField fieldFilterInstace, string fieldExprName)
            : base(fieldFilterInstace as IHasSortField, typeof(TFilterField), fieldExprName)
        {
            _fieldFilterInstace = fieldFilterInstace;
            
        }

        public new TFilterField FilterFieldInstace
        {
            get => _fieldFilterInstace;
        }
    }
}
