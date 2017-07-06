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
        public FluentFilterQueryableBuilder()
        {
        }

        public FluentFilterQueryableBuilder(IQueryable<TEntity> queryable, IDataFilter filter)
        {
            Filter = filter;
            Queryable = queryable;
        }

        public IDataFilter Filter { get; set; }
        public IQueryable<TEntity> Queryable { get; set; }
        protected DataFilterMetaInfo FilterMateInfo { get; set; }

        public IQueryable<TEntity> Build()
        {
            Init();
            VisitWhere();
            VisitOrderSort();
            return Queryable;
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
                Queryable = Queryable.Where(filterWhereExpression);
            }
            else
            {
                var modifier = new WhereTreeModifier(Queryable);
                modifier.Modify(Queryable.Expression, filterWhereExpression, "Where");
                Queryable = Queryable.Provider.CreateQuery<TEntity>(modifier.Result);
            }
        }

        protected void VisitOrderSort()
        {

        }
    }
}
