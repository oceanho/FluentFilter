using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FluentFilter.Inetnal.Visitors
{
    internal class DataFilterVisitorProvider
    {
        public Expression Accept(IQueryable query, IDataFilter datafilter)
        {
            return Expression.Constant(true);
        }
    }
}
