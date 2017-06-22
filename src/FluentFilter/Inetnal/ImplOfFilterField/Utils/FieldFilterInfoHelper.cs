using System;
using System.Reflection;

using OhPrimitives;

namespace FluentFilter.Inetnal.ImplOfFilterField.Utils
{
    internal static class FilterFieldMetaInfoHelper
    {
        private static MethodInfo _createFilterFieldMetaInfoMethodInfo = typeof(FilterFieldMetaInfoHelper)
            .GetTypeInfo().GetMethod(nameof(CreateFilterFieldMetaInfo), BindingFlags.Static | BindingFlags.Public);

        public static FilterFieldMetaInfo CreateFilterFieldMetaInfo<TField>(TField Field)
            where TField : IField
        {
            return new FilterFieldMetaInfo<TField>(Field);
        }
        public static FilterFieldMetaInfo CreateFilterFieldMetaInfoByType(Type FieldType, object value)
        {
            return (FilterFieldMetaInfo)_createFilterFieldMetaInfoMethodInfo.MakeGenericMethod(FieldType).Invoke(null, new object[] { value });
        }
    }
}
