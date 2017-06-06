using System.Linq;
using System.Linq.Expressions;

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
