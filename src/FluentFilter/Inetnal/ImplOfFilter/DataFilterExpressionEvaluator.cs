using FluentFilter.Inetnal.ExprTreeVisitors;
using FluentFilter.Inetnal.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FluentFilter.Inetnal.ImplOfFilter
{
    internal class DataFilterExpressionEvaluator
    {
        private IQueryable _query;
        private IDataFilter _datafilter;

        private InnerMostWhereExpressionVisitor _whereVisitor;
        private OrderByExpressionVisitorFacde _orderByVisitorFacde;

        public DataFilterExpressionEvaluator(IQueryable query, IDataFilter datafilter)
        {
            _query = query;
            _datafilter = datafilter;
            _whereVisitor = new InnerMostWhereExpressionVisitor();
            _orderByVisitorFacde = new OrderByExpressionVisitorFacde();
        }
        public Expression Eval()
        {
            var orderExpression = _orderByVisitorFacde.Visit(_query.Expression);
            var whereExpression = _whereVisitor.GetInnerMostWhereExpression(_query.Expression);
            return _query.Expression;
        }
    }
}
