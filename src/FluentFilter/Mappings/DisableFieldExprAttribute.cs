using OhPrimitives;
using System;

namespace FluentFilter.Mappings
{
    /// <summary>
    /// 定义一个表示禁用Filter字段到表达式的映射Attribute
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DisableFieldExprAttribute : Attribute
    {
        /// <summary>
        /// 实例化全禁用模式的 <see cref="DisableFieldExprAttribute"/>
        /// </summary>
        public DisableFieldExprAttribute()
            : this(DisableFieldExprMode.SortAndFilter)
        {
        }

        /// <summary>
        /// 实例化指定模式的 <see cref="DisableFieldExprAttribute"/>
        /// </summary>
        /// <param name="mode"></param>
        public DisableFieldExprAttribute(DisableFieldExprMode mode)
        {
            DisabledMode = mode;
        }

        /// <summary>
        /// 获取或者设置一个值，该值表示禁用模式
        /// </summary>
        public DisableFieldExprMode DisabledMode { get; set; }
    }
}
