using FluentFilter.Inetnal.ImplOfFilterField;
using OhDotNetLib.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FluentFilter.Inetnal.ExprTreeVisitors.Modifiers
{
    internal class WhereTreeModifier : ExprTreeModifier
    {
        private bool m_ismodified = false;
        public WhereTreeModifier(IQueryable queryable)
            : base(queryable)
        {
        }

        public override Expression Visit(Expression node)
        {
            var expr = base.Visit(node);
            if (!m_ismodified)
                expr = new SubTreeModifier(Queryable).InitWhere(node);
            return expr;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (m_ismodified)
            {
                return base.VisitMethodCall(node);
            }

            if (node.Method.Name.Equals("Where", StringComparison.OrdinalIgnoreCase))
            {
                m_ismodified = true;
                var parameter = PredicateBuilder.Paramter(Queryable.ElementType);
                var mi = node.Method;
                var obj = Visit(node.Object);

                // 替换node，在后面追加Where条件
                return Expression.Call(obj, mi, node, Right);
            }
            return base.VisitMethodCall(node);
        }

        public class SubTreeModifier : ExpressionVisitor
        {
            private IQueryable m_queryable;
            public SubTreeModifier(IQueryable queryable)
            {
                m_queryable = queryable;
            }

            public Expression InitWhere(Expression node)
            {
                return Visit(node);
            }

            protected override Expression VisitBinary(BinaryExpression node)
            {
                return base.VisitBinary(node);
            }
        }
    }
}
