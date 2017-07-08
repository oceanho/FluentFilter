using System.Linq;
using System.Reflection;

namespace FluentFilter
{
    public static class QueryableExtensions
    {
        private static readonly MethodInfo _method =
            typeof(QueryableExtensions).GetTypeInfo().GetMethod(nameof(_ApplyFluentFilter), BindingFlags.Static | BindingFlags.NonPublic);
        public static IQueryable ApplyFluentFilter(this IQueryable source, IDataFilter filter)
        {
            return (IQueryable)_method.MakeGenericMethod(source.ElementType).Invoke(null, new object[] { source, filter });
        }
        public static IQueryable<TEntity> ApplyFluentFilter<TEntity>(this IQueryable<TEntity> source, IDataFilter filter)
        {
            return _ApplyFluentFilter(source, filter);
        }
        private static IQueryable<TEntity> _ApplyFluentFilter<TEntity>(IQueryable<TEntity> source, IDataFilter filter)
        {
            return filter.VisitAndAccept(source);
        }
    }
}
