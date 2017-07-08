using FluentFilter.Inetnal.ImplOfFilterField;
using FluentFilter.Mappings;
using OhDotNetLib.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FluentFilter
{
    /// <summary>
    /// FluentFilterManager
    /// </summary>
    public static class FluentFilterManager
    {
        /// <summary>
        /// Add 一个自定义 Mapping
        /// </summary>
        /// <typeparam name="TMapping"></typeparam>
        /// <param name="mapping"></param>
        public static void AddMapping<TMapping>(TMapping mapping)
            where TMapping : class, IFilterFieldExprNameMapping
        {
            InternalAddMapping(mapping);
        }

        /// <summary>
        /// Add 一个自定义字段的处理Handler
        /// </summary>
        /// <typeparam name="TFilterFieldHandler"></typeparam>
        /// <param name="filterFiledHandlerFunc"></param>
        public static void AddFilterFieldHandler<TFilterFieldHandler>(Func<TFilterFieldHandler> filterFiledHandlerFunc)
            where TFilterFieldHandler : class, IFilterFieldHandler
        {
            FilterFieldHandlerFactory.Register(filterFiledHandlerFunc);
        }        
        public static void ShoutDown()
        {
            FilterFieldHandlerFactory.Clear();
            FieldExprNameMappingFactory.Clear();
            GC.Collect();
        }

        internal static void Reset()
        {
            FilterFieldHandlerFactory.Reset();
            FieldExprNameMappingFactory.Clear();
        }
        internal static void InternalAddMapping(IFilterFieldExprNameMapping mapping)
        {
            if (mapping.GetType().GetTypeInfo().IsClass)
            {
                var name = TypeHelper.GetGenericTypeUniqueName(mapping.FilterType);
                FieldExprNameMappingFactory.Add(name, mapping.Mapping().ToList());
            }
        }
    }
}
