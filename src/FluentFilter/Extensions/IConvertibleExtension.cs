using System;

namespace FluentFilter.Extensions
{
    public static class IConvertibleExtension
    {
        public static T As<T>(this IConvertible convertible)
        {
            return (T)Convert.ChangeType(convertible, typeof(T));
        }
    }
}
