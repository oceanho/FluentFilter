using System.Linq;

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
