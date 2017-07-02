using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentFilter.Inetnal.ImplOfFilterField.Handlers
{
    using OhPrimitives;
    using System.Linq.Expressions;
    internal class BetweenFilterFieldHandler : FreeDomRangeFilterFieldHandler
    {
        private static readonly Type filterFieldType = typeof(BetweenField<>);
        public override Type FilterFieldType => filterFieldType;

        protected override Expression InternalHandleWhere<TPrimitive, TFiledOfPrimitive>(LambdaExpression node, FilterFieldMetaInfo metaData)
        {
            var field = metaData.FilterFieldInstace as BetweenField<TPrimitive>;
            if (field == null)
            {
                throw new ArgumentException($"field should be {typeof(BetweenField<TPrimitive>)}");
            }

            Expression left = null;
            Expression right = null;

            var property = Expression.Property(node.Parameters[0], metaData.FilterFieldName);

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
                left = Expression.MakeBinary(expr, property, body);
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
                right = Expression.MakeBinary(expr, property, body);
            }
            #endregion

            return MakeExpr(node, left, right);
        }
    }
}
