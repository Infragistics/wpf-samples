using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace System.Collections.Generic // IGExtensions.Framework.System
{
    /// <summary>
    /// Represents an extension for IDictionary
    /// </summary>
    public static class DictionaryEx
    {

        public static List<string> GetKeyNames(this ResourceDictionary dictionary)
        {
            PropertyInfo prop = typeof(ResourceDictionary).GetProperty("Keys");
            var keys = prop.GetValue(dictionary, null) as object[];
            var list = keys.Select(key => key.ToString()).ToList();
            return list;
        }
        /// <summary>
        /// Converts IDictionary &lt;string, string&gt; to a string
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        /// <remarks>This is an extension method provided by <seealso cref="DictionaryEx"/></remarks>
        public static string ToListString(this IDictionary<string, string> queryString)
        {
            string result = string.Empty;
            foreach (var kv in queryString)
            {
                result += "" + kv.Key + "=" + kv.Value + ", ";
            }
            return result;
        }
        //public static string ToListString(this ResourceDictionary dictionary)
        //{
        //    string result = string.Empty;
        //    var keys = dictionary.Keys;
        //    var values = dictionary.Values;


        //    var ind = 0;
        //    foreach (var key in dictionary.Values)
        //    {
        //        result += ind++ + " " + key + " = " + keys[] + "  ";
        //    }
        //    //for (int i = 0; i < dictionary.Count; i++)
        //    //{
        //    //    result += ind++ + " " + dictionary[i] + " = " + keys[] + "  ";
        //    //}

        //       var md = dictionary.MergedDictionaries;

             
        //    foreach (var resource in md)
        //    {
        //        foreach (var key in resource.Keys)
        //        {
        //            result += ind++ + " " + key + " = " + keys[] + "  ";
        //       System.Diagnostics.Debug.WriteLine(ind++ + " " + key);
        //            keys.Add(key.ToString());
        //        }
        //    }

        //        return result;
        //}
    }
}