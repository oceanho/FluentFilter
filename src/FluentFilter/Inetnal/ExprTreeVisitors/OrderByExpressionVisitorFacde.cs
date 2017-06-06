
using System.Linq.Expressions;
using FluentFilter.Inetnal.ExprTreeVisitors.OrderVisitors;

namespace FluentFilter.Inetnal.ExprTreeVisitors
{
    internal class OrderByExpressionVisitorFacde
    {
        private ThenByVisitor _thenByVisitor;
        private OrderByVisitor _orderByAscVisitor;
        private OrderByDescendingVisitor _orderByDescVisitor;
        public OrderByExpressionVisitorFacde()
        {
            _thenByVisitor = new ThenByVisitor();
            _orderByAscVisitor = new OrderByVisitor();
            _orderByDescVisitor = new OrderByDescendingVisitor();
        }

        public Expression Visit(Expression node)
        {
            var _thenByVisitorR = _thenByVisitor.GetOrderExpression(node);
            var _orderByAscVisitorR = _orderByAscVisitor.GetOrderExpression(node);
            var _orderByDescVisitorR = _orderByDescVisitor.GetOrderExpression(node);

            return node;
        }
    }
}
