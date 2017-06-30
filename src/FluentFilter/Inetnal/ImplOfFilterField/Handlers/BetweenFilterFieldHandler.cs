using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentFilter.Inetnal.ImplOfFilterField.Handlers
{
    using OhPrimitives;
    using System.Linq.Expressions;
    internal class BetweenFilterFieldHandler : DefaultFilterFieldHandler
    {
        public override Type FilterType => typeof(BetweenField<>);

        public override Expression HandleSort<TPrimitive, TFiledOfPrimitive>(LambdaExpression node, FilterFieldMetaInfo metaData)
        {
            return base.HandleSort<TPrimitive, TFiledOfPrimitive>(node, metaData);
        }

        public override Expression HandleWhere<TPrimitive, TFiledOfPrimitive>(LambdaExpression node, FilterFieldMetaInfo metaData)
        {
            return base.HandleWhere<TPrimitive, TFiledOfPrimitive>(node, metaData);
        }
    }
}
