namespace FluentFilter
{
    public class DefaultGroupFilter<TEntity, TFilterEntity, TGroupFilterEntity> : DefaultDataFilter<TEntity, TFilterEntity>, IGroupFilter<TFilterEntity, TGroupFilterEntity>
        where TEntity : class, new()
        where TFilterEntity : IDataFilter<TFilterEntity>
        where TGroupFilterEntity : IGroupFilter<TFilterEntity, TGroupFilterEntity>

    {
        public GroupFilterKind Kind { get; set; }
        public TGroupFilterEntity Left { get; set; }
        public TGroupFilterEntity Right { get; set; }
    }
}
