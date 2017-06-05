using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluent.DataFilter.Inetnal
{
    internal static class DefaultDataFilterStaticObject
    {
        private static readonly FilterFieldVisitorExecutor _filterFieldVisitorExecutor;
        static DefaultDataFilterStaticObject()
        {
            _filterFieldVisitorExecutor = new FilterFieldVisitorExecutor();
        }

        public static void Execute(FilterFieldVisitorContext context, FieldFilterInfo fieldFilterInfo)
        {
            _filterFieldVisitorExecutor.Execute(context, fieldFilterInfo);
        }
    }
}
