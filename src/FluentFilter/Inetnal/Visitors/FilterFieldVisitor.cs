using System;
using System.Linq.Expressions;

using FluentFilter.Inetnal.ExprTreeVisitors;

namespace FluentFilter.Inetnal.Visitors
{
    /// <summary>
    /// All visitor method should be start with visit
    /// </summary>
    internal partial class FilterFieldVisitor
    {
        private InnerMostWhereExpressionVisitor _visitor;

        public FilterFieldVisitor()
        {
            _visitor = new InnerMostWhereExpressionVisitor();
        }

        public Expression Visit(Expression node, IFilterField filter)
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
