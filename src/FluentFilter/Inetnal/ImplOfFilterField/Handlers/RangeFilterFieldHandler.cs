using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentFilter.Inetnal.ImplOfFilterField.Handlers
{
    using OhPrimitives;
    using System.Linq.Expressions;
    internal class RangeFilterFieldHandler : FreeDomRangeFilterFieldHandler
    {
        private static readonly Type filterFieldType = typeof(RangeField<>);
        public override Type FilterFieldType => filterFieldType;

        protected override Expression InternalHandleWhere<TPrimitive, TFiledOfPrimitive>(LambdaExpression node, FilterFieldMetaInfo metaData)
        {
            var field = metaData.FilterFieldInstace as RangeField<TPrimitive>;
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
                if (field.MinCompareMode == CompareMode.GreaterThan)
                    expr = ExpressionType.GreaterThan;
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

                if (field.MaxCompareMode == CompareMode.LessThan)
                    expr = ExpressionType.LessThan;
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
