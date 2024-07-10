using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWillGo.Search.SearchOptions
{
    public static class ExtensionMethods
    {
        public static DateTime? TryParseToNullableDateTime(this string dateString)
        {
            DateTime date;
            return DateTime.TryParse(dateString, out date) ? date : (DateTime?)null;
        }

        public static int? ToNullableInt(this int intValue)
        {
            int? ret;
            ret = intValue;
            return ret;
        }
    }
}
