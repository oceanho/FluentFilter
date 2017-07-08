using System;

namespace FluentFilter.Inetnal.ImplOfFilterField.Handlers
{
    using OhPrimitives;
    using System.Linq.Expressions;
    internal class BetweenFilterFieldHandler : FreeDomRangeFilterFieldHandler
    {
        private static readonly Type filterFieldType = typeof(BetweenField<>);
        public override Type FilterFieldType => filterFieldType;

        protected override Expression InternalHandleWhere<TPrimitive, TFiledOfPrimitive>(LambdaExpression node, Expression memberAccessExpr, Expression parameterExpr, FilterFieldMetaInfo metaData)
        {
            var field = metaData.FilterFieldInstace as BetweenField<TPrimitive>;
            if (field == null)
            {
                throw new ArgumentException($"field should be {typeof(BetweenField<TPrimitive>)}");
            }

            Expression left = null;
            Expression right = null;
            
            #region min (left)
            if (field.Min != null)
            {
                var expr = ExpressionType.Default;
                var body = Expression.Constant(field.Min.Value);
                if (field.MinCompareMode == CompareMode.GreaterThanOrEqual)
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

                if (field.MaxCompareMode == CompareMode.LessThanOrEqual)
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
