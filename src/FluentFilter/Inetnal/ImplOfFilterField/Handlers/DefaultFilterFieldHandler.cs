using OhPrimitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;

using OhDotNetLib.Extension;

namespace FluentFilter.Inetnal.ImplOfFilterField.Handlers
{
    using OhDotNetLib.Reflection;
    using OhDotNetLib.Utils;
    using OhPrimitives;

    public abstract class DefaultFilterFieldHandler : IFilterFieldHandler
    {
        private string filterFieldTypeUniqueName = string.Empty;
        private static readonly string filterName = nameof(HandleWhere);
        public DefaultFilterFieldHandler()
        {
        }

        public abstract Type FilterFieldType { get; }

        public virtual String FilterFieldTypeUniqueName
        {
            get
            {
                if (ObjectNullChecker.IsNullOrEmpty(filterFieldTypeUniqueName))
                {
                    filterFieldTypeUniqueName = TypeHelper.GetGenericTypeUniqueName(FilterFieldType);
                }
                return filterFieldTypeUniqueName;
            }
        }

        public Expression Handle(Expression node, FilterFieldMetaInfo metaData)
        {
            if (node.NodeType != ExpressionType.Lambda)
            {
                throw new ArgumentException($"invalid node.NodeType {node.NodeType}. It's should be {ExpressionType.Lambda.ToString()}");
            }

            var method = this.GetType().GetTypeInfo().GetMethod(filterName);
            var genericMethod = method.MakeGenericMethod(metaData.PrimitiveType, metaData.FilterFieldType);
            return (Expression)genericMethod.Invoke(this, new object[] { (LambdaExpression)node, metaData });
        }

        public virtual Expression HandleWhere<TPrimitive, TFiledOfPrimitive>(LambdaExpression node, FilterFieldMetaInfo metaData)
            where TPrimitive : IConvertible, IComparable
            where TFiledOfPrimitive : class, IField<TPrimitive>
        {
            return node;
        }
    }
}
