﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OhDotNetLib.Reflection;
using OhDotNetLib.Utils;
using System.Collections.Immutable;

namespace FluentFilter.Mappings
{
    public class DefaultExprNameMapping<TFilter> : IFilterFieldExprNameMapping
        where TFilter : class, IDataFilter
    {
        private readonly TFilter m_filter;
        private readonly string m_filterTypeUniqueName;
        private readonly IReadOnlyList<PropertyInfo> m_filterProperties;
        private static readonly List<Type> m_validFiltertypes = new List<Type> {
            typeof(DataFilter<,>),
            typeof(DefaultDataFilter<,>),
        };

        public DefaultExprNameMapping()
        {
            m_filter = ReflectionHelper.CreateInstance<TFilter>();
            m_filterProperties = ReflectionHelper.GetProperties(m_filter).ToImmutableList();
            m_filterTypeUniqueName = TypeHelper.GetGenericTypeUniqueName(m_filter.GetType());
        }

        public virtual MappingInfo[] Mapping()
        {
            return InternalMapping();
        }

        internal MappingInfo[] InternalMapping()
        {
            var _maps = new List<MappingInfo>();
            foreach (var property in FilterProperties)
            {
                var exprAttr = property.GetCustomAttribute<FilterFieldExprNameAttribute>(true);
                var exprName = ObjectNullChecker.IsNullOrEmptyOfAnyOne(exprAttr, exprAttr?.ExprName) ? property.Name : exprAttr.ExprName;
                _maps.Add(new MappingInfo()
                {
                    Property = property,
                    ExprName = exprName,
                    ExprNameAttribute = exprAttr
                });
            }
            return _maps.ToArray();
        }
        protected TFilter Filter => m_filter;
        public virtual string FilterTypeUniqueName => m_filterTypeUniqueName;
        protected IReadOnlyList<PropertyInfo> FilterProperties => m_filterProperties;
    }
}
