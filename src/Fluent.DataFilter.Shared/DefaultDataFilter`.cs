﻿
using System;
using System.Linq.Expressions;

namespace Fluent.DataFilter
{
#if NET
    [Serializable]
#endif
    public class DefaultDataFilter<TEntity, TFilterEntity> : IDataFilter<TFilterEntity>
        where TFilterEntity : IDataFilter<TFilterEntity>
    {
    }
}
