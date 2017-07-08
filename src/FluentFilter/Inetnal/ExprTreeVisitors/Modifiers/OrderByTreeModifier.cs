using FluentFilter.Inetnal.ImplOfFilterField;
using OhDotNetLib.Linq;
using OhPrimitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentFilter.Inetnal.ExprTreeVisitors.Modifiers
{
    internal class OrderByTreeModifier : ExprTreeModifier
    {
        private readonly List<String> m_MethodNameList = new List<string>() {
            "OrderBy",
            "ThenBy",
            "OrderByDescending"
        };

        private bool m_IsModified = false;

        private List<FilterFieldSortMetaInfo> m_SortedFilterFieldMetaInfos;

        public OrderByTreeModifier(IQueryable queryable, IEnumerable<FilterFieldSortMetaInfo> sortedFilterFieldMetaInfos)
            : base(queryable)
        {
            m_SortedFilterFieldMetaInfos = sortedFilterFieldMetaInfos.ToList();
        }

        public override Expression Visit(Expression node)
        {
            if (m_IsModified || m_SortedFilterFieldMetaInfos?.Count == 0)
            {
                return node;
            }

            var expr = base.Visit(node);
            if (!m_IsModified)
                expr = new SubTreeModifier(Queryable).InitSort(expr, m_SortedFilterFieldMetaInfos);
            return expr;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (m_IsModified || m_SortedFilterFieldMetaInfos?.Count == 0)
                return base.VisitMethodCall(node);

            if (m_MethodNameList.FirstOrDefault(m => node.Method.Name.Equals(m, StringComparison.OrdinalIgnoreCase)) != null)
            {
                m_IsModified = true;
                Expression result = node;
                var methodName = "ThenBy";
                foreach (var item in m_SortedFilterFieldMetaInfos)
                {
                    Expression memberAccess = null;
                    var parameter = Expression.Parameter(Queryable.ElementType, "p");
                    foreach (var property in item.FilterFieldName.Split('.'))
                    {
                        memberAccess = Expression.Property(memberAccess ?? (parameter as Expression), property);
                    }

                    if (item.FilterFieldInstace.SortMode == SortMode.Asc)
                    {
                        methodName = "ThenBy";
                    }
                    else if (item.FilterFieldInstace.SortMode == SortMode.Desc)
                    {
                        methodName = "ThenByDescending";
                    }
                    LambdaExpression orderByLambda = Expression.Lambda(memberAccess, parameter);
                    result = Expression.Call(
                        typeof(Queryable),
                        methodName,
                        new[] { Queryable.ElementType, memberAccess.Type },
                        result,
                        Expression.Quote(orderByLambda));
                }
                return result;
            }
            return base.VisitMethodCall(node);
        }

        public class SubTreeModifier : ExpressionVisitor
        {
            private bool m_ismodified;
            private IQueryable m_queryable;
            private List<FilterFieldSortMetaInfo> m_SortedFilterFieldMetaInfos;
            public SubTreeModifier(IQueryable queryable)
            {
                m_ismodified = false;
                m_queryable = queryable;
            }

            public Expression InitSort(Expression node, IEnumerable<FilterFieldSortMetaInfo> sortedFilterFieldMetaInfos)
            {
                m_SortedFilterFieldMetaInfos = sortedFilterFieldMetaInfos.ToList();
                return Visit(node);
            }
            protected override Expression VisitConstant(ConstantExpression node)
            {
                if (m_ismodified)
                {
                    return node;
                }

                var typeInfo = node.Type.GetTypeInfo();
                if (typeInfo.IsGenericType && typeInfo.GetGenericArguments().Contains(m_queryable.ElementType))
                {
                    m_ismodified = true;
                    Expression result = node;
                    var methodName = "OrderBy";
                    var isFirst = true;
                    var p = PredicateBuilder.Paramter(m_queryable.ElementType);
                    foreach (var item in m_SortedFilterFieldMetaInfos)
                    {
                        Expression memberAccess = null;
                        var parameter = PredicateBuilder.Paramter(m_queryable.ElementType);
                        foreach (var property in item.FilterFieldName.Split('.'))
                        {
                            memberAccess = Expression.Property(memberAccess ?? (parameter as Expression), property);
                        }

                        if (item.FilterFieldInstace.SortMode == SortMode.Asc)
                        {
                            if (!isFirst)
                                methodName = "ThenBy";
                        }
                        else if (item.FilterFieldInstace.SortMode == SortMode.Desc)
                        {
                            methodName = isFirst ? "OrderByDescending" : "ThenByDescending";
                        }
                        isFirst = false;

                        LambdaExpression orderByLambda = Expression.Lambda(memberAccess, parameter);
                        result = Expression.Call(
                            typeof(Queryable),
                            methodName,
                            new[] { m_queryable.ElementType, memberAccess.Type },
                            result,
                            Expression.Quote(orderByLambda));
                    }
                    return result;
                }
                return base.VisitConstant(node);
            }
        }
    }
}
