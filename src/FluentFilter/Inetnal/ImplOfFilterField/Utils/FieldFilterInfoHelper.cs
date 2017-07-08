using System;
using System.Reflection;

using OhPrimitives;
using System.Linq;
using FluentFilter.Mappings;
using OhDotNetLib.Reflection;
using FluentFilter.Mappings.Internal;

namespace FluentFilter.Inetnal.ImplOfFilterField.Utils
{
    internal static class FilterFieldMetaInfoHelper
    {
        private static MethodInfo _createFilterFieldMetaInfoMethodInfo = typeof(FilterFieldMetaInfoHelper)
            .GetTypeInfo().GetMethod(nameof(CreateFilterFieldMetaInfo), BindingFlags.Static | BindingFlags.Public);

        public static FilterFieldMetaInfo CreateFilterFieldMetaInfo<TField>(TField field, string fieldExprName, Type filterFieldOfElementBinderType)
            where TField : class, IField
        {
            return new FilterFieldMetaInfo<TField>(field, fieldExprName, filterFieldOfElementBinderType);
        }
        public static FilterFieldMetaInfo CreateFilterFieldMetaInfoByType(Type fieldType, object value, string fieldExprName, Type filterFieldOfElementBinderType)
        {
            return (FilterFieldMetaInfo)_createFilterFieldMetaInfoMethodInfo.MakeGenericMethod(fieldType).Invoke(null, new object[] { value, fieldExprName, filterFieldOfElementBinderType });
        }

        public static PropertyInfo[] GetFieldPropertiesFromFilter(IDataFilter filter)
        {
            return ReflectionHelper.GetCanAssignabledTypeProperties(filter, typeof(IField)).ToArray();
        }

        public static FilterFieldMetaInfo[] GetFilterFields(IDataFilter filter)
        {
            var filterUniqueName = TypeHelper.GetGenericTypeUniqueName(filter.GetType());
            var propExprMappings = FieldExprNameMappingFactory.Get(filterUniqueName, () =>
            {
                return InternalExprNameMappingUtil.CreateFilterExprNameMappings(filter).ToList();
            });

            var exprNames = propExprMappings.Select(p => p.Property.Name);
            var properties = GetFieldPropertiesFromFilter(filter).Where(p => exprNames.Contains(p.Name));

            var fieldFilterIndex = 0;
            var FilterFieldMetaInfos = new FilterFieldMetaInfo[properties.Count()];
            foreach (var property in properties)
            {
                var map = propExprMappings.FirstOrDefault(p => p.Property.Name == property.Name);
                var objValue = property.GetValue(filter, null);
                var sortValue = objValue as IHasSortField;
                if (sortValue != null && sortValue.SortMode == SortMode.Disable)
                {
                    if (map.ExprNameAttribute != null)
                    {
                        sortValue.SortMode = map.ExprNameAttribute.SortMode;
                        sortValue.SortPriority = map.ExprNameAttribute.SortPriority;
                    }
                    objValue = sortValue;
                }
                FilterFieldMetaInfos[fieldFilterIndex++] = CreateFilterFieldMetaInfoByType(property.PropertyType, objValue, map.ExprName, map.FilterFieldElementBindType);
            }

            return FilterFieldMetaInfos;
        }
    }
}
