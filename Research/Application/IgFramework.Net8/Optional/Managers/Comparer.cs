using System;
using System.Collections.Generic;

using System.Globalization;
using System.Windows.Data;

namespace Infragistics.Framework
{
    public static class SortBy
    {
        public static int Ascending(DateTime a, DateTime b)
        {
            return +Comparer<DateTime>.Default.Compare(a.Date, b.Date);
        }
        public static int Descending(DateTime a, DateTime b)
        {
            return -Comparer<DateTime>.Default.Compare(a.Date, b.Date);
        }

        public static int Ascending(double a, double b)
        {
            return +Comparer<double>.Default.Compare(a, b);
        }
        public static int Descending(double a, double b)
        {
            return -Comparer<double>.Default.Compare(a, b);
        }

        public static int Ascending(int a, int b)
        {
            return +Comparer<int>.Default.Compare(a, b);
        }
        public static int Descending(int a, int b)
        {
            return -Comparer<int>.Default.Compare(a, b);
        }
         
        public static int Ascending(string a, string b)
        {
            return +Comparer<string>.Default.Compare(a, b);
        }
        public static int Descending(string a, string b)
        {
            return -Comparer<string>.Default.Compare(a, b);
        }
    }
}
