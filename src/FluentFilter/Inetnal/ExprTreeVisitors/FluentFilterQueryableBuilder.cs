using FluentFilter.Inetnal.ExprTreeVisitors.Modifiers;
using FluentFilter.Inetnal.ImplOfFilter;
using FluentFilter.Inetnal.ImplOfFilter.Utils;
using System;
using System.Linq;
using System.Linq.Expressions;

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
            VisitSort();
            VisitWhere();
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
            var modifier = new WhereTreeModifier(Queryable);
            var filterWhereExpr = DataFilterMetaInfoParser.Parse<TEntity>(Filter, FilterMateInfo, string.Empty);
            modifier.Modify(m_InternalExpr, filterWhereExpr);
            m_InternalExpr = modifier.ModifiedResult;
        }

        protected void VisitSort()
        {
            var modifier = new OrderByTreeModifier(Queryable, FilterMateInfo.FilterFiledsOfSort);
            modifier.Modify(m_InternalExpr, null);
            m_InternalExpr = modifier.ModifiedResult;
        }
    }
}
