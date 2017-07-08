using FluentFilter.Inetnal.ExprTreeVisitors.Modifiers;
using FluentFilter.Inetnal.ImplOfFilter;
using FluentFilter.Inetnal.ImplOfFilter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FluentFilter.Inetnal.ExprTreeVisitors
{
    internal class FluentFilterQueryableBuilder<TEntity>
    {
        private Expression m_InternalExpr;
        private IQueryable<TEntity> m_Queryable;
        public FluentFilterQueryableBuilder()
        {
        }

        public FluentFilterQueryableBuilder(IQueryable<TEntity> queryable, IDataFilter filter)
        {
            Filter = filter;
            Queryable = queryable;
        }

        public IDataFilter Filter { get; set; }
        public IQueryable<TEntity> Queryable
        {
            get
            {
                return m_Queryable;
            }
            set
            {
                m_Queryable = value;
                m_InternalExpr = m_Queryable.Expression;
            }
        }
        protected DataFilterMetaInfo FilterMateInfo { get; set; }

        public IQueryable<TEntity> Build()
        {
            Init();
            VisitWhere();
            VisitOrderSort();
            return Queryable.Provider.CreateQuery<TEntity>(m_InternalExpr);
        }

        private void Init()
        {
            if (Filter == null)
            {
                throw new ArgumentNullException("Filter");
            }
            if (Queryable == null)
            {
                throw new ArgumentNullException("Queryable");
            }
            FilterMateInfo = DataFilterMetaInfoHelper.GetFilterMeatInfo(Filter);
        }

        protected void VisitWhere()
        {
            var innerWhereFinder = new InnerMostWhereExpressionFinder();
            var innerQueryableWhereExpr = innerWhereFinder.GetInnerMostWhereExpression(Queryable.Expression);
            var filterWhereExpression = DataFilterMetaInfoParser.Parse<TEntity>(Filter, FilterMateInfo, innerWhereFinder.ParamterName);
            filterWhereExpression = new ExprTreeOptimizer(filterWhereExpression).Optimize() as Expression<Func<TEntity, bool>>;
            if (innerQueryableWhereExpr == null)
            {
                Queryable = Queryable.Where(filterWhereExpression);//.Expression;
            }
            else
            {
                var modifier = new WhereTreeModifier(Queryable);
                modifier.Modify(m_InternalExpr, filterWhereExpression);
                m_InternalExpr = modifier.ModifiedResult;
            }
        }

        protected void VisitOrderSort()
        {
            var modifier = new OrderByTreeModifier(Queryable, FilterMateInfo.FilterFiledsOfSort);
            modifier.Modify(m_InternalExpr, null);
            m_InternalExpr = modifier.ModifiedResult;
        }
    }
}
