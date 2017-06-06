
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentFilter
{
    public static class QueryableExtensions
    {
        private static readonly MethodInfo _method =
            typeof(QueryableExtensions).GetTypeInfo().GetMethod(nameof(_ApplyDataFilter), BindingFlags.Static | BindingFlags.NonPublic);
        public static IQueryable ApplyDataFilter(this IQueryable source, IDataFilter filter)
        {
            var element = source.ElementType;
            return (IQueryable)_method.MakeGenericMethod(source.ElementType).Invoke(null, new object[] { source, filter });
        }
        public static IQueryable<TEntity> ApplyDataFilter<TEntity>(this IQueryable<TEntity> source, IDataFilter filter)
        {
            return _ApplyDataFilter(source, filter);
        }
        private static IQueryable<TEntity> _ApplyDataFilter<TEntity>(IQueryable<TEntity> source, IDataFilter filter)
        {
            var left = source.Expression;
            var right = filter.ToExpression(source);
            return source;
//#if NET
//            var where = source.Where(filter.ToExpression(source));
//#else
//            var where = source.Where(filter.ToExpression());
//#endif
//            return source.Provider.CreateQuery<TEntity>(Expression.AndAlso(left, right));
        }
    }
}
