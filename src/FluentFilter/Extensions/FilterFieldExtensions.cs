using FluentFilter.Inetnal.ImplOfFilter;
using OhPrimitives;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace FluentFilter
{
    internal static class FilterFieldExtensions
    {
        /// <summary>
        /// Check someone field is satisfy condition
        /// </summary>
        /// <param name="filterField"></param>
        /// <returns></returns>
        public static bool IsSatisfy(this IField filterField)
        {
            return filterField != null;
        }
    }
}
