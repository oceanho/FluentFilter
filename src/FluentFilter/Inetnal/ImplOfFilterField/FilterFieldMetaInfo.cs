using System;

namespace FluentFilter.Inetnal.ImplOfFilterField
{
    internal abstract class FilterFieldMetaInfo
    {
        /// <summary>
        /// Get an type of field, eg: Int32,Int64,DateTime and so so .
        /// </summary>
        public abstract Type FieldType { get; }
        public abstract Type FilterFieldType { get; }
        public abstract IFilterField FilterFieldInstace { get; }
    }
}
