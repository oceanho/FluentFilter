using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using System.Xml.Serialization;

namespace Abp.DataFilter.DataFilter
{

    public interface IDataFilter
    {
        [XmlIgnore]
        [JsonIgnore]
        Type ElementType { get; }
    }

    public interface IDataFilter<TEntity> : IDataFilter
    {
        TEntity Entity { get; set; }
        Expression<Func<TEntity, bool>> GetExpression();
    }
}
