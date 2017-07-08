using System;

namespace FluentFilter.Inetnal.ImplOfFilterField.Handlers
{
    using FluentFilter.Inetnal.ImplOfFilterField.Internal;
    using OhDotNetLib.Extension;
    using OhPrimitives;
    using System.Linq.Expressions;

    internal class ContainsFilterFieldHandler : DefaultFilterFieldHandler
    {
        private static readonly Type filterFieldType = typeof(ContainsField<>);
        public override Type FilterFieldType => filterFieldType;

        public override Expression HandleWhere<TPrimitive, TFiledOfPrimitive>(LambdaExpression node, Expression memberAccessExpr, Expression parameterExpr, FilterFieldMetaInfo metaData)
        {
            var field = metaData.FilterFieldInstace as ContainsField<TPrimitive>;
            if (field == null)
            {
                throw new ArgumentException($"field should be {typeof(ContainsField<TPrimitive>)}");
            }

            if (!(field.Values.IsEmpty()))
            {
                var body = Expression.Constant(field.Values);
                var method = EnumableMethods.Contains.MakeGenericMethod(typeof(TPrimitive));
                var methodExpr = Expression.Call(method, body, memberAccessExpr);
                if (field.CompareMode == CompareMode.Contains)
                {
                    return Expression.Lambda(Expression.AndAlso(node.Body, methodExpr), node.Parameters);
                }
                if (field.CompareMode == CompareMode.NotContains)
                {
                    return Expression.Lambda(Expression.AndAlso(node.Body, Expression.Not(methodExpr)), node.Parameters);
                }
                throw new ArgumentException($"invalid CompareMode {field.CompareMode.ToString()}");
            }
            return base.HandleWhere<TPrimitive, TFiledOfPrimitive>(node, memberAccessExpr, parameterExpr, metaData);
        }
    }
}
