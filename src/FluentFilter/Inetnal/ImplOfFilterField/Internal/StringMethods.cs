using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FluentFilter.Inetnal.ImplOfFilterField.Internal
{
    internal static class StringMethods
    {
        private static readonly MethodInfo[] _strMethods = typeof(String).GetTypeInfo().GetMethods();

        public static readonly MethodInfo IndexOf = _strMethods.FirstOrDefault(p => p.Name == "IndexOf" && p.GetParameters()[0].ParameterType == typeof(String) && p.GetParameters().Count() == 1);
        public static readonly MethodInfo EndsWith = _strMethods.FirstOrDefault(p => p.Name == "EndsWith");
        public static readonly MethodInfo StartsWith = _strMethods.FirstOrDefault(p => p.Name == "StartsWith");
        public static readonly MethodInfo IsNullOrEmpty = _strMethods.FirstOrDefault(p => p.Name == "IsNullOrEmpty");
    }
}
