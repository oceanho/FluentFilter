using System;
using System.Linq.Expressions;

namespace Fluent.DataFilter
{
    public abstract class FilterField : IFilterField
    {
        private Type _selfType;
        public FilterField()
        {
        }

        public virtual Type FilterFieldType
        {
            get
            {
                if (_selfType == null)
                {
                    _selfType = this.GetType();
                }
                return _selfType;
            }
        }

        public abstract bool IsSatisfy();
    }
}
