using OhPrimitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;


namespace FluentFilter
{
    using FluentFilter.Inetnal.ImplOfFilterField;
    using OhDotNetLib.Linq;
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
                throw new ArgumentException($"invalid node.NodeType {node.NodeType}. It's should be ExpressionType.Lambda");
            }

            var method = GetType().GetTypeInfo().GetMethod(filterName);
            var genericMethod = method.MakeGenericMethod(metaData.PrimitiveType, metaData.FilterFieldType);

            var lambdaExpr = node as LambdaExpression;
            var parameter = lambdaExpr.Parameters[0];
            Expression memberAccess = null;
            foreach (var property in metaData.FilterFieldName.Split('.'))
            {
                memberAccess = Expression.Property(memberAccess ?? (parameter as Expression), property);
            }
            return (Expression)genericMethod.Invoke(this, new object[] { lambdaExpr, memberAccess, parameter, metaData });
        }

        public virtual Expression HandleWhere<TPrimitive, TFluentFilterFiled>(LambdaExpression node, Expression memberAccessExpr, Expression parameterExpr, FilterFieldMetaInfo metaData)
            where TPrimitive : IConvertible, IComparable
            where TFluentFilterFiled : class, IField<TPrimitive>
        {
            return node;
        }

    }
}
