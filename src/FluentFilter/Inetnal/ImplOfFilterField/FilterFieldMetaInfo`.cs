using OhPrimitives;
using System;
using System.Collections.Generic;

namespace FluentFilter.Inetnal.ImplOfFilterField
{
    internal class FilterFieldMetaInfo<TFilterField> : FilterFieldSortMetaInfo
        where TFilterField : IField
    {
        private readonly TFilterField _fieldFilterInstace;

        public FilterFieldMetaInfo(TFilterField fieldFilterInstace, string fieldExprName, IEnumerable<Attribute> fieldAttributes)
            : base(fieldFilterInstace as IHasSortField, typeof(TFilterField), fieldExprName, fieldAttributes)
        {
            _fieldFilterInstace = fieldFilterInstace;
            
        }

        public new TFilterField FilterFieldInstace
        {
            get => _fieldFilterInstace;
        }
    }
}
