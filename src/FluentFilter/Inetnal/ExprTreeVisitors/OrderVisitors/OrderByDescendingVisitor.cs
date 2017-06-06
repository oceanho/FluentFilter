using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
