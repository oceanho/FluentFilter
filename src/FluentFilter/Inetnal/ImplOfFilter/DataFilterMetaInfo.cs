using System;
using System.Collections.Generic;
using FluentFilter.Inetnal.ImplOfFilterField;
using System.Collections.Immutable;
using System.Linq;
using OhPrimitives;
using System.Reflection;

namespace FluentFilter.Inetnal.ImplOfFilter
{
    internal class DataFilterMetaInfo
    {
        public DataFilterMetaInfo(IDataFilter filterInstance, IEnumerable<FilterFieldMetaInfo> filterFieldList)
        {
            FilterInstance = filterInstance;
            FilterType = filterInstance.GetType();

            // Exclude SortField<T>
            FilterFields = filterFieldList.Where(field =>
                field.FilterFieldInstace.FilterSwitch == FilterSwitch.Open &&
                !(field.FilterFieldType.GetTypeInfo().IsGenericType && field.FilterFieldType.GetGenericTypeDefinition() == typeof(SortField<>))).ToImmutableList();

            // Sort Field List
            FilterFiledsOfSort = filterFieldList
                .Where(p => p.FilterFieldType.GetTypeInfo().IsGenericType && ((p.FilterFieldInstace as _SortField)?.SortMode != SortMode.Disable))
                .Select((filter) =>
                {
                    return new FilterFieldSortMetaInfo(filter.FilterFieldInstace as _SortField, filter.FilterFieldType, filter.FilterFieldName, filter.FilterFieldOfElementBinderType);
                }).OrderByDescending(p => p.FilterFieldInstace).ToImmutableList();
        }
        public Type FilterType { get; }
        public IDataFilter FilterInstance { get; }
        public IReadOnlyCollection<FilterFieldMetaInfo> FilterFields { get; }
        public IReadOnlyCollection<FilterFieldSortMetaInfo> FilterFiledsOfSort { get; }
    }
}
