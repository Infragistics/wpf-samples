using System.Collections.Generic;
using System.Windows.Media;

namespace IgExcel.Services
{
    public interface IFontService
    {
        List<int> GetFontSizes();
        List<string> GetFontNames();
        List<Color> GetHighlightColors();
    }
}
