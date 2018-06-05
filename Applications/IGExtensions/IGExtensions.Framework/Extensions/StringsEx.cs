using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
    public static class StringEx //IGExtensions.Framework.System
    {
        public static int ToInteger(this string value)
        {
            int result;
            if (!int.TryParse(value, out result)) 
                result = 0;
            return result;
        }

        ///<summary>
        /// Converts string to title case
        ///</summary>
        public static string ToTitle(this string current)
        {
            var result = current.ToLower();
            result = Regex.Replace(result, @"\b(\w)", m => m.Value.ToUpper());
            result = Regex.Replace(result, @"\s(of|in|by|and)\s", m => m.Value.ToLower(), RegexOptions.IgnoreCase);
            return result.Trim();
        }
        public static string ToSentence(this List<string> words)
        {
            var result = string.Empty;
            for (var i = 0; i < words.Count - 1; i++)
            {
                result += words[i] + " ";
            }
            result += words[words.Count - 1];
            return result;
        }
       
        ///<summary>
        /// Converts removes specified string from the current string
        ///</summary>
        public static string Remove(this string current, string item)
        {
            var result = current.Replace(item, "");
            return result.Trim();
        }
        public static string Remove(this string current, List<string> removeList)
        {
            var result = current;
            foreach (var item in removeList)
            {
                result = result.Replace(item, "");
            }
            return result.Trim();
        }
        #region Substring methods
        ///<summary>
        /// Returns a new string that ends at the first occurrence of specified string in the current string
        ///</summary>
        public static string SubstringTo(this string current, string stop)
        {
            var index = current.IndexOf(stop);
            if (index < 0)
            {
                Debug.WriteLine("Warning cannot retrieve substring [" + stop + "] from [" + current + "]");
                return string.Empty; //return current;
            }
            var discard = current.Substring(index);
            var result = current.Remove(discard);
            return result.Trim();
        }
        ///<summary>
        /// Returns a new string that starts from the first occurrence of specified string in the current string
        ///</summary>
        public static string SubstringFrom(this string current, string start)
        {
            var index = current.IndexOf(start);
            if (index < 0)
            {
                Debug.WriteLine("Warning cannot retrieve substring [" + start + "] from [" + current + "]");
                return string.Empty; // current;
            }
            var result = current.Substring(index);
            result = result.Remove(start);
            return result.Trim();
        }
        ///<summary>
        /// Returns a new string that enclosed between two specified strings in the current string
        ///</summary>
        public static string SubstringBetween(this string current, string start, string stop)
        {
            var result = current.SubstringFrom(start);
            result = result.SubstringTo(stop);
            return result.Trim();
        }
        #endregion
  
        public static byte[] GetBytes(this string str)
        {
            //byte[] bytes = new byte[str.Length * sizeof(char)];
            //System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            var bytes = Encoding.Unicode.GetBytes(str);
            return bytes;

        }

        public static string GetString(this byte[] bytes)
        {
            //char[] chars = new char[bytes.Length / sizeof(char)];
            //System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            //return new string(chars);

            return Encoding.Unicode.GetString(bytes, 0, bytes.Length);
        }
        /// <summary>
        /// Matches pattern in any string using Regex
        /// <para>Example: string.Match("[0-9]"</para>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool Match(this string value, string pattern)
        {
            var regex = new Regex(pattern);
            return regex.IsMatch(value);
        }

        public static bool IsValidUrl(this string text)
        {
            var rx = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
            return rx.IsMatch(text);
        }
#if WPF
        public static NameValueCollection ToNameValueCollection(this string[] value)
        {
            var parameters = new NameValueCollection();

            var regexSplitter = new Regex(@"^-{1,2}|^/|=|:", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            var regexRemover = new Regex(@"^['""]?(.*?)['""]?$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string parameter = null;
            string[] parts;

            // Valid parameters forms:
            // {-,/,--}param{ ,=,:}((",')value(",'))
            // Examples: 
            // -param1 value1 --param2 /param3:"Test-:-work" 
            //   /param4=happy -param5 '--=nice=--'
            foreach (var txt in value)
            {
                // Look for new parameters (-,/ or --) and a
                // possible enclosed value (=,:)
                parts = regexSplitter.Split(txt, 3);

                switch (parts.Length)
                {
                    // Found a value (for the last parameter 
                    // found (space separator))
                    case 1:
                        if (parameter != null)
                        {
                            parts[0] = regexRemover.Replace(parts[0], "$1");

                            parameters.Add(parameter, parts[0]);
                        }
                        // else Error: no parameter waiting for a value (skipped)
                        break;

                    // Found just a parameter
                    case 2:
                        // The last parameter is still waiting. 
                        // With no value, set it to true.
                        if (parameter != null)
                        {
                            parameters.Add(parameter, "true");
                        }
                        parameter = parts[1];
                        break;

                    // Parameter with enclosed value
                    case 3:
                        // The last parameter is still waiting. 
                        // With no value, set it to true.
                        if (parameter != null)
                        {
                            parameters.Add(parameter, "true");
                        }

                        parameter = parts[1];

                        // Remove possible enclosing characters (",')
                        parts[2] = regexRemover.Replace(parts[2], "$1");
                        parameters.Add(parameter, parts[2]);

                        parameter = null;
                        break;
                }
            }

            // In case a parameter is still waiting
            if (parameter != null)
            {
                parameters.Add(parameter, "true");
            }

            return parameters;
        }

#endif
    }
}
 