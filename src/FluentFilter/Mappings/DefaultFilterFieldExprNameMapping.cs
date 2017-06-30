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

namespace FluentFilter.Mappings
{
    public class DefaultFilterFieldExprNameMapping<TMapping, TFilter> : IFilterFieldExprNameMapping
        where TFilter : class, IDataFilter
        where TMapping : DefaultFilterFieldExprNameMapping<TMapping, TFilter>
    {

        private readonly TFilter m_TFilter;
        public DefaultFilterFieldExprNameMapping()
        {
            m_TFilter = ReflectionHelper.CreateInstance<TFilter>();
        }
        public virtual void Mapping()
        {
        }
        protected TFilter Filter => m_TFilter;
        public void Map(Expression<Func<TFilter, MemberInfo>> predicate)
        {
            var property = predicate.Compile()(m_TFilter);
        }
    }
}
