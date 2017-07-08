using System;
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
        private readonly Type m_defaultFilterFieldElementBindType;
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
            m_defaultFilterFieldElementBindType = FindFilterDefaultElementBindType(m_filter.GetType().GetTypeInfo());
        }

        public virtual MappingInfo[] Mapping()
        {
            return InternalMapping();
        }
        private Type FindFilterDefaultElementBindType(TypeInfo typeInfo)
        {
            var _typeInfo = typeInfo.BaseType.GetTypeInfo();
            var _typeInfoGenericTypeDefinition = _typeInfo.GetGenericTypeDefinition();
            if (m_validFiltertypes.Contains(_typeInfoGenericTypeDefinition) == false)
            {
                throw new ArgumentException("invalid type of TFilter, TFilter should be inherit from DefaultDataFilter<,> or DataFilter<,>");
            }
            return _typeInfo.GetGenericArguments()[0];
        }

        internal MappingInfo[] InternalMapping()
        {
            var _maps = new List<MappingInfo>();
            foreach (var property in FilterProperties)
            {
                var exprAttr = property.GetCustomAttribute<FilterFieldExprNameMapAttribute>(true);
                var exprName = ObjectNullChecker.IsNullOrEmptyOfAnyOne(exprAttr, exprAttr?.ExprName) ? property.Name : exprAttr.ExprName;
                var elementBindType = ObjectNullChecker.IsNullOrEmptyOfAnyOne(exprAttr, exprAttr?.FilterFieldElementBindType) ? m_defaultFilterFieldElementBindType : exprAttr.FilterFieldElementBindType;
                _maps.Add(new MappingInfo()
                {
                    Property = property,
                    ExprName = exprName,
                    ExprNameAttribute = exprAttr,
                    FilterFieldElementBindType = elementBindType
                });
            }
            return _maps.ToArray();
        }
        protected TFilter Filter => m_filter;
        public virtual string FilterTypeUniqueName => m_filterTypeUniqueName;
        protected IReadOnlyList<PropertyInfo> FilterProperties => m_filterProperties;
    }
}
