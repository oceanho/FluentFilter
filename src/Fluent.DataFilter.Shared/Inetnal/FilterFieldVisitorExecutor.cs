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
        public void Execute(FilterFieldVisitorContext context, FieldFilterInfo fieldFilterInfo)
        {
            var isGenericFieldType = false;
            var fieldTypeKey = fieldFilterInfo.FilterFieldType;
            if (fieldTypeKey.GetTypeInfo().IsGenericType)
            {
                isGenericFieldType = true;
                fieldTypeKey = fieldFilterInfo.FilterFieldType.GetGenericTypeDefinition();
            }

            FilterFieldVisitorExecutorStaticObject.Visit((visitorProvider, visitorMethods) =>
            {
                var method = visitorMethods[fieldTypeKey];
                if (isGenericFieldType)
                {
                    var filterFieldType = fieldFilterInfo.FilterFieldType;
                    method = method.MakeGenericMethod(fieldFilterInfo.FilterFieldInstace.FieldType);
                }
                if (method == null)
                {
                    throw new ArgumentNullException($"UnSupport {nameof(fieldFilterInfo)}.FieldType visit");
                }
                var expression = (Expression)method.Invoke(visitorProvider, new object[] { fieldFilterInfo.FilterFieldInstace });
            });
        }
    }
}
