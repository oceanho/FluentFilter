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
                throw new NotImplementedException();

                //
                // 此处如何实现？
                // 需要达到效果：
                //   node现有一个 Where(p=>p.Id>=0) , 需要把 RightBody 的 Where 追加到 node 的 Where 上（它们的参数名，类型都一样）
                //   比如：node 上的 Where(p=> p.Id>=0) , RightBody 的 Where(p=> p.OrderFee >= 50)
                //   需要把这两个条件合并，最终结果是 node 的 Where(p=> p.Id>=0 && p.OrderFee >=50)

                //var left = (LambdaExpression)(((UnaryExpression)node.Arguments[1]).Operand);
                //var expr = Expression.MakeBinary(ExpressionType.AndAlso, left.Body, RightBody);
                //var lambdaExpr = Expression.Lambda(expr, left.Parameters[0]);
                // return Expression.Call(lambdaExpr, node.Method);
            }
            return base.VisitMethodCall(node);
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
