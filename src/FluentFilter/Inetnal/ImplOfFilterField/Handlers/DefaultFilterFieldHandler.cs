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
        public abstract Type FilterType { get; }

        public Expression Handle(Expression node, FilterFieldMetaInfo metaData)
        {
            MethodInfo method = GetType().GetTypeInfo().GetMethod(nameof(HandleWhere));

            //if (metaData.FilterFieldInstace is IHasSortField)
            //{
            //    method = GetType().GetTypeInfo().GetMethod(nameof(HandleSort));
            //}
            //else
            //{
            //    method = GetType().GetTypeInfo().GetMethod(nameof(HandleWhere));
            //}

            var genericMethod = method.MakeGenericMethod(metaData.PrimitiveType, metaData.FilterFieldType);
            return (Expression)genericMethod.Invoke(this, new object[] { node, metaData });
        }

        public virtual Expression HandleSort<TPrimitive, TFiledOfPrimitive>(Expression node, FilterFieldMetaInfo metaData)
            where TPrimitive : IConvertible, IComparable
            where TFiledOfPrimitive : class, IField<TPrimitive>
        {
            return node;
        }

        public virtual Expression HandleWhere<TPrimitive, TFiledOfPrimitive>(Expression node, FilterFieldMetaInfo metaData)
            where TPrimitive : IConvertible, IComparable
            where TFiledOfPrimitive : class, IField<TPrimitive>
        {
            return node;
        }
    }
}
