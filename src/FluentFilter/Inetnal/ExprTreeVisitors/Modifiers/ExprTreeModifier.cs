using System.Linq.Expressions;

namespace FluentFilter.Inetnal.ExprTreeVisitors.Modifiers
{
    internal abstract class ExprTreeModifier : ExpressionVisitor
    {
        public abstract void Accept();
    }
}
