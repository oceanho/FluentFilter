using System;

namespace FluentFilter
{
    [Serializable]
    public abstract class DataFilter<TEntity, TFilterEntity> : IDataFilter<TFilterEntity>
        where TEntity : class
        where TFilterEntity : class,  IDataFilter<TFilterEntity>
    {
    }
}
