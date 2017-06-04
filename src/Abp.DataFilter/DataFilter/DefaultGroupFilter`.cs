using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abp.DataFilter.DataFilter
{
    public class DefaultGroupFilter<TEntity, TGroupFilter> : DefaultDataFilter<TEntity>, IGroupFilter<TEntity, TGroupFilter>, IDataFilter<TEntity>, IDataFilter
        where TEntity : IDataFilter<TEntity>
        where TGroupFilter : IGroupFilter<TEntity, TGroupFilter>

    {
        public TGroupFilter Left { get; set; }
        public TGroupFilter Right { get; set; }
        public GroupFilterKind Kind { get; set; }
    }
}
