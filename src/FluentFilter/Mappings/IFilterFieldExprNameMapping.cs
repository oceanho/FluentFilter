using System;

namespace FluentFilter.Mappings
{
    public interface IFilterFieldExprNameMapping
    {
        MappingInfo[] Mapping();
        string FilterTypeUniqueName { get; }
    }
}
