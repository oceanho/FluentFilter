using System;
using System.Collections.Generic;
using FluentFilter.Inetnal.ImplOfFilterField;

namespace FluentFilter.Inetnal.ImplOfFilter
{
    internal class DataFilterMetaInfo
    {
        public Type FilterType { get; }
        public IDataFilter FilterInstance { get; }
        public IReadOnlyCollection<FilterFieldMetaInfo> FilterFields { get; set; }
    }
}
