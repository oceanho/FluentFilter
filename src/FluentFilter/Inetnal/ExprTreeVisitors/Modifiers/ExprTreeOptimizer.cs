using System.Linq;
using System.Linq.Expressions;

namespace FluentFilter.Inetnal.ExprTreeVisitors.Modifiers
{
    internal class ExprTreeOptimizer : ExpressionVisitor
    {
        private Expression m_rawExpr;
        public ExprTreeOptimizer(Expression expr)
        {
            m_rawExpr = expr;
        }

        public Expression Optimize()
        {
            return Visit(m_rawExpr);
        }

        protected override Expression VisitBlock(BlockExpression node)
        {
            // if(node.)
            return base.VisitBlock(node);
        }
    }
}
