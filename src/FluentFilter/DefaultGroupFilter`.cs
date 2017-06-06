using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentFilter
{
    public class DefaultGroupFilter<TEntity, TFilterEntity, TGroupFilterEntity> : DefaultDataFilter<TEntity, TFilterEntity>, IGroupFilter<TEntity, TGroupFilterEntity>, IDataFilter<TEntity>, IDataFilter
        where TEntity : IDataFilter<TEntity>
        where TFilterEntity : IDataFilter<TFilterEntity>
        where TGroupFilterEntity : IGroupFilter<TEntity, TGroupFilterEntity>

    {
        public GroupFilterKind Kind { get; set; }
        public TGroupFilterEntity Left { get; set; }
        public TGroupFilterEntity Right { get; set; }
    }
}
