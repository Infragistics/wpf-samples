using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace IGExtensions.Framework.Tools
{
    /// <summary>
    /// Represents a string formatting object used to create strings
    /// based upon an object and a formatting string which dereferences
    /// properties from the object.
    /// </summary>
    public class StringFormatter
    {
        /// <summary>
        /// Predicate indicating that the current StringFormatter object refers
        /// to the named property.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns>true if the current StringFormatter object refers
        /// to the named property.</returns>
        public bool References(string propertyName)
        {
            return PropertyNames.Contains(propertyName);
        }

        /// <summary>
        /// Create a formatted a string according to the compiled format
        /// string and the properties on the specified object
        /// </summary>
        /// <param name="obj">The object in context.</param>
        /// <param name="propertyValue">The Func<object, string, object> in context.</param>
        /// <returns>Formatted string or null on error.</returns>
        public string Format(object obj)
        {
            return Format(null, obj);
        }

        public string Format(IFormatProvider formatProvider, object obj)
        {
            if (CompiledFormatString == null || PropertyNames == null)
            {
                return null;
            }

            object[] propertyValues = new object[PropertyNames.Count];

            for (int i = 0; i < PropertyNames.Count; ++i)
            {
                propertyValues[i] = GetPropertyValue(obj, PropertyNames[i]);

                if (propertyValues[i] == null)
                {
                    return null;
                }
            }

            return string.Format(formatProvider, CompiledFormatString, propertyValues);
        }

        /// <summary>
        /// Gets or sets the raw formatting string for the current StringFormatter object. 
        /// </summary>
        /// <remarks>
        /// Setting the raw formatting string immediately compiles the format string and
        /// list of referenced properties.
        /// </remarks>
        public string FormatString
        {
            get { return formatString; }
            set
            {
                if (formatString != value)
                {
                    formatString = value;

                    int i = 0;
                    int cur = 0;

                    PropertyNames = new List<string>();
                    StringBuilder stringBuilder = new StringBuilder();

                    for (int bgn = formatString.IndexOf('{', cur); bgn >= cur; bgn = formatString.IndexOf('{', cur))
                    {
                        stringBuilder.Append(formatString.Substring(cur, bgn - cur));

                        int end = formatString.IndexOf('}', bgn);

                        if (end <= bgn)
                        {
                            return; // that's a problem
                        }

                        string cmd = formatString.Substring(bgn + 1, end - bgn - 1).Trim();
                        int separator = cmd.IndexOf(':');

                        if (separator == -1)
                        {
                            PropertyNames.Add(cmd);
                            stringBuilder.Append("{" + i + "}");
                        }
                        else
                        {
                            PropertyNames.Add(cmd.Substring(0, separator).Trim());
                            stringBuilder.Append("{" + i + ":" + cmd.Substring(separator + 1).Trim() + "}");
                        }

                        ++i;
                        cur = end + 1;
                    }

                    stringBuilder.Append(formatString.Substring(cur));  // append trailing stuff
                    CompiledFormatString = stringBuilder.ToString();
                }
            }
        }
        private string formatString;

        /// <summary>
        /// Returns a String that represents the current StringFormatter.
        /// </summary>
        /// <returns>A String that represents the current StringFormatter.</returns>
        public override string ToString()
        {
            return FormatString ?? "";
        }

        /// <summary>
        /// Gets the compiled format string for the current StringFormatter object.
        /// </summary>
        public string CompiledFormatString { get; private set; }

        /// <summary>
        /// Gets the list of the property names referred to by the current StringFormatter object. 
        /// </summary>
        public List<string> PropertyNames { get; private set; }

        private static Regex regex = new Regex(@"(.*)\[(\d+)\]$");

        /// <summary>
        /// The default Func<object, string, object>
        /// </summary>
        /// <param name="obj">Object with the named property </param>
        /// <param name="name">Property name. A property name of '*' uses the object as the property.</param>
        /// <returns></returns>
        internal static object GetPropertyValue(Object obj, string name)
        {
            string[] names = name.Split('.');

            for (int i = 0; i < names.Length && obj != null; ++i)
            {
                Match match = regex.Match(names[i]);
                string propertyName = match.Success ? match.Groups[1].ToString() : names[i];
 
                Type type = obj.GetType();

                if (propertyName == "0" && type.IsValueType)
                {
                    return obj;
                }

                if (!string.IsNullOrEmpty(propertyName))
                {
                    PropertyInfo propertyInfo = type.GetProperty(propertyName);

                    obj = propertyInfo != null && propertyInfo.CanRead ? propertyInfo.GetValue(obj, null) : null;
                }

                if (obj != null && match.Success)
                {
                    PropertyInfo propertyInfo = type.GetProperty("Item");
                    object[] indexArgs = { match.Groups[2].ToString() };

                    obj = propertyInfo != null && propertyInfo.CanRead ? propertyInfo.GetValue(obj, indexArgs) : null;
                }
            }

            return obj;
        }
    }

}
