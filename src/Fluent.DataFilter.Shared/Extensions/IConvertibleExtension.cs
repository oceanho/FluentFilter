using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fluent.DataFilter.Extensions
{
    public static class IConvertibleExtension
    {
        public static T As<T>(this IConvertible convertible)
        {
            return (T)Convert.ChangeType(convertible, typeof(T));
        }
    }
}
