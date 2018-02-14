using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace IgExcel.Services
{
    public class FontService : IFontService
    {
        public List<int> GetFontSizes()
        {
            return new List<int> { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
        }

        public List<string> GetFontNames()
        {
            var fonts = Fonts.SystemFontFamilies.ToList().Select(x => x.Source).ToList();

            fonts.Sort();

            return fonts;
        }

        public List<Color> GetHighlightColors()
        {
            return new List<Color> { Colors.Red, Colors.Green, Colors.Yellow, Colors.Gray, Colors.Red, Colors.Red, Colors.Red, Colors.Red };
        }
    }
}
