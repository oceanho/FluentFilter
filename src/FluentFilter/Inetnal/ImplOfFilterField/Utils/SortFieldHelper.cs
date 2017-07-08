using System;
using System.Reflection;

using OhPrimitives;
using System.Linq;
using FluentFilter.Mappings;
using OhDotNetLib.Reflection;
using FluentFilter.Mappings.Internal;

namespace FluentFilter.Inetnal.ImplOfFilterField.Utils
{
    internal static class SortFieldHelper
    {
        private static MethodInfo _createSortFieldMethodInfo = MethodHelper.GetMethod(typeof(SortFieldHelper), nameof(CreateSortField));

        public static IHasSortField CreateSortFieldByType(Type fieldType, IHasSortField value)
        {
            return (IHasSortField)_createSortFieldMethodInfo.MakeGenericMethod(fieldType).Invoke(null, new object[] { value, value});
        }

        public static SortField<TPrimitive> CreateSortField<TPrimitive>(IHasSortField sortSrc)
        {
            return new SortField<TPrimitive>
            {
                SortMode = sortSrc.SortMode,
                SortPriority = sortSrc.SortPriority
            };
        }
    }
}
