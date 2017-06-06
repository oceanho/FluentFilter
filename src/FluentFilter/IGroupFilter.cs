namespace FluentFilter
{
    public interface IGroupFilter<TFilterEntity, TGroupFilter> : IDataFilter<TFilterEntity>, IDataFilter
        where TFilterEntity : IDataFilter<TFilterEntity>
        where TGroupFilter : IGroupFilter<TFilterEntity, TGroupFilter>
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
        GroupFilterKind Kind { get; set; }
    }
}
