using System;
using System.Linq.Expressions;

namespace FluentFilter.Inetnal.ExprTreeVisitors
{
    internal class InnerMostWhereExpressionVisitor : ExpressionVisitor
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
