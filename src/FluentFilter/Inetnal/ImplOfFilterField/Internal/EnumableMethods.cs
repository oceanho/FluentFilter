using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FluentFilter.Inetnal.ImplOfFilterField.Internal
{
    internal static class EnumableMethods
    {
        public static readonly MethodInfo Where = typeof(Enumerable).GetTypeInfo().GetMethods().FirstOrDefault(p => p.Name == "Where");
        public static readonly MethodInfo Contains = typeof(Enumerable).GetTypeInfo().GetMethods().FirstOrDefault(p => p.Name == "Contains");
    }
}
