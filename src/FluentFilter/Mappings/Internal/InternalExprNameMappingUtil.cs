
using OhDotNetLib.Reflection;
using System.Reflection;

namespace FluentFilter.Mappings.Internal
{
    internal static class InternalExprNameMappingUtil
    {
        private static readonly MethodInfo internalCreateFilterExprNameMappingsAPI = null;
        static InternalExprNameMappingUtil()
        {
            internalCreateFilterExprNameMappingsAPI = typeof(InternalExprNameMappingUtil)
                .GetTypeInfo().GetMethod(nameof(InternalCreateFilterExprNameMappings), BindingFlags.Static | BindingFlags.NonPublic);
        }
        public static MappingInfo[] CreateFilterExprNameMappings(IDataFilter filter)
        {
            return (MappingInfo[])internalCreateFilterExprNameMappingsAPI.MakeGenericMethod(filter.GetType()).Invoke(null, null);
        }

        private static MappingInfo[] InternalCreateFilterExprNameMappings<TDataFilter>()
            where TDataFilter : class, IDataFilter
        {
            return ReflectionHelper.CreateInstance<InternalExprNameMapping<TDataFilter>>().Mapping();
        }
    }
}
