
using OhDotNetLib.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FluentFilter.Mappings.Internal
{
    internal static class InternalExprNameMappingFacde
    {
        private static readonly MethodInfo internalCreateFilterExprNameMappingsAPI = null;
        static InternalExprNameMappingFacde()
        {
            internalCreateFilterExprNameMappingsAPI = typeof(InternalExprNameMappingFacde)
                .GetTypeInfo().GetMethod(nameof(InternalCreateFilterExprNameMappings), BindingFlags.Static | BindingFlags.NonPublic);
        }
        public static void CreateFilterExprNameMappings(IDataFilter filter)
        {
            internalCreateFilterExprNameMappingsAPI.MakeGenericMethod(filter.GetType()).Invoke(null, null);
        }

        private static void InternalCreateFilterExprNameMappings<TDataFilter>()
            where TDataFilter : class, IDataFilter
        {
            FluentFilterManager.AddMapping(ReflectionHelper.CreateInstance<InternalExprNameMapping<TDataFilter>>());
        }
    }
}
