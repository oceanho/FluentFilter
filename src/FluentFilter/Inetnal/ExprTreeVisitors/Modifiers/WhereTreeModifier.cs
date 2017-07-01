using System;
using System.Linq;
using System.Linq.Expressions;

namespace FluentFilter.Inetnal.ExprTreeVisitors.Modifiers
{
    internal class WhereTreeModifier : ExprTreeModifier
    {
        public WhereTreeModifier(IQueryable queryable)
            : base(queryable)
        {
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name.Equals(MethodName, StringComparison.OrdinalIgnoreCase))
            {
                //
                // 此处如何实现？
                // 需要达到效果：
                //   在 node 上现在有一个 Where(p=>p.Id>=0) , 需要把 RightBody 的条件（与node Where 的参数是一样的）追加到 node 的 Where 上

                //var left = (LambdaExpression)(((UnaryExpression)node.Arguments[1]).Operand);
                //var expr = Expression.MakeBinary(ExpressionType.AndAlso, left.Body, RightBody);
                //var lambdaExpr = Expression.Lambda(expr, left.Parameters[0]);
                //this.Visit(lambdaExpr);
                //return Expression.Call(node.Method, Expression.Constant(Queryable), lambdaExpr);
            }
            this.Visit(node.Arguments[0]);
            return node;
        }

        //protected override Expression VisitBinary(BinaryExpression node)
        //{
        //    if (node == whereExpr)
        //    {
        //        return mergeExpr;
        //    }
        //    return base.VisitBinary(node);
        //}

        //protected override Expression VisitLambda<T>(Expression<T> node)
        //{
        //    return base.VisitLambda(node);
        //}
    }
}
