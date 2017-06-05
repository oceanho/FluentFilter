
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using Fluent.DataFilter.Inetnal.Visitors;

namespace Fluent.DataFilter.Inetnal
{
    internal static class FilterFieldVisitorExecutorStaticObject
    {
        private static readonly object _filterFieldVisitor;
        private static readonly Dictionary<Type, MethodInfo> _filterFieldMethods;
        static FilterFieldVisitorExecutorStaticObject()
        {
            _filterFieldVisitor = new FilterFieldVisitor();
            _filterFieldMethods = new Dictionary<Type, MethodInfo>();

            var methods = _filterFieldVisitor.GetType().GetTypeInfo()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .Where(method => method.Name.StartsWith("visit", StringComparison.CurrentCultureIgnoreCase)).ToList();

            foreach (var method in methods)
            {
                var type = method.GetParameters()[0].ParameterType;
                if (type.GetTypeInfo().IsGenericType)
                {
                    type = type.GetGenericTypeDefinition();
                }
                _filterFieldMethods[type] = method;
            }
        }

        public static void Visit(Action<object, Dictionary<Type, MethodInfo>> visitorAction)
        {
            visitorAction(_filterFieldVisitor, _filterFieldMethods);
        }
    }
}
