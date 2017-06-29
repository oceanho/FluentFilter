using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;
using OhDotNetLib.Reflection;

namespace FluentFilter.Mappings
{
    public class DefaultFilterFieldExprNameMapping<TMapping, TFilter> : IFilterFieldExprNameMapping
        where TFilter : class, IDataFilter
        where TMapping : DefaultFilterFieldExprNameMapping<TMapping, TFilter>
    {

        public virtual void Mapping()
        {
            var filterTypeUniqueName = TypeHelper.GetGenericTypeUniqueName(FilterType);
            var properties = ReflectionHelper.GetPropertiesFromType(FilterType);
            FieldExprNameMappingFactory.Add(filterTypeUniqueName, (maps) =>
            {
                var mapinfo = default(MappingInfo);
                foreach (var property in properties)
                {
                    
                    var exprNameAttribute = property.GetCustomAttribute<ExprNameAttribute>(true);
                    mapinfo = new MappingInfo()
                    {
                        Property = property,
                        ExprName = exprNameAttribute == null ? property.Name : exprNameAttribute.ExprName
                    };
                    maps.Add(mapinfo);
                }
            });
        }

        protected Type FilterType => typeof(TFilter);

        public TMapping Map(Expression<Func<TFilter, MemberInfo>> predicate)
        {
            var property = predicate.Compile();

            return (TMapping)this;
        }
    }
}
