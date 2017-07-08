using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentFilter.Inetnal.ImplOfFilterField.Handlers
{
    using FluentFilter.Inetnal.ImplOfFilterField.Internal;
    using OhPrimitives;
    using System.Linq.Expressions;
    using System.Reflection;

    internal class LikeFilterFieldHandler : DefaultFilterFieldHandler
    {
        private static readonly Type filterFieldType = typeof(LikeField);
        public override Type FilterFieldType => filterFieldType;

        public override Expression HandleWhere<TPrimitive, TFiledOfPrimitive>(LambdaExpression node, Expression memberAccessExpr, Expression parameterExpr, FilterFieldMetaInfo metaData)
        {
            var field = metaData.FilterFieldInstace as LikeField;
            if (field == null)
            {
                throw new ArgumentException($"field should be {typeof(LikeField)}");
            }

            if (String.IsNullOrEmpty(field.Value))
            {
                return base.HandleWhere<TPrimitive, TFiledOfPrimitive>(node, memberAccessExpr, parameterExpr, metaData);
            }

            // p => !String.IsNullOrEmpty(p) && p.StartsWith(Value)
            // p => !String.IsNullOrEmpty(p) && p.EndsWith(Value)
            // p => !String.IsNullOrEmpty(p) && p.IndexOf(Value)>-1
            
            var exprArg = Expression.Constant(field.Value);
            var exprIsNotNullChecker = Expression.Not(Expression.Call(StringMethods.IsNullOrEmpty, memberAccessExpr));
            Expression exprBody = null;
            if (field.CompareMode == CompareMode.LeftLike)
            {
                exprBody = Expression.AndAlso(exprIsNotNullChecker, Expression.Call(memberAccessExpr, StringMethods.StartsWith, exprArg));
            }
            else if (field.CompareMode == CompareMode.RightLike)
            {
                exprBody = Expression.AndAlso(exprIsNotNullChecker, Expression.Call(memberAccessExpr, StringMethods.EndsWith, exprArg));
            }
            else if (field.CompareMode == CompareMode.FullSearchLike)
            {
                exprBody = Expression.AndAlso(exprIsNotNullChecker, Expression.GreaterThan(Expression.Call(memberAccessExpr, StringMethods.IndexOf, exprArg), Expression.Constant(-1)));
            }

            if (exprBody != null)
            {
                return Expression.Lambda(Expression.AndAlso(node.Body, exprBody), node.Parameters);
            }

            throw new ArgumentException($"invalid CompareMode {field.CompareMode.ToString()}");
        }
    }
}
