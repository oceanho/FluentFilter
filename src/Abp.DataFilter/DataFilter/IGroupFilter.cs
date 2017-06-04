using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abp.DataFilter.DataFilter
{
    public interface IGroupFilter<TEntity,TGroupFilter> : IDataFilter<TEntity>, IDataFilter
        where TGroupFilter:IGroupFilter<TEntity,TGroupFilter>

    {
        /// <summary>
        /// This is left group filter
        /// </summary>
        TGroupFilter Left { get; set; }

        /// <summary>
        /// This is right group filter
        /// </summary>
        TGroupFilter Right { get; set; }
    
        /// <summary>
        /// This is a operation tag to concat Left and Right filter
        /// </summary>
        GroupFilterFlag Flag { get; set; }
    }
}
