using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OhPrimitives;
namespace FluentFilter
{
    using FluentFilter.Inetnal.ImplOfFilterField;
    using System.Linq.Expressions;

    /// <summary>
    /// 定义一个表示用于处理 <see cref="IField"/> 接口实现子类的Handler
    /// </summary>
    public interface IFilterFieldHandler
    {
        /// <summary>
        /// 获取一个之，该值表示用于处理某类型 <see cref="IField"/> 的 <see cref="Type"/>
        /// </summary>
        Type FilterType { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="metaData"></param>
        /// <returns></returns>
        Expression Handle(Expression node, FilterFieldMetaInfo metaData);
    }
}
