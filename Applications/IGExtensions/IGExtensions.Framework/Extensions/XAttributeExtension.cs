using System.Xml.Linq;
using System;

namespace System.Xml.Linq  
{
    /// <summary>
    /// Extension Methods for the XAttribute class.
    /// </summary>
    public static class XAttributeExtension
    {
        /// <summary>
        /// Get the value for an attribute as a <see cref="bool"/>
        /// </summary>
        /// <param name="attribute">Class being extended</param>
        /// <returns>The value from an attribute</returns>
        public static bool GetBool(this XAttribute attribute)
        {
            bool value = false;
            bool.TryParse(attribute.Value, out value);
            return value;
        }

        /// <summary>
        /// Get the value for an attribute as a <see cref="decimal"/>
        /// </summary>
        /// <param name="attribute">Class being extended</param>
        /// <returns>The value from an attribute</returns>
        public static decimal GetDecimal(this XAttribute attribute)
        {
            decimal value = 0m;
            decimal.TryParse(attribute.Value, out value);
            return value;
        }

        /// <summary>
        /// Get the value for an attribute as a <see cref="int"/>
        /// </summary>
        /// <param name="attribute">Class being extended</param>
        /// <returns>The value from an attribute</returns>
        public static int GetInt(this XAttribute attribute)
        {
            int value = 0;
            int.TryParse(attribute.Value, out value);
            return value;
        }

        /// <summary>
        /// Get the value for an attribute as a <see cref="double"/>
        /// </summary>
        /// <param name="attribute">Class being extended</param>
        /// <returns>The value from an attribute</returns>
        public static double GetDouble(this XAttribute attribute)
        {
            double value = 0d;
            double.TryParse(attribute.Value, out value);
            return value;
        }

        /// <summary>
        /// Get the value for an attribute as a <see cref="DateTime"/>
        /// </summary>
        /// <param name="attribute">Class being extended</param>
        /// <returns>The value from an attribute</returns>
        public static DateTime GetDateTime(this XAttribute attribute)
        {
            DateTime value = default(DateTime);

            if (attribute != null)
            {
                DateTime.TryParse(attribute.Value, out value);
            }
            return value;
        }

        /// <summary>
        /// Check to see if this element is valid and return its value. When its missing return string.empty.
        /// </summary>
        /// <param name="attribute">Class being extended</param>
        /// <returns>The value from an element</returns>
        public static string GetString(this XAttribute attribute)
        {
            string value = string.Empty;

            if (attribute != null)
            {
                value = attribute.Value;
            }
            return value;
        }

    }

}