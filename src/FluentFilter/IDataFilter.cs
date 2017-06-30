namespace FluentFilter
{
    public interface IDataFilter
    {
    }

    public interface IDataFilter<TFilterEntity> : IDataFilter
        where TFilterEntity : class, IDataFilter<TFilterEntity>
    {
    }
}
