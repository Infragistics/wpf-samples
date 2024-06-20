using System.Collections.Generic;
using System.Linq;

namespace CascadingComboBox.Utilities
{
    public static class ZipCodeData
    {
        static List<string> _zipCodeData = new List<string>();

        private static void GenerateZipCodeData()
        {
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    _zipCodeData.Add($"ZipCode: {x}.{y}");
                }
            }
        }

        public static IEnumerable<string> GetZipCodes(string city)
        {
            if (_zipCodeData.Count == 0)
                GenerateZipCodeData();

            var num = city.Substring(city.Length - 1);
            return _zipCodeData.Where(x => x.Substring(x.Length - 3, 1) == num);
        }
    }
}
