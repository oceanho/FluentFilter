
using System;

namespace FluentFilter
{
    [Serializable]
    public class DefaultDataFilter<TEntity, TFilterEntity> : DataFilter<TEntity, TFilterEntity>, IDataFilter<TFilterEntity>
        where TEntity : class, new()
        where TFilterEntity : class, IDataFilter<TFilterEntity>
    {
    }
}
