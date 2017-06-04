﻿
using System;
using System.Linq.Expressions;

namespace Abp.DataFilter.DataFilter
{

    public interface IDataFilter
    {
        /// <summary>
        /// Get the entity's element type
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        Type ElementType { get; }

        /// <summary>
        /// Get all field filters
        /// </summary>
        /// <returns></returns>
        IFiledFilter[] GetFieldFilters();
    }

    public interface IDataFilter<TEntity> : IDataFilter
    {
        TEntity Entity { get; set; }
        Expression<Func<TEntity, bool>> ToExpression();
    }
}
