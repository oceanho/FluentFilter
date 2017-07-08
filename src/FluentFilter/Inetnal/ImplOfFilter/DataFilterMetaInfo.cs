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
            FilterFields = filterFieldList.ToImmutableList();

            FilterFiledsOfSort = filterFieldList
                .Where(p => ((p.FilterFieldInstace as IHasSortField)?.SortMode != SortMode.Disable))
                .Select((filter) =>
                {
                    return new FilterFieldSortMetaInfo(filter.FilterFieldInstace as IHasSortField, filter.FilterFieldType, filter.FilterFieldName, filter.FilterFieldOfElementBinderType);
                }).OrderByDescending(p => p.FilterFieldInstace).ToImmutableList();
        }
        public Type FilterType { get; }
        public IDataFilter FilterInstance { get; }
        public IReadOnlyCollection<FilterFieldMetaInfo> FilterFields { get; }
        public IReadOnlyCollection<FilterFieldSortMetaInfo> FilterFiledsOfSort { get; }
    }
}
