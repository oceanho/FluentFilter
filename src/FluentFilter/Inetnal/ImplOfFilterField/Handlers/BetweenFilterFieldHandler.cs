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
        private static readonly Type filterFieldType = typeof(BetweenField<>);
        public override Type FilterFieldType => filterFieldType;

        public override Expression HandleWhere<TPrimitive, TFiledOfPrimitive>(LambdaExpression node, FilterFieldMetaInfo metaData)
        {
            return base.HandleWhere<TPrimitive, TFiledOfPrimitive>(node, metaData);
        }
    }
}
