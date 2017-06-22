namespace FluentFilter
{
    public class DefaultGroupFilter<TEntity, TFilterEntity, TGroupFilterEntity> : DefaultDataFilter<TEntity, TFilterEntity>, IGroupFilter<TFilterEntity, TGroupFilterEntity>
        where TEntity : class, new()
        where TFilterEntity : IDataFilter<TFilterEntity>
        where TGroupFilterEntity : IGroupFilter<TFilterEntity, TGroupFilterEntity>

    {
        public TGroupFilterEntity Left { get; set; }
        public GroupFilterOption Option { get; set; }
        public TGroupFilterEntity Right { get; set; }
    }
}
