using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abp.DataFilter.DataFilter
{
    /// <summary>
    /// This is a enum for Group Filter Kind
    /// </summary>
    public enum GroupFilterKind : byte
    {
        Default = Or,
        And = 1,
        Or = 2
    }
}
