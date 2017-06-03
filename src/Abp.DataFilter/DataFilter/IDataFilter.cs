using System;
using System.Linq.Expressions;

namespace Abp.DataFilter.DataFilter
{

    public interface IDataFilter
    {
    }

    public interface IDataFilter<TEntity>:IDataFilter
	{
        Expression<Func<TEntity,bool>> GetExpression();
	}
}
