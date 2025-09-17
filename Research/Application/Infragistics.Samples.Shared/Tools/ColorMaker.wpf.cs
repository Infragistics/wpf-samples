using System.Drawing;

namespace Infragistics.Samples.Shared.Tools
{
    public class ColorMaker
    {
        public static Color GetColor(int alpha, int red, int green, int blue)
        {
            return System.Drawing.Color.FromArgb(alpha, red, green, blue);
        }

        //public bool IsNamedColor { get; set; }
    }
}