using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentFilter
{
    public sealed class FilterFieldData<TFiled>
    {
        internal FilterFieldData(TFiled field)
        {
            FiledInstance = field;
        }

        public TFiled FiledInstance { get; }
    }
}
