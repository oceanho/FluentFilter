using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluent.DataFilter
{
    public abstract class FieldFilterInfo
    {
        public abstract Type FilterFieldType { get; set; }

        public abstract IFilterField FilterFieldInstace { get; }
    }
}
