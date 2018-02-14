using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace IgOutlook.Modules.Mail.Converters
{
    public class MailDateGroupByConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return MailDateGroupByComparer.GetGroupString((DateTime)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MailDateGroupByComparer : IEqualityComparer<DateTime>
    {

        public bool Equals(DateTime x, DateTime y)
        {
            return true;
        }

        public int GetHashCode(DateTime obj)
        {
            return GetGroupString(obj).GetHashCode();
        }

        public static string GetGroupString(DateTime obj)
        {
            if (obj.Date == DateTime.Now.Date)
                return "Today";

            if (obj.Date == DateTime.Now.Date.AddDays(-1)
                && ((int)DateTime.Now.DayOfWeek > 1 || (int)DateTime.Now.DayOfWeek == 0))
                return "Yesterday";


            if ((int)DateTime.Now.Date.DayOfWeek > 2)
            {
                for (int i = 1; i <= (int)DateTime.Now.DayOfWeek - 2; i++)
                {
                    DateTime compareDate = DateTime.Now.Date.AddDays(-(i + 1));
                    if (obj.Date == compareDate)
                        return compareDate.DayOfWeek.ToString();
                }
            }

            if ((int)DateTime.Now.Date.DayOfWeek == 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    DateTime compareDate = DateTime.Now.Date.AddDays(-(i + 1));
                    if (obj.Date == compareDate)
                        return compareDate.DayOfWeek.ToString();
                }
            }


            int daysToRemove = Math.Abs((int)DateTime.Now.DayOfWeek - 1);

            DateTime lastWeekEnd = DateTime.Now.Date.AddDays(-daysToRemove);
            DateTime lastWeekStart = lastWeekEnd.AddDays(-7);

            if (obj >= lastWeekStart && obj < lastWeekEnd)
                return "Last Week";

            if (obj >= lastWeekStart.AddDays(-7) && obj < lastWeekStart)
                return "Two Weeks Ago";

            if (obj > lastWeekStart.AddDays(-14) && obj < lastWeekStart.AddDays(-7))
                return "Three Weeks Ago";

            if (obj.Month == DateTime.Now.Month - 1)
                return "Last Month";

            return "Older";
        }
    }
}
