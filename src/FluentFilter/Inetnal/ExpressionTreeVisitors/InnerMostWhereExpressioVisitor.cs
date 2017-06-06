using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FluentFilter.Inetnal.ExpressionTreeVisitors
{
    internal class InnerMostWhereExpressioVisitor : ExpressionVisitor
    {
        private MethodCallExpression _innerMostWhereExpression;
        public MethodCallExpression GetInnerMostWhereExpression(Expression node)
        {
            Visit(node);
            if (_innerMostWhereExpression == null)
                _innerMostWhereExpression = null;
            return _innerMostWhereExpression;
        }
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name.Equals("where", StringComparison.OrdinalIgnoreCase))
                _innerMostWhereExpression = node;
            Visit(node.Arguments[0]);
            return node;
        }
    }
}
