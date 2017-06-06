
using System;
using System.Linq.Expressions;

namespace Fluent.DataFilter
{
    public interface IDataFilter
    {
    }

    public interface IDataFilter<TFilterEntity> : IDataFilter
        where TFilterEntity : IDataFilter<TFilterEntity>
    {        
    }
}
