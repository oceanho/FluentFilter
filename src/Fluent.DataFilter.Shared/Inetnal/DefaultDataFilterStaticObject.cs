using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fluent.DataFilter.Inetnal.ImplOfFilterField;

namespace Fluent.DataFilter.Inetnal
{
    internal static class DefaultDataFilterStaticObject
    {
        private static readonly FilterFieldVisitorExecutor _filterFieldVisitorExecutor;
        static DefaultDataFilterStaticObject()
        {
            _filterFieldVisitorExecutor = new FilterFieldVisitorExecutor();
        }

        public static void Execute(FilterFieldVisitorContext context, FilterFieldMetaInfo metaInfo)
        {
            _filterFieldVisitorExecutor.Execute(context, metaInfo);
        }
    }
}
