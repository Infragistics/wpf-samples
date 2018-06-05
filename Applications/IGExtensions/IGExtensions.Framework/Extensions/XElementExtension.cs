using System;
using System.Xml.Linq;

namespace System.Xml.Linq  
{
    public static class XElementExtension
    {
        /// <summary>
        /// Get the value for an element as a <see cref="decimal"/>
        /// </summary>
        /// <param name="element">Class being extended</param>
        /// <returns>The value from an element</returns>
        public static decimal GetDecimal(this XElement element)
        {
            decimal value = 0m;
            if (element != null) 
                decimal.TryParse(element.Value, out value);
            return value;
        }
        /// <summary>
        /// Get the value for an element as a <see cref="int"/>
        /// </summary>
        /// <param name="element">Class being extended</param>
        /// <returns>The value from an element</returns>
        public static int GetInt(this XElement element)
        {
            int value = 0;
            if (element != null)
                int.TryParse(element.Value, out value);
            return value;
        }
        /// <summary>
        /// Get the value for an element as a <see cref="double"/>
        /// </summary>
        /// <param name="element">Class being extended</param>
        /// <returns>The value from an element</returns>
        public static double GetDouble(this XElement element)
        {
            double value = 0d;
            if (element != null)
                double.TryParse(element.Value, out value);
            return value;
        }
        /// <summary>
        /// Get the value for an element as a <see cref="DateTime"/>
        /// </summary>
        /// <param name="element">Class being extended</param>
        /// <returns>The value from an element</returns>
        public static DateTime GetDateTime(this XElement element)
        {
            var value = default(DateTime);
            if (element != null)
            {
                DateTime.TryParse(element.Value, out value);
            }
            return value;
        }
        /// <summary>
        /// Check to see if this element is valid and return its value. When its missing return string.empty.
        /// </summary>
        /// <param name="element">Class being extended</param>
        /// <returns>The value from an element</returns>
        public static string GetString(this XElement element)
        {
            string value = string.Empty;
            if (element != null)
            {
                value = element.Value;
            }
            return value;
        }
        /// <summary>
        /// Check to see if this element is valid and return its value. When its missing return string.empty.
        /// </summary>
        /// <param name="element">Class being extended</param>
        /// <returns>The value from an element</returns>
        public static bool GetBool(this XElement element)
        {
            bool value = false;
            if (element != null)
            {
                if (element.Value == "1")
                {
                    value = true;
                }
            }
            return value;
        }
        ///// <summary>
        ///// Check to see if this element is valid and return its value. When its missing return string.empty.
        ///// </summary>
        ///// <param name="element">Class being extended</param>
        ///// <returns>The value from an element</returns>
        //public static string GetOptionalValue(this XElement element)
        //{
        //    return element.GetString();
        //}
    }

}