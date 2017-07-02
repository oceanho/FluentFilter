using FluentFilter.Inetnal.ImplOfFilterField.Utils;
using System.Linq;

namespace FluentFilter.Inetnal.ImplOfFilter.Utils
{
    internal static class DataFilterMetaInfoHelper
    {
        public static DataFilterMetaInfo GetFilterMeatInfo(IDataFilter filter)
        {
            return new DataFilterMetaInfo(filter, FilterFieldMetaInfoHelper.GetFilterFields(filter).Where(filterField => filterField.FilterFieldInstace.IsSatisfy()));
        }
    }
}
