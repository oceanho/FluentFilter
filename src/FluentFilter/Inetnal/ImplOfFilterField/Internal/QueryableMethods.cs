using OhDotNetLib.Reflection;
using System;
using System.Linq;
using System.Reflection;

namespace FluentFilter.Inetnal.ImplOfFilterField.Internal
{
    internal static class QueryableMethods
    {
        private static readonly Type enumerableTyper = typeof(Queryable);

        public static readonly MethodInfo Where = MethodHelper.GetMethod(enumerableTyper, "Where", true);
        public static readonly MethodInfo Contains = MethodHelper.GetMethod(enumerableTyper, "Contains", true);
        public static readonly MethodInfo ThenBy = MethodHelper.GetMethod(enumerableTyper, "ThenBy", true);
        public static readonly MethodInfo OrderBy = MethodHelper.GetMethod(enumerableTyper, "OrderBy", true);
        public static readonly MethodInfo OrderByDescending = MethodHelper.GetMethod(enumerableTyper, "OrderByDescending", true);
    }
}
