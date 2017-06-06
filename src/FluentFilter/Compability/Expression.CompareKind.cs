using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentFilter
{
    public enum CompareKind : byte
    {
        Equals = 1,
        LessThan = 2,
        LessOrEqualsThan = 3,
        GreaterThan = 4,
        GreaterOrEqualsThan = 5
    }
}
