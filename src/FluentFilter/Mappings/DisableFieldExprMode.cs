using OhPrimitives;
using System;

namespace FluentFilter.Mappings
{
    /// <summary>
    /// 禁用过滤器字段（过滤、排序）的模式
    /// </summary>
    [Flags]
    public enum DisableFieldExprMode : byte
    {
        /// <summary>
        /// 禁用排序
        /// </summary>
        Sort = 2,

        /// <summary>
        /// 禁用过滤
        /// </summary>
        Filter = 4,

        /// <summary>
        /// 禁用排序+过滤
        /// </summary>
        SortAndFilter = Sort | Filter
    }
}
