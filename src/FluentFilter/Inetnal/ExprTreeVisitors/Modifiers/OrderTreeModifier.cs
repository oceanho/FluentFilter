using System;
using System.Linq;
using System.Linq.Expressions;

namespace FluentFilter.Inetnal.ExprTreeVisitors.Modifiers
{
    internal class OrderTreeModifier : ExprTreeModifier
    {
        public OrderTreeModifier(IQueryable queryable) : base(queryable)
        {
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            return base.VisitMethodCall(node);
        }
    }
}
