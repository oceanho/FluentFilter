using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;
using OhDotNetLib.Reflection;

using OhDotNetLib.Extension;
using OhDotNetLib.Utils;
using System.Collections.Immutable;

namespace FluentFilter.Mappings
{
    public class DefaultExprNameMapping<TFilter> : IFilterFieldExprNameMapping
        where TFilter : class, IDataFilter
    {
        private readonly TFilter m_filter;
        private readonly Type m_filterType;
        private readonly string m_filterTypeUniqueName;
        private readonly IReadOnlyList<PropertyInfo> m_filterProperties;
        public DefaultExprNameMapping()
        {
            m_filter = ReflectionHelper.CreateInstance<TFilter>();
            m_filterProperties = ReflectionHelper.GetProperties(m_filter).ToImmutableList();
            m_filterTypeUniqueName = TypeHelper.GetGenericTypeUniqueName(m_filter.GetType());
            m_filterType = m_filter.GetType();
        }

        public virtual MappingInfo[] Mapping()
        {
            return null;
        }

        public Type FilterType => m_filterType;
        protected TFilter Filter => m_filter;
        protected string FilterTypeUniqueName => m_filterTypeUniqueName;
        protected IReadOnlyList<PropertyInfo> FilterProperties => m_filterProperties;

    }
}
