﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentFilter.Inetnal.ImplOfFilterField.Handlers
{
    using OhPrimitives;
    using System.Linq.Expressions;
    internal class RangeFilterFieldHandler : DefaultFilterFieldHandler<IRangeField>
    {
        public Expression Handle(Expression node, FilterFieldData<IRangeField> metaData)
        {
            return node;
        }

        public override Expression HandleSort<TPrimitive, TFiledOfPrimitive>(Expression node, FilterFieldMetaInfo metaData)
        {
            return base.HandleSort<TPrimitive, TFiledOfPrimitive>(node, metaData);
        }

        public override Expression HandleWhere<TPrimitive, TFiledOfPrimitive>(Expression node, FilterFieldMetaInfo metaData)
        {
            return base.HandleWhere<TPrimitive, TFiledOfPrimitive>(node, metaData);
        }
    }
}
