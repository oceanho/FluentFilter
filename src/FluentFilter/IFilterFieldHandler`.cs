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
    /// 定义一个表示用于处理某一类型 <see cref="IField"/> 接口实现子类的Handler
    /// </summary>
    public interface IFilterFieldHandler<TField> : IFilterFieldHandler
        where TField : IField
    {
    }
}
