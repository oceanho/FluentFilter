using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentFilter.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToDayOfStart(this DateTime source)
        {
            return new DateTime(source.Year, source.Month, source.Day, 00, 00, 00);
        }

        public static DateTime ToDayOfFinish(this DateTime source)
        {
            return new DateTime(source.Year, source.Month, source.Day, 23, 59, 59);
        }
    }
}
