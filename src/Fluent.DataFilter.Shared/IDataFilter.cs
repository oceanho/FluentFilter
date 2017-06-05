
using System;
using System.Linq.Expressions;

namespace Fluent.DataFilter
{

    public interface IDataFilter
    {
        /// <summary>
        /// Get the entity's element type
        /// </summary>
        Type ElementType { get; }

        /// <summary>
        /// Get all field filters
        /// </summary>
        /// <returns></returns>
        FieldFilterInfo[] GetFieldFilters();
    }

    public interface IDataFilter<TEntity> : IDataFilter
    {
        //TEntity EntityOfFilter { get; set; }
        Expression<Func<TEntity, bool>> ToExpression();
    }
}
