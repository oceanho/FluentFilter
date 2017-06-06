
using System;
using System.Linq.Expressions;

namespace FluentFilter
{
#if NET46
    [Serializable]
#endif
    public class DefaultDataFilter<TEntity, TFilterEntity> : IDataFilter<TFilterEntity>
        where TFilterEntity : IDataFilter<TFilterEntity>
    {
    }
}
