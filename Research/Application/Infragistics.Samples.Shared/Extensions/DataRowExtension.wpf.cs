using System;
using System.Data;

namespace Infragistics.Samples.Shared.Extensions
{
    //NOTE: DataRowExtension is only for WPF Platform
    public static class DataRowExtension
    {
        /// <summary>
        // Get the value for an DataRow as a double
        /// </summary>
        /// <param name="row"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static double GetDouble(this DataRow row, string columnName)
        {
            double value = 0d;
            if (row != null)
            {
                value = row[columnName] is DBNull ? 0.0 : Convert.ToDouble(row[columnName]);
            }
            return value;
        }
        /// <summary>
        // Get the value for an DataRow as a string
        /// </summary>
        /// <param name="row"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static string GetString(this DataRow row, string columnName)
        {
            string value = string.Empty;
            if (row != null)
            {
                value = row[columnName] is DBNull ? string.Empty : row[columnName] as string;
            }
            return value;
        }
    }
}
