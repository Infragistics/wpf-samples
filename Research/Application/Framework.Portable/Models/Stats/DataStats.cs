using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Infragistics.Framework 
{
    public class DataStats
    {
        public DataStats()
        {
            Columns = new Dictionary<string, DataColumn>();
        }

        public Dictionary<string, DataColumn> Columns { get; set; }
        public int DataCount { get; internal set; }
        public Type DataType { get; internal set; }
        public Type ItemType { get; internal set; }

        public static DataStats Create(object items)
        {
            var stats = new DataStats();

            stats.DataCount = 0;
            stats.Columns = new Dictionary<string, DataColumn>();

            if (items == null) return stats;

            stats.DataType = items.GetType();

            var list = items as IEnumerable<object>;

            if (list == null) return stats;
            if (list.Count() == 0) return stats;

            var item = list.ElementAt(0);
            stats.DataCount = list.Count();
            stats.ItemType = item.GetType();

            var properties = item.GetProperties();
            foreach (var p in properties)
            {
                if (p.PropertyType == typeof(double) ||
                    p.PropertyType == typeof(int) ||
                    p.PropertyType == typeof(long))
                {
                    stats.Columns.Add(p.Name, new DataNumericColumn(p));
                }
                else
                {
                    stats.Columns.Add(p.Name, new DataColumn(p));
                }
            }
            foreach (var p in properties)
            {
                stats.Columns[p.Name].Items = list;
                stats.Columns[p.Name].Calculate();
                //Debug.WriteLine(stats.Columns[p.Name].ToString());
            }

            return stats;
        }
        public override string ToString()
        {
            var type = this.DataType.ToReadableString();

            var lines = new StringBuilder();
            lines.AppendLine("DataStats: " + this.DataCount + " items in " + type + " ");
            //type = this.ItemType.ToReadableString();
            lines.AppendLine(type + " Properties=" + Columns.Count + "");

            var columns = Columns.Values.OrderByDescending(c => c.PropertyType.FullName).ToList();
            var types = columns.Select(c => c.PropertyType.ToReadableString().Length).ToList();
            //var names = columns.Select(c => c.Name.Length).ToList();
            //var typesMax = types.Max();
            //var namesMax = names.Max();

            //for (var i = 0; i < columns.Count(); i++)
            //{
            //    lines.AppendLine((i + 1).ToString("D2") + " " + columns[i].ToString(typesMax, namesMax));
            //}

            return lines.ToString();
        }
    }

}
