namespace FluentFilter
{
    public class DefaultGroupFilter<TEntity, TFilterEntity, TGroupFilterEntity> : DefaultDataFilter<TEntity, TFilterEntity>, IGroupFilter<TFilterEntity, TGroupFilterEntity>
        where TEntity : class, new()
        where TFilterEntity : class, IDataFilter<TFilterEntity>
        where TGroupFilterEntity : class, IGroupFilter<TFilterEntity, TGroupFilterEntity>

    {
        public TGroupFilterEntity Left { get; set; }
        public GroupFilterOption Option { get; set; }
        public TGroupFilterEntity Right { get; set; }
    }
}
