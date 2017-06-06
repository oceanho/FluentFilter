using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentFilter.Inetnal.ImplOfFilterField;

namespace FluentFilter.Inetnal.ImplOfFilter
{
    internal class DataFilterMetaInfo
    {
        public IDataFilter FilterType { get; }
        public IDataFilter FilterInstance { get; }
        public IReadOnlyCollection<FilterFieldMetaInfo> FilterFields { get; set; }
    }
}
