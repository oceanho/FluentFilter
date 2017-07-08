using OhPrimitives;
using System;

namespace FluentFilter.Inetnal.ImplOfFilterField
{
    /// <summary>
    /// Filter字段元数据信息载体对象类
    /// </summary>
    public abstract class FilterFieldMetaInfo
    {
        internal FilterFieldMetaInfo(IField filterField, string fieldExprName, Type filterFieldOfElementBinderType)
        {
            FilterFieldName = fieldExprName;
            FilterFieldInstace = filterField;
            FilterFieldOfElementBinderType = filterFieldOfElementBinderType;
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
        public virtual string FilterFieldName { get; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IField FilterFieldInstace { get; }

        /// <summary>
        /// 
        /// </summary>
        public Type FilterFieldOfElementBinderType { get; }
    }
}
