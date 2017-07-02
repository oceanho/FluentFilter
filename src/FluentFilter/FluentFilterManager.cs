﻿using FluentFilter.Inetnal.ImplOfFilterField;
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
    public static class FluentFilterManager
    {
        public static void AddMapping<TMapping, TFilter>()
            where TFilter : class, IDataFilter
            where TMapping : DefaultExprNameMapping<TFilter>
        {
            AddMapping<TMapping>();
        }

        public static void AddMapping<TMapping>()
            where TMapping : class, IFilterFieldExprNameMapping
        {
            AddMapping(ReflectionHelper.CreateInstance<TMapping>());
        }
        internal static void InternalAddMapping(IFilterFieldExprNameMapping mapping)
        {
            if (mapping.GetType().GetTypeInfo().IsClass)
            {
                var name = TypeHelper.GetGenericTypeUniqueName(mapping.FilterType);
                FieldExprNameMappingFactory.Add(name, mapping.Mapping().ToList());
            }
        }
        public static void AddMapping<TMapping>(TMapping mapping)
            where TMapping : class, IFilterFieldExprNameMapping
        {
            InternalAddMapping(mapping);
        }

        public static void AddFilterFieldHandler<TFilterFieldHandler>(Func<TFilterFieldHandler> filterFiledHandlerFunc)
            where TFilterFieldHandler : class, IFilterFieldHandler
        {
            FilterFieldHandlerFactory.Register(filterFiledHandlerFunc);
        }

        public static void Reset()
        {
            FilterFieldHandlerFactory.Reset();
            FieldExprNameMappingFactory.Clear();
        }

        public static void ShoutDown()
        {
            FilterFieldHandlerFactory.Clear();
            FieldExprNameMappingFactory.Clear();
            GC.Collect();
        }
    }
}
