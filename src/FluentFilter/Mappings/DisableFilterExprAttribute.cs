using OhPrimitives;
using System;

namespace FluentFilter.Mappings
{
    /// <summary>
    /// 定义一个表示禁用Filter表达式映射的Attribute
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property)]
    public class DisableFilterExprAttribute : Attribute
    {
    }
}
