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
    internal class DefaultExprNameMapping<TFilter> : DefaultFilterFieldExprNameMapping<DefaultExprNameMapping<TFilter>, TFilter>
        where TFilter : class, IDataFilter
    {
        public override void Mapping()
        {
            var filterType = Filter.GetType();
            var filterTypeUniqueName = TypeHelper.GetGenericTypeUniqueName(filterType);
            var filterTypeProperties = ReflectionHelper.GetPropertiesFromType(filterType);
            FieldExprNameMappingFactory.Add(filterTypeUniqueName, (maps) =>
            {
                var mapinfo = default(MappingInfo);
                foreach (var property in filterTypeProperties)
                {
                    var exprAttr = property.GetCustomAttribute<ExprNameAttribute>(true);
                    var exprName = ObjectNullChecker.IsNullOrEmptyOfAnyOne(exprAttr, exprAttr?.ExprName) ? property.Name : exprAttr.ExprName;
                    mapinfo = new MappingInfo()
                    {
                        Property = property,
                        ExprName = exprName
                    };
                    maps.Add(mapinfo);
                }
            });
        }
    }
}
