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

    public class DefaultFilterFieldHandler<TFiled> : IFilterFieldHandler<TFiled>
        where TFiled : IField
    {
        public Expression Handle(Expression node, FilterFieldMetaInfo metaData)
        {
            MethodInfo method = null;
            if (metaData.FilterFieldInstace is IHasSortField)
            {
                method = GetType().GetTypeInfo().GetMethod(nameof(HandleSort));
            }
            else
            {
                method = GetType().GetTypeInfo().GetMethod(nameof(HandleWhere));
            }

            var genTypes = method.GetGenericArguments();
            var realTypes = new Type[method.GetGenericArguments().Count()];

            var filedType = genTypes[0].MakeGenericType(metaData.PrimitiveType);
            realTypes[0] = filedType;
            realTypes[1] = metaData.PrimitiveType;

            var genericMethod = method.MakeGenericMethod(realTypes);
            return (Expression)genericMethod.Invoke(this, new object[] { node, metaData });
        }

        public virtual Expression HandleSort<TPrimitive, TFiledOfPrimitive>(Expression node, FilterFieldMetaInfo metaData)
            where TFiledOfPrimitive : class, TFiled, IField<TPrimitive>
        {
            return node;
        }

        public virtual Expression HandleWhere<TPrimitive, TFiledOfPrimitive>(Expression node, FilterFieldMetaInfo metaData)
            where TFiledOfPrimitive : class, TFiled, IField<TPrimitive>
        {
            return node;
        }
    }
}
