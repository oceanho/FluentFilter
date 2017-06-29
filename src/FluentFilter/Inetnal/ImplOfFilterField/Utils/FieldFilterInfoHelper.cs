using System;
using System.Reflection;

using OhPrimitives;
using System.Linq;

namespace FluentFilter.Inetnal.ImplOfFilterField.Utils
{
    internal static class FilterFieldMetaInfoHelper
    {
        private static MethodInfo _createFilterFieldMetaInfoMethodInfo = typeof(FilterFieldMetaInfoHelper)
            .GetTypeInfo().GetMethod(nameof(CreateFilterFieldMetaInfo), BindingFlags.Static | BindingFlags.Public);

        public static FilterFieldMetaInfo CreateFilterFieldMetaInfo<TField>(TField field, string fieldExprName)
            where TField : IField
        {
            return new FilterFieldMetaInfo<TField>(field, fieldExprName);
        }
        public static FilterFieldMetaInfo CreateFilterFieldMetaInfoByType(Type fieldType, object value, string fieldExprName)
        {
            return (FilterFieldMetaInfo)_createFilterFieldMetaInfoMethodInfo.MakeGenericMethod(fieldType).Invoke(null, new object[] { value, fieldExprName });
        }

        public static PropertyInfo[] GetFieldPropertiesFromFilter(IDataFilter filter)
        {
            return OhDotNetLib.Reflection.ReflectionHelper.GetCanAssignabledTypeProperties(filter, typeof(IField)).ToArray();
        }

        public static FilterFieldMetaInfo[] GetFilterFields(IDataFilter filter)
        {
            // TODO: This is should cache all property for every IDataFilter
            var properties = GetFieldPropertiesFromFilter(filter);

            var fieldExprName = "";
            var fieldFilterIndex = 0;
            object fieldFilterValue = null;
            var FilterFieldMetaInfos = new FilterFieldMetaInfo[properties.Count()];

            foreach (var property in properties)
            {
                fieldExprName = property.Name;
                fieldFilterValue = property.GetValue(filter, null);
                FilterFieldMetaInfos[fieldFilterIndex++] = CreateFilterFieldMetaInfoByType(property.PropertyType, fieldFilterValue, fieldExprName);
            }

            return FilterFieldMetaInfos;
        }
    }
}
