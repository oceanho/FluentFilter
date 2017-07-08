﻿using OhPrimitives;
using System;

namespace FluentFilter.Mappings
{
    /// <summary>
    /// 定义一个表示Filter属性作用于查询表达式的映射名称 <see cref="Attribute"/>
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property)]
    public class FilterFieldExprNameAttribute : Attribute
    {
        /// <summary>
        /// 实例化 <see cref="FilterFieldExprNameAttribute"/>
        /// </summary>
        /// <param name="exprName"></param>
        public FilterFieldExprNameAttribute(string exprName)
        {
            ExprName = exprName;
            SortMode = SortMode.Disable;
            SortPriority = 0;
        }

        /// <summary>
        /// 映射到过滤实体对象的字段名称（属性）
        /// </summary>
        public string ExprName { get; set; }

        /// <summary>
        /// 排序模式（该字段优先级小于，Filter实体字段指定的SortMode，若Filter字段上的SortMode为Disable，则使用该属性作为排序的SortMode）
        /// </summary>
        public SortMode SortMode { get; set; }

        /// <summary>
        /// 排序优先级（该字段优先级小于，Filter实体字段指定的SortPriority，若Filter字段上的SortPriority为空，则使用该属性作为排序的SortPriority））
        /// </summary>
        public int SortPriority { get; set; }

        /// <summary>
        /// 保留字段（后期可能会移除，请勿使用此字段）
        /// </summary>
        public Type FilterFieldElementBindType { get; set; }
    }
}
