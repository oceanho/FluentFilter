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

        public static IReadOnlyList<MappingInfo> Get(string typeUniqueName, Action ifNotFindAction)
        {
            var result = default(List<MappingInfo>);
            var count = 0;
            while (count < 2)
            {
               if(!fieldExprNameMappings.TryGetValue(typeUniqueName, out result))
                {
                    ifNotFindAction?.Invoke();
                    count++;
                    continue;
                }
                break;
            }
            return result?.ToImmutableList();
        }

        public static void Clear()
        {
            fieldExprNameMappings.Clear();
        }
    }
}
