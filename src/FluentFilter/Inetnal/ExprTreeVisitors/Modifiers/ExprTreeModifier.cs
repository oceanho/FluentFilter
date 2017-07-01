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
        public Expression Result { get; private set; }
        protected string MethodName { get; private set; }
        protected Expression Right { get; private set; }
        protected Expression RightBody { get; private set; }


        public void Accept(Expression node, LambdaExpression right, string methodName)
        {
            Right = right;
            RightBody = right.Body;
            MethodName = methodName;

            Result = Visit(node);
        }
    }
}
