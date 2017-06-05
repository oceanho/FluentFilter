using Fluent.DataFilter.Relection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Fluent.DataFilter.Inetnal
{
    internal class FilterFieldVisitorExecutor
    {
        private static readonly object _filterFieldVisitor;
        private static readonly Dictionary<string, MethodInfo> _filterFieldMethods;
        static FilterFieldVisitorExecutor()
        {
            _filterFieldVisitor = new FilterFieldVisitor();
            _filterFieldMethods = new Dictionary<string, MethodInfo>();

            var methods = _filterFieldVisitor.GetType().GetTypeInfo()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .Where(method => method.Name.StartsWith("visit", StringComparison.CurrentCultureIgnoreCase)).ToList();

            foreach (var method in methods)
            {
                _filterFieldMethods[ReflectionHelper.GetTypeUniqueName(method.GetParameters()[0].ParameterType)] = method;
            }
        }

        public void Execute(FilterFieldVisitorContext context, FieldFilterInfo fieldFilterInfo)
        {
            var isGenericFieldType = false;
            var fieldTypeKey = fieldFilterInfo.FilterFieldType;
            if (fieldTypeKey.GetTypeInfo().IsGenericType)
            {
                isGenericFieldType = true;
                fieldTypeKey = fieldFilterInfo.FilterFieldType.GetGenericTypeDefinition();
            }
            var method = _filterFieldMethods[ReflectionHelper.GetTypeUniqueName(fieldTypeKey)];
            if (isGenericFieldType)
            {
                method = method.MakeGenericMethod(fieldFilterInfo.FilterFieldType);
            }
            if (method == null)
            {
                throw new ArgumentNullException($"UnSupport {nameof(fieldFilterInfo)}.FieldType visit");
            }
            var expression = (Expression)method.Invoke(_filterFieldVisitor, new object[] { fieldFilterInfo.FilterFieldInstace });
        }
    }
}
