
using System;
using System.Linq.Expressions;

namespace FluentFilter
{
    public interface IDataFilter
    {
    }

    public interface IDataFilter<TFilterEntity> : IDataFilter
        where TFilterEntity : IDataFilter<TFilterEntity>
    {        
    }
}
