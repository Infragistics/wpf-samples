using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace IGExtensions.Framework.Tools
{
    public partial class StringTool
    {
        public static string Suffix(string key, int count, Func<int, string> dictionary)
        {
            int i0 = ArrayTool.BinarySearch(key, count, (a, b) => dictionary(a).CompareTo(b));
            int s0 = key.Length;        // index of first character in suffix  
            int s1 = s0 - 1;            // index of last character in suffix

            i0 = i0 < 0 ? ~i0 : i0;

            if (i0 < count && dictionary(i0).StartsWith(key))
            {
                s1 = dictionary(i0).Length - 1;          

                for (int i = i0+1; i < count && s1 >= s0 && dictionary(i).StartsWith(key); ++i)
                {
                    #region shorten the suffix according to dictionary(i)
                    s1 = Math.Min(s1, dictionary(i).Length);

                    int j = s0;

                    while (j <= s1 && dictionary(i0)[j] == dictionary(i)[j])
                    {
                        ++j;
                    }

                    s1 = j - 1;
                    #endregion
                }
            }

            return s1 >= s0 ? dictionary(i0).Substring(s0, s1 - s0 + 1) : null;
        }

        public static Pair<int, int> Suffixes(string key, int count, Func<int, string> dictionary)
        {
            int i0 = ArrayTool.BinarySearch(key, count, (a, b) => dictionary(a).CompareTo(b));

            if (i0 < 0)
            {
                i0 = ~i0;
            }

            int i1 = i0;

            while(i1<count-1 && dictionary(i1+1).StartsWith(key))
            {
                ++i1;
            }

            return new Pair<int, int>(i0, i1);
        }


        private static Random random=new Random();
        private static readonly string[] loremIpsum = "Lorem ipsum dolor sit amet consectetur adipisicing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua Ut enim ad minim veniam quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur Excepteur sint occaecat cupidatat non proident sunt in culpa qui officia deserunt mollit anim id est laborum".Split(' ');

        public static string LoremIpsum()
        {
            return loremIpsum[random.Next(loremIpsum.Length)];
        }
    }
}
