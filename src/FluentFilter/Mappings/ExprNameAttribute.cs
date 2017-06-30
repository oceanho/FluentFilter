using OhPrimitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentFilter.Mappings
{
    /// <summary>
    /// 表示某 DataFilter 的过滤实体对象字段的 PropertyInfo 与 <see cref="IDataFilter"/> 字段间 映射关系的 <see cref="System.Attribute"/>
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property)]
    public class ExprNameAttribute : Attribute
    {
        public ExprNameAttribute(string exprName)
        {
            ExprName = exprName;
        }

        /// <summary>
        /// 映射到过滤实体对象的字段名称（属性名）
        /// </summary>
        public string ExprName { get; set; }

        /// <summary>
        /// 排序模式（该字段优先级小于，Filter实体字段指定的 SortMode）
        /// </summary>
        public SortMode? SortMode { get; set; }

        /// <summary>
        /// 排序优先级（该字段优先级小于，Filter实体字段指定的 SortPriority）
        /// </summary>
        public int? SortPriority { get; set; }
    }
}
