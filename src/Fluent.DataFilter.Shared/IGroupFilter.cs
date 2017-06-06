using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fluent.DataFilter
{
    public interface IGroupFilter<TDataFilterEntity, TGroupFilter> : IDataFilter<TDataFilterEntity>, IDataFilter
        where TDataFilterEntity : IDataFilter<TDataFilterEntity>
        where TGroupFilter : IGroupFilter<TDataFilterEntity, TGroupFilter>
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
        GroupFilterKind Kind { get; set; }
    }
}
