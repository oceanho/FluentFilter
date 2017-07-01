using System;
using System.Linq.Expressions;

namespace FluentFilter.Inetnal.ExprTreeVisitors
{
    internal class InnerMostWhereExpressionFinder : ExpressionVisitor
    {
        public string ParamterName { get; private set; }
        private MethodCallExpression _innerMostWhereExpression;
        public MethodCallExpression GetInnerMostWhereExpression(Expression node)
        {
            Visit(node);
            if (_innerMostWhereExpression != null)
            {
                var _str = _innerMostWhereExpression.Arguments[1].ToString();
                ParamterName = _str.Substring(0, _str.IndexOf('=')).Replace(" ", "");
            }
            return _innerMostWhereExpression;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name.Equals("Where", StringComparison.OrdinalIgnoreCase))
                _innerMostWhereExpression = node;
            Visit(node.Arguments[0]);
            return node;
        }
    }
}
