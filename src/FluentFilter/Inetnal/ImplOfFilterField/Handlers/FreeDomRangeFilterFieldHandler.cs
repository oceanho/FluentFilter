using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentFilter.Inetnal.ImplOfFilterField.Handlers
{
    using OhDotNetLib.Reflection;
    using OhPrimitives;
    using System.Linq.Expressions;
    using System.Reflection;

    internal class FreeDomRangeFilterFieldHandler : DefaultFilterFieldHandler
    {
        private static readonly Type filterFieldType = typeof(FreeDomRangeField<>);
        public override Type FilterFieldType => filterFieldType;

        public override Expression HandleWhere<TPrimitive, TFiledOfPrimitive>(LambdaExpression node, Expression memberAccessExpr, Expression parameterExpr, FilterFieldMetaInfo metaData)
        {
            var method = GetType().GetTypeInfo().GetMethod(nameof(InternalHandleWhere), BindingFlags.Instance | BindingFlags.NonPublic);
            return (Expression)method.MakeGenericMethod(typeof(TPrimitive), typeof(TFiledOfPrimitive)).Invoke(this, new object[] { node, memberAccessExpr, parameterExpr, metaData });
        }

        protected Expression MakeExpr(LambdaExpression node, Expression left, Expression right)
        {
            if (left == null && right == null)
            {
                return node;
            }
            if (left != null && right == null)
            {
                Expression.Lambda(Expression.AndAlso(node.Body, left), node.Parameters);
            }
            if (left == null && right != null)
            {
                Expression.Lambda(Expression.AndAlso(node.Body, right), node.Parameters);
            }
            return Expression.Lambda(Expression.AndAlso(node.Body, Expression.MakeBinary(ExpressionType.AndAlso, left, right)), node.Parameters);
        }

        protected virtual Expression InternalHandleWhere<TPrimitive, TFiledOfPrimitive>(LambdaExpression node, Expression memberAccessExpr, Expression parameterExpr, FilterFieldMetaInfo metaData)
            where TPrimitive : struct, IConvertible, IComparable
        {
            var field = metaData.FilterFieldInstace as FreeDomRangeField<TPrimitive>;
            if (field == null)
            {
                throw new ArgumentException($"field should be {typeof(FreeDomRangeField<TPrimitive>)}");
            }

            Expression left = null;
            Expression right = null;

            #region min (left)
            if (field.Min != null)
            {
                var expr = ExpressionType.Default;
                var body = Expression.Constant(field.Min.Value);

                if (field.MinCompareMode == CompareMode.GreaterThan)
                    expr = ExpressionType.GreaterThan;
                else if (field.MinCompareMode == CompareMode.GreaterThanOrEqual)
                    expr = ExpressionType.GreaterThanOrEqual;
                if (expr == ExpressionType.Default)
                {
                    throw new ArgumentException($"invalid MinCompareMode {field.MinCompareMode.ToString()}");
                }
                left = Expression.MakeBinary(expr, memberAccessExpr, body);
            }
            #endregion

            #region max (right)
            if (field.Max != null)
            {
                var expr = ExpressionType.Default;
                var body = Expression.Constant(field.Max.Value);

                if (field.MaxCompareMode == CompareMode.LessThan)
                    expr = ExpressionType.LessThan;
                else if (field.MaxCompareMode == CompareMode.LessThanOrEqual)
                    expr = ExpressionType.LessThanOrEqual;
                if (expr == ExpressionType.Default)
                {
                    throw new ArgumentException($"invalid MaxCompareMode {field.MaxCompareMode.ToString()}");
                }
                right = Expression.MakeBinary(expr, memberAccessExpr, body);
            }
            #endregion

            return MakeExpr(node, left, right);
        }
    }
}
