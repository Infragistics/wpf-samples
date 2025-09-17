using System.Windows.Media;

namespace Infragistics.Samples.Shared.Tools
{
    public class ColorMaker
    {
        public static Color GetColor(int alpha, int red, int green, int blue)
        {
            return Color.FromArgb((byte)alpha, (byte)red, (byte)green, (byte)blue);
            //return new Color() {A = alpha};
        }

        //public bool IsNamedColor { get; set; }
    }
}