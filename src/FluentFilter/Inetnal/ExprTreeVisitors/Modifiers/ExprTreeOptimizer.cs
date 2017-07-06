using System.Linq;
using System.Linq.Expressions;

namespace FluentFilter.Inetnal.ExprTreeVisitors.Modifiers
{
    internal class ExprTreeOptimizer : ExpressionVisitor
    {
        private Expression m_expr;
        public ExprTreeOptimizer(Expression expr)
        {
            m_expr = expr;
        }

        public Expression Optimize()
        {
            return Visit(m_expr);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.NodeType == ExpressionType.AndAlso)
            {
                return DeleteTrueExpr(node);
            }
            return base.VisitBinary(node);
        }

        private Expression DeleteTrueExpr(BinaryExpression node)
        {
            var constExpr = node.Left as ConstantExpression;
            var constExprValue = false;
            if (constExpr != null && constExpr.Value != null && constExpr.Value.GetType() == typeof(bool))
            {
                constExprValue = (bool)constExpr.Value;
            }
            if (constExprValue == true)
            {
                return node.Right;
            }

            constExprValue = false;
            constExpr = node.Right as ConstantExpression;
            if (constExpr != null && constExpr.Value != null && constExpr.Value.GetType() == typeof(bool))
            {
                constExprValue = (bool)constExpr.Value;
            }
            if (constExprValue == true)
            {
                return node.Left;
            }
            return base.VisitBinary(node);
        }
    }
}
