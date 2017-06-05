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
        private static readonly Dictionary<Type, MethodInfo> _filterFieldMethods;
        static FilterFieldVisitorExecutor()
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

        public void Execute(FilterFieldVisitorContext context, FieldFilterInfo fieldFilterInfo)
        {
            var isGenericFieldType = false;
            var fieldTypeKey = fieldFilterInfo.FilterFieldType;
            if (fieldTypeKey.GetTypeInfo().IsGenericType)
            {
                isGenericFieldType = true;
                fieldTypeKey = fieldFilterInfo.FilterFieldType.GetGenericTypeDefinition();
            }
            var method = _filterFieldMethods[fieldTypeKey];
            if (isGenericFieldType)
            {
                var filterFieldType = fieldFilterInfo.FilterFieldType;
                method = method.MakeGenericMethod(fieldFilterInfo.FilterFieldInstace.FieldType);
            }
            if (method == null)
            {
                throw new ArgumentNullException($"UnSupport {nameof(fieldFilterInfo)}.FieldType visit");
            }
            var expression = (Expression)method.Invoke(_filterFieldVisitor, new object[] { fieldFilterInfo.FilterFieldInstace });
        }
    }
}
