using Fluent.DataFilter.Inetnal.ExpressionTreeVisitors;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Fluent.DataFilter.Inetnal.Visitors
{
    /// <summary>
    /// All visitor method should be start with visit
    /// </summary>
    internal partial class FilterFieldVisitor
    {
        private InnerMostWhereExpressioVisitor _visitor;

        public FilterFieldVisitor()
        {
            _visitor = new InnerMostWhereExpressioVisitor();
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
