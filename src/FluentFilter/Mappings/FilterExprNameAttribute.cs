using OhPrimitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentFilter.Mappings
{
    /// <summary>
    /// 定义一个表示Filter属性作用于查询表达式的映射名称 <see cref="Attribute"/>
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property)]
    public class FilterExprNameAttribute : Attribute
    {
        /// <summary>
        /// 实例化 <see cref="FilterExprNameAttribute"/>
        /// </summary>
        /// <param name="exprName"></param>
        public FilterExprNameAttribute(string exprName)
        {
            ExprName = exprName;
        }

        /// <summary>
        /// 映射到过滤实体对象的字段名称（属性名）
        /// </summary>
        public string ExprName { get; set; }

        /// <summary>
        /// 排序模式（该字段优先级小于，Filter实体字段指定的SortMode，若Filter字段上的SortMode为Disable，则使用该属性作为排序的SortMode）
        /// </summary>
        public SortMode? SortMode { get; set; }

        /// <summary>
        /// 排序优先级（该字段优先级小于，Filter实体字段指定的SortPriority，若Filter字段上的SortPriority为空，则使用该属性作为排序的SortPriority））
        /// </summary>
        public int? SortPriority { get; set; }
    }
}
