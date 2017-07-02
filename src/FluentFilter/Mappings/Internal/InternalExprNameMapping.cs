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
    public class InternalExprNameMapping<TFilter> : DefaultExprNameMapping<TFilter>
        where TFilter : class, IDataFilter
    {
        public override MappingInfo[] Mapping()
        {
            return InternalMapping();
        }
        protected MappingInfo[] InternalMapping()
        {
            var _maps = default(MappingInfo[]);
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
                _maps = maps.ToArray();
            });
            return _maps;
        }
    }
}
