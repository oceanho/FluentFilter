using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Fluent.DataFilter.Inetnal.ImplOfFilterField;

namespace Fluent.DataFilter.Inetnal.ImplOfFilter
{
    internal class DataFilterMetaInfo
    {
        public IDataFilter FilterType { get; }
        public IDataFilter FilterInstance { get; }
        public IReadOnlyCollection<FilterFieldMetaInfo> FilterFields { get; set; }
    }
}
