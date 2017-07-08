using OhPrimitives;
using System;

namespace FluentFilter.Mappings
{
    /// <summary>
    /// 禁用过滤器器的级别
    /// </summary>
    [Flags]
    public enum DisableFieldExprMode : byte
    {
        Sort = 2,
        Filter = 4,
        SortAndFilter = Sort | Filter
    }
}
