using System;

namespace FluentFilter.Mappings
{
    public interface IFilterFieldExprNameMapping
    {
        MappingInfo[] Mapping();
        Type FilterType { get; }
    }
}
