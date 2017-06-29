using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Concurrent;

namespace FluentFilter.Mappings
{
    internal static class FieldExprNameMappingFactory
    {
        private static ConcurrentDictionary<string, List<MappingInfo>> fieldExprNameMappings;
        static FieldExprNameMappingFactory()
        {
            fieldExprNameMappings = new ConcurrentDictionary<string, List<MappingInfo>>();
        }

        public static void Add(string typeUniqueName, Action<List<MappingInfo>> addFunc)
        {
            if (!fieldExprNameMappings.ContainsKey(typeUniqueName))
            {
                fieldExprNameMappings.TryAdd(typeUniqueName, new List<MappingInfo>());
            }
            addFunc(fieldExprNameMappings[typeUniqueName]);
        }

        public static void Clear()
        {
            fieldExprNameMappings.Clear();
        }
    }
}
