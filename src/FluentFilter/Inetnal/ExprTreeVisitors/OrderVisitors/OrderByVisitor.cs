using System;
using System.Linq.Expressions;

namespace FluentFilter.Inetnal.ExprTreeVisitors.OrderVisitors
{
    internal class OrderByVisitor : ExpressionVisitor
    {
        protected MethodCallExpression OrderByExpression;

        public Expression GetOrderExpression(Expression node)
        {
            Visit(node);
            return OrderByExpression;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name.Equals("ThenBy", StringComparison.OrdinalIgnoreCase))
                OrderByExpression = node;
            Visit(node.Arguments[0]);
            return node;
        }
    }
}
