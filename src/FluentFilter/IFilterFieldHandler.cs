using System;
namespace FluentFilter
{
    using FluentFilter.Inetnal.ImplOfFilterField;
    using System.Linq.Expressions;

    /// <summary>
    /// 定义一个表示用于处理 <see cref="IField"/> 所需要实现的接口
    /// </summary>
    public interface IFilterFieldHandler
    {
        /// <summary>
        /// 获取一个之，该值表示用于处理某类型 <see cref="IField"/> 的 <see cref="Type"/>
        /// </summary>
        Type FilterFieldType { get; }

        /// <summary>
        /// 获取 <see cref="FilterFieldType"/> 的唯一名称，FluentFilter内部将使用此名称作为查找用于处理某个 <see cref="IField{TPrimitive}"/> 的标识
        /// </summary>
        String FilterFieldTypeUniqueName { get; }

        /// <summary>
        /// 定义一个处理某个 <see cref="IField{TPrimitive}"/> 的Handle函数
        /// </summary>
        /// <param name="node">当前Expression节点</param>
        /// <param name="metaData">字段元数据实例对象</param>
        /// <returns></returns>
        Expression Handle(Expression node, FilterFieldMetaInfo metaData);
    }
}
