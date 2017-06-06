
using System;
using System.Reflection;
using System.Linq.Expressions;

using FluentFilter.Inetnal.ImplOfFilterField;

namespace FluentFilter.Inetnal
{
    internal class FilterFieldVisitorExecutor
    {
        public void Execute(FilterFieldVisitorContext context, FilterFieldMetaInfo metaInfo)
        {
            var isGenericFieldType = false;
            var fieldTypeKey = metaInfo.FilterFieldType;
            if (fieldTypeKey.GetTypeInfo().IsGenericType)
            {
                isGenericFieldType = true;
                fieldTypeKey = metaInfo.FilterFieldType.GetGenericTypeDefinition();
            }

            FilterFieldVisitorExecutorStaticObject.Visit((visitorProvider, visitorMethods) =>
            {
                var method = visitorMethods[fieldTypeKey];
                if (isGenericFieldType)
                {
                    var filterFieldType = metaInfo.FilterFieldType;
                    method = method.MakeGenericMethod(metaInfo.FieldType);
                }
                if (method == null)
                {
                    throw new ArgumentNullException($"UnSupport {nameof(metaInfo)}.FieldType visit");
                }
                var expression = (Expression)method.Invoke(visitorProvider, new object[] { context.Left, metaInfo.FilterFieldInstace });
                context.Left = expression;
            });
        }
    }
}
