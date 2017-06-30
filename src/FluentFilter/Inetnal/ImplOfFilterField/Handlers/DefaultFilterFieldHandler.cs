using OhPrimitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentFilter.Inetnal.ImplOfFilterField.Handlers
{
    using OhPrimitives;

    public abstract class DefaultFilterFieldHandler : IFilterFieldHandler
    {
        private static readonly string sortName = nameof(HandleSort);
        private static readonly string filterName = nameof(HandleWhere);
        public DefaultFilterFieldHandler()
        {
        }

        public abstract Type FilterType { get; }

        public Expression Handle(Expression node, FilterFieldMetaInfo metaData, bool isSort)
        {
            if (node.NodeType != ExpressionType.Lambda)
            {
                throw new ArgumentException($"invalid node.NodeType {node.NodeType}. It's should be {ExpressionType.Lambda.ToString()}");
            }

            var method = GetType().GetTypeInfo().GetMethod((isSort ? sortName : filterName));
            var genericMethod = method.MakeGenericMethod(metaData.PrimitiveType, metaData.FilterFieldType);
            return (Expression)genericMethod.Invoke(this, new object[] { (LambdaExpression)node, metaData });
        }

        public virtual Expression HandleSort<TPrimitive, TFiledOfPrimitive>(LambdaExpression node, FilterFieldMetaInfo metaData)
            where TPrimitive : IConvertible, IComparable
            where TFiledOfPrimitive : class, IField<TPrimitive>
        {
            return node;
        }

        public virtual Expression HandleWhere<TPrimitive, TFiledOfPrimitive>(LambdaExpression node, FilterFieldMetaInfo metaData)
            where TPrimitive : IConvertible, IComparable
            where TFiledOfPrimitive : class, IField<TPrimitive>
        {
            return node;
        }
    }
}
