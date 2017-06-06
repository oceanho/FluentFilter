using FluentFilter.Inetnal.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FluentFilter.Inetnal.ImplOfFilter
{
    internal class DataFilterExpressionBuilder
    {
        private IQueryable _query;

        private IDataFilter _datafilter;
        private DataFilterVisitor _datafilterVisitor;
        public DataFilterExpressionBuilder(IQueryable query,IDataFilter datafilter)
        {
            _query = query;
            _datafilter = datafilter;
            _datafilterVisitor = new DataFilterVisitor();
        }

        protected IQueryable Query { get => _query; }

        public Expression Build()
        {
            throw new NotImplementedException();
        }
    }
}
