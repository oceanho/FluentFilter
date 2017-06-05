using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fluent.DataFilter
{
    /// <summary>
    /// This is a enum for Group Filter Kind
    /// </summary>
    public enum GroupFilterKind : byte
    {
        Or = 1,
        And = 2,
        Default = Or
    }
}
