using System;
using System.Collections.Generic;
using FluentFilter.Inetnal.ImplOfFilterField;
using System.Collections.Immutable;
using System.Linq;
using OhPrimitives;
using System.Reflection;
using FluentFilter.Mappings;

namespace FluentFilter.Inetnal.ImplOfFilter
{
    using OhDotNetLib.Extension;
    internal class DataFilterMetaInfo
    {
        public DataFilterMetaInfo(IDataFilter filterInstance, IEnumerable<FilterFieldMetaInfo> filterFieldList)
        {
            FilterInstance = filterInstance;
            FilterType = filterInstance.GetType();

            // Exclude SortField<T>
            FilterFields = filterFieldList.Where((attr) =>
            {
                var _attr = attr.FieldAttributes.OfType<DisableFieldExprAttribute>().FirstOrDefault();
                return _attr == null || _attr.DisabledMode == DisableFieldExprMode.Sort;
            })
            .Where(field => field.FilterFieldInstace.FilterSwitch == FilterSwitch.Open && 
            !(field.FilterFieldType.GetTypeInfo().IsGenericType && field.FilterFieldType.GetGenericTypeDefinition() == typeof(SortField<>))).ToImmutableList();


            // Sort Field List
            FilterFiledsOfSort = filterFieldList.Where((attr) =>
            {
                var _attr = attr.FieldAttributes.OfType<DisableFieldExprAttribute>().FirstOrDefault();
                return _attr == null || _attr.DisabledMode == DisableFieldExprMode.Filter;
            })
            .Where(p => p.FilterFieldType.GetTypeInfo().IsGenericType && ((p.FilterFieldInstace as IHasSortField)?.SortMode != SortMode.Disable))
            .Select((filter) =>
            {
                return new FilterFieldSortMetaInfo(filter.FilterFieldInstace as IHasSortField, filter.FilterFieldType, filter.FilterFieldExprName, filter.FieldAttributes);
            }).OrderByDescending(p => p.FilterFieldInstace as _SortField).ToImmutableList();
        }
        public Type FilterType { get; }
        public IDataFilter FilterInstance { get; }
        public IReadOnlyCollection<FilterFieldMetaInfo> FilterFields { get; }
        public IReadOnlyCollection<FilterFieldSortMetaInfo> FilterFiledsOfSort { get; }
    }
}
