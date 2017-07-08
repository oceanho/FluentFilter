using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentFilter.Inetnal.ImplOfFilterField.Handlers
{
    using OhDotNetLib.Extension;
    using OhPrimitives;
    using System.Linq.Expressions;
    internal class EqualFilterFieldHandler : DefaultFilterFieldHandler
    {
        private static readonly Type filterFieldType = typeof(EqualsField<>);
        public override Type FilterFieldType => filterFieldType;

        public override Expression HandleWhere<TPrimitive, TFiledOfPrimitive>(LambdaExpression node, Expression memberAccessExpr, Expression parameterExpr, FilterFieldMetaInfo metaData)
        {
            var field = metaData.FilterFieldInstace as EqualsField<TPrimitive>;
            if (field == null)
            {
                throw new ArgumentException($"field should be {typeof(EqualsField<TPrimitive>)}");
            }
            if (!(field.Value.IsEmpty()))
            {
                if (field.CompareMode == CompareMode.Equal)
                {
                    var value = Expression.Constant(field.Value);
                    return Expression.Lambda(Expression.AndAlso(node.Body, Expression.Equal(memberAccessExpr, value)), node.Parameters);
                }
                throw new ArgumentException($"invalid CompareMode {field.CompareMode.ToString()}");
            }
            return base.HandleWhere<TPrimitive, TFiledOfPrimitive>( node, memberAccessExpr, parameterExpr, metaData);
        }
    }
}
