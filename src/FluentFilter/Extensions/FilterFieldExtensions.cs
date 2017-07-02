using FluentFilter.Inetnal.ImplOfFilter;
using OhPrimitives;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentFilter
{
    internal static class FilterFieldExtensions
    {
        /// <summary>
        /// 判断某个字段是否满足过滤条件，提前筛选出不满足条件的过滤字段，可以减少不必要的表达式构建，提升性能
        /// </summary>
        /// <param name="filterField"></param>
        /// <returns></returns>
        public static bool IsSatisfy(this IField filterField)
        {
            return filterField != null;
        }
    }
}
