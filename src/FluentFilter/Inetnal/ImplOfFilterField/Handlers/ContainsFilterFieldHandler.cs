using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentFilter.Inetnal.ImplOfFilterField.Handlers
{
    using OhPrimitives;
    using System.Linq.Expressions;
    internal class ContainsFilterFieldHandler : DefaultFilterFieldHandler
    {
        private static readonly Type filterFieldType = typeof(ContainsField<>);
        public override Type FilterFieldType => filterFieldType;
    }
}
