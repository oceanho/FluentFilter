using OhPrimitives;
using System;

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
        /// <param name="exprName"><see cref="ExprName"/></param>
        /// <param name="shouldMapExprName"><see cref="ShouldMapExprName"/></param>
        public FilterExprNameAttribute(string exprName)
            : this(exprName, true)
        {
        }

        /// <summary>
        /// 实例化 <see cref="FilterExprNameAttribute"/>
        /// </summary>
        /// <param name="exprName"><see cref="ExprName"/></param>
        /// <param name="shouldMapExprName"><see cref="ShouldMapExprName"/></param>
        public FilterExprNameAttribute()
            : this(string.Empty, false)
        {
        }

        /// <summary>
        /// 实例化 <see cref="FilterExprNameAttribute"/>
        /// </summary>
        /// <param name="exprName"><see cref="ExprName"/></param>
        /// <param name="shouldMapExprName"><see cref="ShouldMapExprName"/></param>
        public FilterExprNameAttribute(string exprName, bool shouldMapExprName)
            : this(exprName, shouldMapExprName, SortMode.Disable)
        {
        }

        /// <summary>
        /// 实例化 <see cref="FilterExprNameAttribute"/>
        /// </summary>
        /// <param name="exprName"><see cref="ExprName"/></param>
        /// <param name="shouldMapExprName"><see cref="ShouldMapExprName"/></param>
        /// <param name="sortMode"><see cref="SortMode"/></param>
        public FilterExprNameAttribute(string exprName, bool shouldMapExprName, SortMode sortMode) 
            : this(exprName, shouldMapExprName, sortMode, 0)
        {
        }

        /// <summary>
        /// 实例化 <see cref="FilterExprNameAttribute"/>
        /// </summary>
        /// <param name="exprName"><see cref="ExprName"/></param>
        /// <param name="shouldMapExprName"><see cref="ShouldMapExprName"/></param>
        /// <param name="sortMode"><see cref="SortMode"/></param>
        /// <param name="sortPriority"<see cref="SortPriority"/>
        public FilterExprNameAttribute(string exprName, bool shouldMapExprName, SortMode sortMode, int sortPriority)
        {
            ExprName = exprName;
            ShouldMapExprName = shouldMapExprName;

            SortMode = sortMode;
            SortPriority = sortPriority;

        }

        /// <summary>
        /// 获取或者设置一个值，该值表示是否映射为过滤实体对象的字段名称（属性名），默认为True。若需要禁用映射，请将此属性设为False
        /// </summary>
        public bool ShouldMapExprName { get; set; }

        /// <summary>
        /// 映射到过滤实体对象的字段名称（属性名）
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
        /// 
        /// </summary>
        public Type FilterFieldElementBindType { get; set; }
    }
}
