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

        protected override Expression VisitBlock(BlockExpression node)
        {
            // if(node.)
            return base.VisitBlock(node);
        }        
    }
}
