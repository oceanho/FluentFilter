using FluentFilter.Inetnal.ImplOfFilterField;
using FluentFilter.Inetnal.ImplOfFilterField.Internal;
using OhDotNetLib.Linq;
using OhDotNetLib.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

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
                expr = new SubTreeModifier(Queryable).InitWhere(expr, Right);
            return expr;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (m_ismodified || !(node.Method.Name.Equals("Where", StringComparison.OrdinalIgnoreCase)))
            {
                return node;
            }

            m_ismodified = true;
            var parameter = PredicateBuilder.Paramter(Queryable.ElementType);
            var mi = node.Method;
            var obj = Visit(node.Object);
            return Expression.Call(obj, mi, node, Right);
        }

        public class SubTreeModifier : ExpressionVisitor
        {
            private bool m_ismodified;
            private IQueryable m_queryable;
            private Expression m_conditionalExpr;
            public SubTreeModifier(IQueryable queryable)
            {
                m_queryable = queryable;
                m_ismodified = false;
            }

            public Expression InitWhere(Expression node, Expression conditionalExpr)
            {
                m_conditionalExpr = conditionalExpr;
                return Visit(node);
            }

            protected override Expression VisitConstant(ConstantExpression node)
            {
                if (m_ismodified)
                    return node;
                var typeInfo = node.Type.GetTypeInfo();
                if (typeInfo.IsGenericType&& typeInfo.GetGenericArguments().Contains(m_queryable.ElementType))
                {
                        var p = PredicateBuilder.Paramter(m_queryable.ElementType);
                        var mi = QueryableMethods.Where.MakeGenericMethod(m_queryable.ElementType);
                        return Expression.Call(null, mi, node, m_conditionalExpr);
                }
                return node;
            }
        }
    }
}
