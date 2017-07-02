using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentFilter.Inetnal.ImplOfFilterField.Handlers
{
    using OhPrimitives;
    using System.Linq.Expressions;
    internal class CompareFilterFieldHandler : DefaultFilterFieldHandler
    {
        private static readonly Type filterFieldType = typeof(CompareField<>);
        public override Type FilterFieldType => filterFieldType;

        public override Expression HandleWhere<TPrimitive, TFiledOfPrimitive>(LambdaExpression node, FilterFieldMetaInfo metaData)
        {            
            var field = metaData.FilterFieldInstace as CompareField<TPrimitive>;
            if (field == null)
            {
                throw new ArgumentException($"field should be {typeof(CompareField<TPrimitive>)}");
            }
            var property = Expression.Property(node.Parameters[0], metaData.FilterFieldName);
            var predicateBody = Expression.Constant(field.Value);

            var expressionType = ExpressionType.Default;
            if (field.CompareMode == CompareMode.Equal)
                expressionType = ExpressionType.Equal;
            else if (field.CompareMode == CompareMode.NotEqual)
                expressionType = ExpressionType.NotEqual;
            else if (field.CompareMode == CompareMode.LessThan)
                expressionType = ExpressionType.LessThan;
            else if (field.CompareMode == CompareMode.LessThanOrEqual)
                expressionType = ExpressionType.LessThanOrEqual;
            else if (field.CompareMode == CompareMode.GreaterThan)
                expressionType = ExpressionType.GreaterThan;
            else if (field.CompareMode == CompareMode.GreaterThanOrEqual)
                expressionType = ExpressionType.GreaterThanOrEqual;
            if (expressionType == ExpressionType.Default)
            {
                throw new ArgumentException($"invalid CompareMode {field.CompareMode.ToString()}");
            }
            return Expression.Lambda(Expression.AndAlso(node.Body, Expression.MakeBinary(expressionType, property, predicateBody)), node.Parameters);
        }
    }
}
