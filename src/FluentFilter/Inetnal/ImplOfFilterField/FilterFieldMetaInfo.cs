using System;

namespace FluentFilter.Inetnal.ImplOfFilterField
{
    internal abstract class FilterFieldMetaInfo
    {
        public FilterFieldMetaInfo(IFilterField filterField)
        {
            FilterFieldInstace = filterField;
        }

        /// <summary>
        /// Get an primitive type of field, eg: Int32,Int64,DateTime and so so .
        /// </summary>
        public abstract Type PrimitiveType { get; }

        /// <summary>
        /// 
        /// </summary>
        public abstract Type FilterFieldType { get; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IFilterField FilterFieldInstace { get; }
    }
}
