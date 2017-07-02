using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Concurrent;
using System.Collections.Immutable;

namespace FluentFilter.Mappings
{
    internal static class FieldExprNameMappingFactory
    {
        private static object safeAddLocker = new object();
        private static ConcurrentDictionary<string, List<MappingInfo>> fieldExprNameMappings;
        static FieldExprNameMappingFactory()
        {
            fieldExprNameMappings = new ConcurrentDictionary<string, List<MappingInfo>>();
        }

        public static void Add(string typeUniqueName, Action<List<MappingInfo>> addFunc)
        {
            if (!fieldExprNameMappings.ContainsKey(typeUniqueName))
                fieldExprNameMappings.TryAdd(typeUniqueName, new List<MappingInfo>());
            addFunc(fieldExprNameMappings[typeUniqueName]);
        }

        public static IReadOnlyList<MappingInfo> Get(string typeUniqueName, Func<List<MappingInfo>> ifNotFindFunc)
        {
            var result = default(List<MappingInfo>);
            if (!fieldExprNameMappings.TryGetValue(typeUniqueName, out result))
            {
                result = ifNotFindFunc();
                fieldExprNameMappings[typeUniqueName] = result;
            }
            return result?.ToImmutableList();
        }

        public static void Clear()
        {
            fieldExprNameMappings.Clear();
        }
    }
}
