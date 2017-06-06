using System;
using System.Linq.Expressions;

namespace FluentFilter.Inetnal.ExprTreeVisitors.OrderVisitors
{
    internal class OrderByDescendingVisitor : OrderByVisitor
    {
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name.Equals("OrderByDescending", StringComparison.OrdinalIgnoreCase))
                OrderByExpression = node;
            Visit(node.Arguments[0]);
            return node;
        }
    }
}
