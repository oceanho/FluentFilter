using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fluent.DataFilter.Utils
{
    internal static class FieldFilterInfoHelper
    {
        private static MethodInfo _createFieldFilterInfoMethodInfo = typeof(FieldFilterInfoHelper)
            .GetTypeInfo().GetMethod(nameof(CreateFieldFilterInfo), BindingFlags.Static | BindingFlags.Public);

        public static FieldFilterInfo CreateFieldFilterInfo<TField>(TField Field)
            where TField : IFilterField
        {
            return new FieldFilterInfo<TField>(Field);
        }
        public static FieldFilterInfo CreateFieldFilterInfoByType(Type FieldType, object value)
        {
            return (FieldFilterInfo)_createFieldFilterInfoMethodInfo.MakeGenericMethod(FieldType).Invoke(null, new object[] { value });
        }
    }
}
