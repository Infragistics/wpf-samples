using System.Collections.Generic;

namespace System
{
    public static class StringEx
    {

        public static readonly string LineBreak = "\r\n";
        public static readonly string[] LineSeperator = { LineBreak };

        /// <summary>
        /// Splits a string that contains line breaks into lines of strings
        /// </summary>
        public static string[] SplitToLines(this string current)
        {
            var lines = current.Split(LineSeperator, new StringSplitOptions());
            return lines;
        }
        /// <summary>
        /// Remove blank lines or lines with whitespaces from the string 
        /// </summary>
        public static string RemoveBlankLines(this string current)
        {
            var sourceLines = current.SplitToLines();
            current = string.Empty;
            foreach (var line in sourceLines)
            {
                if (line.Trim() == "")
                {
                    continue;
                }
                current += line + LineBreak;
            }
            return current;
        }

        /// <summary>
        /// Checks if the current string to all strings in specified list
        /// </summary>
        public static bool Contains(this string current, List<string> items, StringCompare comparison = StringCompare.Ordinal)
        {
            if (comparison == StringCompare.LowerCase)
                current = current.ToLower();
            else if (comparison == StringCompare.UpperCase)
                current = current.ToUpper();

            foreach (var item in items)
            {
                string target;
                if (comparison == StringCompare.LowerCase)
                    target = item.ToLower();
                else if (comparison == StringCompare.UpperCase)
                    target = item.ToUpper();
                else //if (comparison == StringCompare.Ordinal)
                    target = item;

                if (!current.Trim().Contains(target.Trim()))
                    return false;

            }
            return true;
        }

        /// <summary>
        /// Converts the current string to double or zero if cannot parse it
        /// </summary>
        public static double ToDouble(this string current, double defaultValue = 0.0)
        {
            double value;
            return double.TryParse(current, out value) ? value : defaultValue;
        }

        /// <summary>
        /// Converts the current string to integer or zero if cannot parse it
        /// </summary>
        public static int ToInteger(this string current, int defaultValue = 0)
        {
            int value;
            return int.TryParse(current, out value) ? value : defaultValue;
        }
        ///// <summary>
        ///// Converts the current string to integer or zero if cannot parse it
        ///// </summary>
        //public static int ToInteger(this string current, double defaultValue = 0.0)
        //{
        //    if (double.IsNaN(defaultValue)) defaultValue = 0;
        //    if (double.IsNegativeInfinity(defaultValue)) defaultValue = int.MinValue;
        //    if (double.IsPositiveInfinity(defaultValue)) defaultValue = int.MaxValue;
                
        //    int value;
        //    return int.TryParse(current, out value) ? value : (int)defaultValue;
        //}
        /// <summary>
        /// Converts the current string to capitalized string
        /// </summary>
        public static string ToUpperFirst(this string label)
        {
            if (string.IsNullOrEmpty(label))
            {
                return string.Empty;
            }
            var result = label.Substring(0, 1).ToUpper();
            if (label.Length > 1) result += label.Substring(1);
            return result;
        }

        /// <summary>
        /// Converts the current string to camel string
        /// </summary>
        public static string ToCamel(this string label)
        {
            if (string.IsNullOrEmpty(label))
            {
                return string.Empty;
            }
            var result = label.Substring(0, 1).ToLower();
            if (label.Length > 1) result += label.Substring(1);
            return result;
        }
        /// <summary>
        /// Converts the current List of strings to a string separated by specified symbol (default to ",")
        /// </summary>
        public static string ToListString(this List<string> strings, string seprator = ", ")
        {
            var result = String.Empty;
            for (var i = 0; i < strings.Count; i++)
            {
                result += strings[i];
                if (i < strings.Count - 1)
                {
                    result += seprator; // ", ";
                }
            }
            return result;
        }
        /// <summary>
        /// Add spaces to left and right of the string such that new string has has a length of specified value
        /// </summary>
        public static string PadCenter(this string label, int length)
        {
            //if (string.IsNullOrEmpty(label))
            //{
            //    return string.Empty;
            //}
            label = label.Trim();
            var left = (length / 2) - (label.Length / 2);
           // if (left < 0) left = 0;
           // var right = (length / 2) - (label.Length / 2);
            //var right = length - left - label.Length;

            var right = (length - label.Length) / 2;
            if (right < 0) right = 0;
            var result = "".PadLeft(right) + label + "".PadRight(right);
            if (result.Contains(".")) result += " ";
            return result;
        }

        /// <summary>
        /// Remove occurrences of a string from the current string
        /// </summary>
        public static string Remove(this string label, string removeStrings)
        {
            var result = label.Replace(removeStrings, "");
            return result;
        }
        /// <summary>
        /// Remove occurrences of list of strings from the current string
        /// </summary>
        public static string Remove(this string label, List<string> removeStrings)
        {
            var result = label.Remove(removeStrings.ToArray());
            return result;
        }
        /// <summary>
        /// Removes occurrences of strings from the current string
        /// </summary>
        public static string Remove(this string label, string[] removeStrings)
        {
            var result = label;
            foreach (var str in removeStrings)
            {
                result = result.Replace(str, "");
            }
            return result;
        }



    }

    public enum StringCompare
    {
        Ordinal,
        LowerCase,
        UpperCase,
    }
}