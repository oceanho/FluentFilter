namespace FluentFilter
{
    public interface IGroupFilter
    {

    }

    public interface IGroupFilter<TFilterEntity, TGroupFilter> : IGroupFilter, IDataFilter<TFilterEntity>, IDataFilter
        where TFilterEntity : class, IDataFilter<TFilterEntity>
        where TGroupFilter : class, IGroupFilter<TFilterEntity, TGroupFilter>
    {
        /// <summary>
        /// This is left group filter
        /// </summary>
        TGroupFilter Left { get; set; }

        /// <summary>
        /// This is right group filter
        /// </summary>
        TGroupFilter Right { get; set; }

        /// <summary>
        /// This is a operation tag to concat Left and Right filter
        /// </summary>
        GroupFilterOption Option { get; set; }
    }
}
