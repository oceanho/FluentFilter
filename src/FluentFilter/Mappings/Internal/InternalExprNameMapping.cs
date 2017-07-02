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

namespace FluentFilter.Mappings.Internal
{
    public class InternalExprNameMapping<TFilter> : DefaultExprNameMapping<TFilter>
        where TFilter : class, IDataFilter
    {
        public override MappingInfo[] Mapping()
        {
            return FieldExprNameMappingFactory.Add(FilterTypeUniqueName, InternalMapping().ToList()).ToArray();
        }
    }
}
