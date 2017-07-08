using OhPrimitives;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace FluentFilter.Inetnal.ImplOfFilterField
{
    /// <summary>
    /// Filter字段元数据信息载体对象类
    /// </summary>
    public abstract class FilterFieldMetaInfo
    {
        internal FilterFieldMetaInfo(
            IField filterField,
            string fieldExprName,
            IEnumerable<Attribute> fieldAttributes)
        {
            FilterFieldInstace = filterField;
            FilterFieldExprName = fieldExprName;
            FieldAttributes = fieldAttributes.ToImmutableList();
        }

        /// <summary>
        /// Get an primitive type of field, eg: Int32,Int64,DateTime and so so .
        /// </summary>
        public abstract Type PrimitiveType { get; }

        /// <summary>
        /// 过滤器字段类型，比如 <see cref="LikeField"/>, <see cref="CompareField{T}"/>
        /// </summary>
        public abstract Type FilterFieldType { get; }

        /// <summary>
        /// 过滤器字段映射到 Expression 上的属性名称，比如 Id, OrderId, Address.CountryId 等等
        /// </summary>
        public virtual string FilterFieldExprName { get; }

        /// <summary>
        /// 过滤器字段实例对象，比如  <see cref="LikeField"/> 实例
        /// </summary>
        public virtual IField FilterFieldInstace { get; }

        /// <summary>
        /// 过滤器字段上的 <see cref="Attribute"/> 扩展标记，用于记录扩展信息，比如字段的禁用情况 <see cref="Mappings.DisableFieldExprAttribute"/> 等等
        /// </summary>
        public IReadOnlyList<Attribute> FieldAttributes { get;  }
    }
}
