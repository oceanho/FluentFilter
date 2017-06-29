using System;
using System.Collections.Generic;
using FluentFilter.Inetnal.ImplOfFilterField;
using System.Collections.Immutable;
using System.Linq;
using OhPrimitives;

namespace FluentFilter.Inetnal.ImplOfFilter
{
    internal class DataFilterMetaInfo
    {
        public DataFilterMetaInfo(IDataFilter filterInstance, IEnumerable<FilterFieldMetaInfo> filterFieldList)
        {
            FilterInstance = filterInstance;
            FilterType = filterInstance.GetType();
            FilterFieldList = filterFieldList.ToImmutableList();

            FilterFiledSortList = filterFieldList
                .Where(p => ((p.FilterFieldInstace as IHasSortField) != null))
                .Select((filter) =>
                {
                    return new FilterFieldSortMetaInfo(filter.FilterFieldInstace as IHasSortField, filter.FilterFieldName);
                }).OrderBy(p => p.FilterFieldInstace).ToImmutableList();
        }
        public Type FilterType { get; }
        public IDataFilter FilterInstance { get; }
        public IReadOnlyCollection<FilterFieldMetaInfo> FilterFieldList { get; }
        public IReadOnlyCollection<FilterFieldSortMetaInfo> FilterFiledSortList { get; }
    }
}
