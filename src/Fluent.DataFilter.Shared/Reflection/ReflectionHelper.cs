using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Fluent.DataFilter.Relection
{
    public static class ReflectionHelper
    {
        public static IEnumerable<PropertyInfo> GetProperties(object source, Type canAssignType)
        {
            return source.GetType().GetTypeInfo().GetProperties().Where(prop => canAssignType.GetTypeInfo().IsAssignableFrom(prop.PropertyType.GetTypeInfo()));
        }

        public static string GetTypeUniqueName(Type type)
        {
            var typeName = type.FullName;
            if (typeName == null)
            {
                typeName = type.AssemblyQualifiedName;
            }
            if (typeName == null)
            {
                typeName = type.ToString();
            }
            return typeName;
        }
    }
}
