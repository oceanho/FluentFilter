namespace FluentFilter
{
#if NET
    [Serializable]
#endif
    public abstract class DataFilter<TEntity, TFilterEntity> : IDataFilter<TFilterEntity>
        where TEntity : class
        where TFilterEntity : IDataFilter<TFilterEntity>
    {
    }
}
