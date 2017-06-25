﻿using System;
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
        Expression Handle(Expression node, FilterFieldMetaInfo metaData);
    }
}
