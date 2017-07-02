using System;
using System.Reflection;

using OhPrimitives;
using System.Linq;
using FluentFilter.Mappings;
using OhDotNetLib.Reflection;
using FluentFilter.Mappings.Internal;
using OhDotNetLib.Extension;

namespace FluentFilter.Inetnal.ImplOfFilterField.Utils
{
    internal static class FilterFieldMetaInfoHelper
    {
        private static MethodInfo _createFilterFieldMetaInfoMethodInfo = typeof(FilterFieldMetaInfoHelper)
            .GetTypeInfo().GetMethod(nameof(CreateFilterFieldMetaInfo), BindingFlags.Static | BindingFlags.Public);

        public static FilterFieldMetaInfo CreateFilterFieldMetaInfo<TField>(TField field, string fieldExprName)
            where TField : class, IField
        {
            return new FilterFieldMetaInfo<TField>(field, fieldExprName);
        }
        public static FilterFieldMetaInfo CreateFilterFieldMetaInfoByType(Type fieldType, object value, string fieldExprName)
        {
            return (FilterFieldMetaInfo)_createFilterFieldMetaInfoMethodInfo.MakeGenericMethod(fieldType).Invoke(null, new object[] { value, fieldExprName });
        }

        public static PropertyInfo[] GetFieldPropertiesFromFilter(IDataFilter filter)
        {
            return ReflectionHelper.GetCanAssignabledTypeProperties(filter, typeof(IField)).ToArray();
        }

        public static FilterFieldMetaInfo[] GetFilterFields(IDataFilter filter)
        {
            var properties = GetFieldPropertiesFromFilter(filter);
            var filterUniqueName = TypeHelper.GetGenericTypeUniqueName(filter.GetType());

            var propExprMappings = FieldExprNameMappingFactory.Get(filterUniqueName, () =>
            {
                return InternalExprNameMappingUtil.CreateFilterExprNameMappings(filter).ToList();
            });

            var newpropExprMappings = propExprMappings.IsEmpty() ? InternalExprNameMappingUtil.CreateFilterExprNameMappings(filter) : propExprMappings;

            var fieldExprName = "";
            var fieldFilterIndex = 0;
            object fieldValue = null;
            var FilterFieldMetaInfos = new FilterFieldMetaInfo[properties.Count()];
            foreach (var property in properties)
            {
                fieldValue = property.GetValue(filter, null);
                fieldExprName = newpropExprMappings.FirstOrDefault(p => p.Property.Name.Equals(property.Name)).ExprName;
                FilterFieldMetaInfos[fieldFilterIndex++] = CreateFilterFieldMetaInfoByType(property.PropertyType, fieldValue, fieldExprName);
            }

            return FilterFieldMetaInfos;
        }
    }
}
