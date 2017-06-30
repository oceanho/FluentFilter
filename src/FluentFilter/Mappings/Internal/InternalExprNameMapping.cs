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

namespace FluentFilter.Mappings.Internal
{
    public class InternalExprNameMapping<TFilter> : EmptyFilterExprNameMapping<TFilter>
        where TFilter : class, IDataFilter
    {
        public override void Mapping()
        {
            FieldExprNameMappingFactory.Add(FilterTypeUniqueName, (maps) =>
            {
                var mapinfo = default(MappingInfo);
                foreach (var property in FilterProperties)
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
