using System.Linq;
using System.Linq.Expressions;

namespace FluentFilter.Inetnal.ExprTreeVisitors.Modifiers
{
    internal abstract class ExprTreeModifier : ExpressionVisitor
    {
        public ExprTreeModifier(IQueryable queryable)
        {
            Queryable = queryable;
        }
        protected IQueryable Queryable { get; }
        protected Expression Right { get; private set; }
        public Expression ModifiedResult { get; private set; }
        
        public void Modify(Expression node, Expression right)
        {
            Right = right;
            ModifiedResult = Visit(node);
        }
    }
}
