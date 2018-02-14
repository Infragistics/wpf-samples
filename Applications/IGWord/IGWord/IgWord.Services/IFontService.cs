using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace IgWord.Services
{
    public interface IFontService
    {
        List<double> GetFontSizes();
        List<string> GetFontNames();
        List<Color> GetHighlightColors();
    }
}
