using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentFilter.Mappings
{
    /// <summary>
    /// 定义一个表示某个 DataFilter 属性过滤表达式的 Property Name 与 <see cref="OhPrimitives.IField"/> 的映射关系的 Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ExprNameAttribute : Attribute
    {
        public string ExprName { get; set; }
    }
}
