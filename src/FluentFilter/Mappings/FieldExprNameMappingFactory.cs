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
        private static readonly object safeAddLocker = new object();
        private static readonly ConcurrentDictionary<string, List<MappingInfo>> fieldExprNameMappings;
        static FieldExprNameMappingFactory()
        {
            fieldExprNameMappings = new ConcurrentDictionary<string, List<MappingInfo>>();
        }

        public static List<MappingInfo> Add(string typeUniqueName, IEnumerable<MappingInfo> mappings)
        {
            return fieldExprNameMappings.AddOrUpdate(typeUniqueName, mappings.ToList(), (name, old) =>
            {
                var newList = mappings.ToList();
                newList.AddRange(old);
                return newList.Distinct().ToImmutableList().ToList();
            });
        }

        public static IReadOnlyList<MappingInfo> Get(string typeUniqueName, Func<List<MappingInfo>> ifNotFindFunc)
        {
            var result = default(List<MappingInfo>);
            if (!fieldExprNameMappings.TryGetValue(typeUniqueName, out result))
            {
                result = Add(typeUniqueName, ifNotFindFunc());
            }
            return result?.ToImmutableList();
        }

        public static void Clear()
        {
            fieldExprNameMappings.Clear();
        }
    }
}
