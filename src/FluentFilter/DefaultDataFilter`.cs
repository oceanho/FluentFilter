
using System;

namespace FluentFilter
{
#if NET46
    [Serializable]
#endif
    public class DefaultDataFilter<TEntity, TFilterEntity> : DataFilter<TEntity, TFilterEntity>, IDataFilter<TFilterEntity>
        where TEntity : class, new()
        where TFilterEntity : IDataFilter<TFilterEntity>
    {
    }
}
