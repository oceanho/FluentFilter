using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abp.DataFilter.DataFilter
{
    public enum GroupFilterFlag
    {
        Default = Or,
        And = 1,
        Or = 2
    }
}
