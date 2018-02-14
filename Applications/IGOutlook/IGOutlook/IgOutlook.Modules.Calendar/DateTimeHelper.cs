using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgOutlook.Modules.Calendar
{
    public class DateTimeHelper
    {
        public static DateTime ConvertFromUtcToLocal(DateTime input)
        {
            return new DateTime(input.Year, input.Month, input.Day, input.Hour, input.Minute, input.Second, DateTimeKind.Utc).ToLocalTime();
        }

        public static DateTime ConvertFromLocalToUtc(DateTime input)
        {
            return new DateTime(input.Year, input.Month, input.Day, input.Hour, input.Minute, input.Second, DateTimeKind.Local).ToUniversalTime();
        }

        public static DateTime ResetTimePart(DateTime input)
        {
            return new DateTime(input.Year, input.Month, input.Day, 0, 0, 0);
        }

        public static DateTime ResetDatePart(DateTime input)
        {
            return new DateTime(0, 0, 0, input.Hour, input.Minute, input.Second);
        }
    }
}
