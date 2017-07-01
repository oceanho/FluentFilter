using System;
using System.Linq.Expressions;

using FluentFilter.Inetnal.ExprTreeVisitors;
using OhPrimitives;

namespace FluentFilter.Inetnal.Visitors
{
    /// <summary>
    /// All visitor method should be start with visit
    /// </summary>
    internal  class FilterFieldVisitor
    {
        private InnerMostWhereExpressionFinder _visitor;

        public FilterFieldVisitor()
        {
            _visitor = new InnerMostWhereExpressionFinder();
        }

        public Expression Visit(Expression node, IField filter)
        {
            throw new NotImplementedException();
        }

        public MethodCallExpression GetInnerMostWhere(Expression node)
        {
            var whereExpression = _visitor.GetInnerMostWhereExpression(node);
            if (whereExpression == null)
                throw new ArgumentNullException("Missing WhereExpression");
            return whereExpression;
        }
    }
}
