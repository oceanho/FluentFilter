using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FluentFilter.Mappings
{

    /// <summary>
    /// 定义一个表示 <see cref="DataFilter{TEntity, TFilterEntity}"/> 属性映射到 <see cref="System.Linq.Expressions.Expression"/> 上参数的Mapping类
    /// </summary>
    [Serializable]
    public class MappingInfo
    {
        /// <summary>
        /// 获取或者设置绑定到 <see cref="System.Linq.Expressions.Expression"/> 上的参数名称
        /// </summary>
        public string ExprName { get; set; }

        internal FilterExprNameAttribute ExprNameAttribute { get; set; }

        /// <summary>
        /// 获取或者设置绑定到 <see cref="System.Linq.Expressions.Expression"/> 上的参数属性对应的 <see cref="PropertyInfo"/>
        /// </summary>
        public PropertyInfo Property { get; set; }

        /// <summary>
        /// 获取或者设置绑定到 <see cref="System.Linq.Expressions.Expression"/> 上的参数属性提供者的类型
        /// </summary>
        public Type FilterFieldElementBindType { get; set; }
    }
}
