using System.Collections.Generic;
using System.Linq;

namespace System.Windows.Media
{

    //TODO: research/add http://www.codeproject.com/Articles/19045/Manipulating-colors-in-NET-Part-1
    
    public static class ColorEx
    {
        /// <summary>Blends the specified colors together</summary>
        /// <param name="thisColor">Color to blend onto the background color</param>
        /// <param name="backColor">Color to blend the other color onto</param>
        /// <param name="amount">How much of <paramref name="thisColor"/> to keep on top of <paramref name="backColor"/></param>
        public static Color Blend(this Color thisColor, Color backColor, double amount = 0.5)
        {
            var r = (byte)((thisColor.R * amount) + backColor.R * (1 - amount));
            var g = (byte)((thisColor.G * amount) + backColor.G * (1 - amount));
            var b = (byte)((thisColor.B * amount) + backColor.B * (1 - amount));
            return Color.FromArgb(255, r, g, b);
        }

        /// <summary> Converts colors to a single color </summary>
        public static Color Blend(this List<Color> colors)
        {
            byte r = 0, g = 0, b = 0;
            double amount = 1.0 / (colors.Count * 1.0);
            foreach (var color in colors)
            {
                r += (byte) (color.R * amount);
                g += (byte) (color.G * amount);
                b += (byte) (color.B * amount);
            }
            return Color.FromArgb(255, r, g, b);
        }
        #region Conversion

        /// <summary> Converts colors to a list of brushes </summary>
        public static List<SolidColorBrush> ToBrushes(this List<Color> colors)
        {
            return colors.Select(item => item.ToBrush()).ToList();
        }
        /// <summary> Converts brushes to a list of colors </summary>
        public static List<Color> ToColors(this List<SolidColorBrush> brushes)
        {
            return brushes.Select(item => item.ToColor()).ToList();
        }
        /// <summary> Converts color HEX to <see cref="Color"/>, e.g. "#FF000000" -> Colors.Black </summary>
        public static Color FromHex(this Color color, string colorHexString)
        {
            return colorHexString.ToColor(); 
        }
        /// <summary> Converts color ARBG to <see cref="Color"/>, e.g. 0xFF000000 -> Colors.Black </summary>
        public static Color FromArgb(this Color color, uint argb)
        {
            return argb.ToColor();
        }
        /// <summary> Converts color ARBG to <see cref="Color"/>, e.g. 0xFF000000 -> Colors.Black </summary>
        public static Color ToColor(this uint argb)
        {
            return Color.FromArgb((byte)((argb & -16777216) >> 0x18),
                                  (byte)((argb & 0xff0000) >> 0x10),
                                  (byte)((argb & 0xff00) >> 8),
                                  (byte)(argb & 0xff));
        }
        /// <summary> Converts color HEX to <see cref="Color"/>, e.g. "#FF000000"  -> Colors.Black </summary>
        public static Color ToColor(this string colorHexString)
        {
            return Color.FromArgb(
                   Convert.ToByte(colorHexString.Substring(1, 2), 16),
                   Convert.ToByte(colorHexString.Substring(3, 2), 16),
                   Convert.ToByte(colorHexString.Substring(5, 2), 16),
                   Convert.ToByte(colorHexString.Substring(7, 2), 16));
        }
        /// <summary> Converts <see cref="Color"/> to <see cref="SolidColorBrush"/>  </summary>
        public static SolidColorBrush ToBrush(this Color color)
        {
            return new SolidColorBrush(color);
        }
        /// <summary> Converts <see cref="Color"/> to color ARBG, e.g. Colors.Black -> 0xFF000000  </summary>
        public static uint ToArgb(this Color color)
        {
            return (uint)((color.A << 24) | (color.R << 16) |
                          (color.G << 8) | (color.B << 0));
        }
        /// <summary> Converts <see cref="Color"/> to color HEX string, e.g. Colors.Black -> "#FF000000"  </summary>
        public static string ToHex(this Color color)
        {
            return color.ToArgb().ToHex();
        }
        /// <summary> Converts color ARBG to color HEX string, e.g. 0xFF000000 -> "#FF000000"   </summary>
        public static string ToHex(this uint argb)
        {
            var hex = String.Format("{0:X}", argb);
            return "#" + hex; // argb; 
        }
        public static string ToText(this Color color)
        {
            return color.ToHex();
        }
	    #endregion
    }
    public static class BrushEx
    {
        /// <summary>Blends the specified brushes together</summary>
        public static SolidColorBrush Blend(this SolidColorBrush thisBrush, SolidColorBrush otherBrush, double amount = 0.5)
        {
            var color = thisBrush.Color.Blend(otherBrush.Color, amount);
            return new SolidColorBrush(color);
        }
        /// <summary> Converts a list of <see cref="SolidColorBrush"/> to a single blended <see cref="SolidColorBrush"/></summary>
        public static SolidColorBrush Blend(this List<SolidColorBrush> brushes)
        {
            var color = brushes.ToColors().Blend();
            return new SolidColorBrush(color);
        }
        /// <summary> Converts a list of <see cref="Brush"/> to a single blended color </summary>
        public static SolidColorBrush Blend(this List<Brush> brushes)
        {
            var color = brushes.ToColors().Blend();
            return new SolidColorBrush(color);
        }
        #region Convertions
        /// <summary> Converts color HEX to <see cref="SolidColorBrush"/>, e.g. "#FF000000" -> Brush.Black </summary>
        public static SolidColorBrush FromHex(this SolidColorBrush brush, string hexColor)
        {
            return new Color().FromHex(hexColor).ToBrush();
        }
        /// <summary> Converts color ARBG to <see cref="SolidColorBrush"/>, e.g. 0xFF000000 -> Brush.Black </summary>
        public static SolidColorBrush FromArgb(this SolidColorBrush brush, uint argb)
        {
            return argb.ToColor().ToBrush();
        }
        /// <summary> Converts <see cref="Color"/> to <see cref="SolidColorBrush"/>, e.g. Color.Black -> Brush.Black </summary>
        public static SolidColorBrush FromColor(this SolidColorBrush brush, Color color)
        {
            return color.ToBrush();
        }
        /// <summary> Converts color ARBG to <see cref="SolidColorBrush"/>, e.g. 0xFF000000 -> Brush.Black </summary>
        public static SolidColorBrush ToBrush(this uint argb)
        {
            return argb.ToColor().ToBrush();
        }
        /// <summary> Converts <see cref="SolidColorBrush"/> to color ARBG, e.g. Brush.Black -> 0xFF000000 </summary>
        public static uint ToArgb(this SolidColorBrush brush)
        {
            return brush.ToColor().ToArgb();
        }
        /// <summary> Converts <see cref="SolidColorBrush"/> to color HEX string, e.g. Brush.Black -> "#FF000000"   </summary>
        public static string ToHex(this SolidColorBrush brush)
        {
            return "#" + brush.ToArgb();
        } 
        /// <summary> Converts <see cref="SolidColorBrush"/> to <see cref="Color"/>, e.g. Brush.Black -> Color.Black </summary>
        public static Color ToColor(this SolidColorBrush brush)
        {
            return brush.Color;
        }
        public static string ToText(this SolidColorBrush brush)
        {
            return brush.ToHex();
        }
        public static LinearGradientBrush Reverse(this LinearGradientBrush brush)
        {
            //var gradient = new LinearGradientBrush();
            //gradient.EndPoint = brush.EndPoint;
            //gradient.ColorInterpolationMode = brush.ColorInterpolationMode;
            //gradient.StartPoint = brush.StartPoint;

            //var gradients = new GradientStopCollection();
            foreach (var gradient in brush.GradientStops)
            {
                gradient.Offset = 1 - gradient.Offset;
            }
            
            return brush;
        }
        public static RadialGradientBrush Reverse(this RadialGradientBrush brush)
        {
            var gradients = new GradientStopCollection();
            foreach (var gradient in brush.GradientStops)
            {
                gradients.Add(new GradientStop { Color = gradient.Color, Offset = 1 - gradient.Offset });
                //gradient.Offset = 1 - gradient.Offset;
            }
            brush.GradientStops = gradients;
            return brush;
        }
        /// <summary> Converts <see cref="Brush"/> to <see cref="Color"/></summary>
        public static Color ToColor(this Brush brush)
        {
            var color = NamedColors.Red.Color;
            if (brush is SolidColorBrush)
                color = (brush as SolidColorBrush).Color;
            else if (brush is LinearGradientBrush)
            {
                var colors = (brush as GradientBrush).GradientStops.Select(gradient => gradient.Color).ToList();
                color = colors.Blend();
            }
            else if (brush is RadialGradientBrush)
            {
                var colors = (brush as RadialGradientBrush).GradientStops.Select(gradient => gradient.Color).ToList();
                color = colors.Blend();
            }
            return color;
        }
        /// <summary> Converts a list of <see cref="Brush"/> to a list of <see cref="Color"/></summary>
        public static List<Color> ToColors(this List<Brush> brushes)
        {
            var colors = new List<Color>();
            foreach (var brush in brushes)
            {
                if (brush is SolidColorBrush)
                    colors.Add((brush as SolidColorBrush).Color);
                else if (brush is LinearGradientBrush)
                {
                    var gradients = (brush as GradientBrush).GradientStops.Select(gradient => gradient.Color).ToList();
                    colors.AddRange(gradients);
                }
                else if (brush is RadialGradientBrush)
                {
                    var gradients = (brush as GradientBrush).GradientStops.Select(gradient => gradient.Color).ToList();
                    colors.AddRange(gradients);
                }
            }
            return colors;
        }
        /// <summary> Converts a list of <see cref="Color"/> to <see cref="LinearGradientBrush"/></summary>
        public static LinearGradientBrush ToGradientBrush(this IEnumerable<Color> colors)
        {
            var list = colors.ToList();
            return list.ToGradientBrush();
        }

        /// <summary> Converts a list of <see cref="Color"/> to <see cref="LinearGradientBrush"/></summary>
        public static LinearGradientBrush ToGradientBrush(this List<Color> colors)
        {
            var brush = new LinearGradientBrush();
            if (colors == null || colors.Count == 0)
            {
                brush.GradientStops.Add(new GradientStop { Color = NamedColors.White.Color, Offset = 0 });
                brush.GradientStops.Add(new GradientStop { Color = NamedColors.Red.Color, Offset = 1 });
            }
            else if (colors.Count == 1)
            {
                brush.GradientStops.Add(new GradientStop { Color = colors[0], Offset = 0 });
                brush.GradientStops.Add(new GradientStop { Color = colors[0], Offset = 1 });
            }
            else //if (brushes.Count > 1)
            {
                var offset = 0.0;
                var offsetChange = 1.0 / (colors.Count - 1);
                foreach (var color in colors)
                {
                    brush.GradientStops.Add(new GradientStop { Color = color, Offset = offset });
                    offset += offsetChange;
                    offset = System.Math.Min(offset, 1.0);
                }
            }
            return brush;
        }
        /// <summary> Converts a list of <see cref="Brush"/> to <see cref="LinearGradientBrush"/></summary>
        public static LinearGradientBrush ToGradientBrush(this List<Brush> brushes)
        {
            var colors = brushes.ToColors();
            return colors.ToGradientBrush();
        }
        public static BrushType GetBrushType(this Brush brush)
        {
            var brushType = BrushType.SolidColorBrush;
            if (brush is SolidColorBrush)
                brushType = BrushType.SolidColorBrush;
            else if (brush is LinearGradientBrush)
                brushType = BrushType.LinearGradientBrush;
            else if (brush is RadialGradientBrush)
                brushType = BrushType.RadialGradientBrush;
            return brushType;
        }

        #endregion
        public static void AddGradientStops(this GradientBrush brush, List<GradientStop> gradientStop)
        {
            brush.GradientStops = new GradientStopCollection();
            foreach (var gs in gradientStop)
            {
                brush.GradientStops.Add(gs);
            }
        }
        public static void AddRange(this GradientStopCollection gradientStopCollection, List<GradientStop> gradientStop)
        {
            //gradientStopCollection = new GradientStopCollection();
            foreach (var gs in gradientStop)
            {
                gradientStopCollection.Add(gs);
            }
        }
    }
    public enum BrushType
    {
        SolidColorBrush,
        LinearGradientBrush,
        RadialGradientBrush
    }
   
    /// <summary>
    /// Represents a named color and provides conversion between different representations of colors:
    /// <para><see cref="Color"/>, Color Hex string, Color ARGB integer</para>
    /// </summary>
    public class NamedColor
    {
        #region Constructors
        public NamedColor()
        {
            ColorCode = 0xFF000000;
            ColorName = this.ColorCode.ToHex();
        }
        public NamedColor(uint colorCode)
        {
            ColorCode = colorCode;
            ColorName = ColorCode.ToHex();
        }
        public NamedColor(Color color)
        {
            ColorCode = color.ToArgb();
            ColorName = ColorCode.ToHex();
        }
        public NamedColor(SolidColorBrush brush)
        {
            ColorCode = brush.Color.ToArgb();
            ColorName = ColorCode.ToHex();
        }
        #endregion
        #region Properties

        public SolidColorBrush BrushOpacity(double opacity)
        {
            return new SolidColorBrush(this.ColorOpacity(opacity));
        }
        public Color ColorOpacity(double opacity)
        {
            var alpha = (byte)(opacity * 255);
            var color = this.Color;
            color.A = alpha;
            return color;
        }

        public SolidColorBrush Brush { get { return this.ToBrush(); } }
        private Color _color;
        /// <summary>
        /// Gets or sets <see cref="Color"/> property
        /// </summary>
        public Color Color
        {
            get  { return ColorCode.ToColor(); }
            set
            {
                if (_color == value) return;
                _color = value;
                this.ColorCode = _color.ToArgb();
                this.ColorName = _color.ToHex();
            }
        }
        /// <summary>
        /// Gets or sets Color name property
        /// </summary>
        public string ColorName { get; set; }
        /// <summary>
        /// Gets or sets Color ARGB integer property
        /// </summary>
        public uint ColorCode { get; set; } 
        #endregion
        #region Methods
        /// <summary>
        /// Converts this object to <see cref="Color"/> 
        /// </summary>
        public Color ToColor()
        {
            return this.Color;
        }
        /// <summary>
        /// Converts this object to <see cref="SolidColorBrush"/> 
        /// </summary>
        public SolidColorBrush ToBrush()
        {
            return new SolidColorBrush(this.Color);
        }
        /// <summary>Blends the this color with specified color together</summary>
        /// <param name="withColor">Color to blend the this color</param>
        /// <param name="amount">How much of specified color to blend with this color</param>
        /// <returns>The blended color</returns>
        public void Blend(Color withColor, double amount = 0.5)
        {
            this.Color = this.Color.Blend(withColor, amount);
        }
        public void Blend(NamedColor withColor, double amount = 0.5)
        {
            this.Color = this.Color.Blend(withColor.Color, amount);
        }
        
        public static NamedColor FromBrush(Brush brush)
        {
            var color = brush.ToColor();
            return NamedColor.FromColor(color);
        }
        /// <summary>
        /// Converts color HEX string to NamedColor, e.g. "0xFF000000" -> Black
        /// </summary>
        public static NamedColor FromHex(string colorHex)
        {
            var color = new Color().FromHex(colorHex);
            return NamedColor.FromColor(color);
        }
        /// <summary>
        /// Converts color ARBG to NamedColor, e.g. 0xFF000000 -> Black
        /// </summary>
        public static NamedColor FromHex(uint colorArgb)
        {
            return new NamedColor { ColorName = "#" + colorArgb, ColorCode = colorArgb };
        }
        /// <summary>
        /// Converts <see cref="Color"/> to NamedColor, e.g. Colors.Black -> Black
        /// </summary>
        public static NamedColor FromColor(Color color)
        {
            var colorArgb = color.ToArgb();
            return NamedColor.FromHex(colorArgb);
        } 
        #endregion
    }
    /// <summary>
    /// Represents a list of <see cref="NamedColor"/> and provides additional list of color names, color codes, and <see cref="Color"/> objects
    /// </summary>
    public class NameColorList : List<NamedColor>
    {
        public List<string> Names
        {
            get
            {
                return this.Select(color => color.ColorName).ToList();
            }
        }
        public List<uint> Codes
        {
            get
            {
                return this.Select(color => color.ColorCode).ToList();
            }
        }
        public List<Color> Colors
        {
            get
            {
                return this.Select(color => color.Color).ToList();
            }
        }
        public List<SolidColorBrush> Brushes
        {
            get
            {
                return this.Select(color => color.Color.ToBrush()).ToList();
            }
        }
        public LinearGradientBrush GradientBrush
        {
            get
            {
                return this.Colors.ToGradientBrush();
            }
        }
        public List<GradientStop> GradientStops
        {
            get
            {
                return this.GradientBrush.GradientStops.Select(gs => new GradientStop {Color = gs.Color, Offset = gs.Offset}).ToList();
            }
        }
        
        public new NameColorList Reverse()
        {
            
            var list = new NameColorList();
            //var list = EnumerableEx.Reverse(this); //.ToList(); //Enumerable.Reverse(this);
            for (int i = this.Count - 1; i >= 0; i--)
            {
                list.Add(this[i]);
                //yield return this[i];
            }
            return list;
            
        }
    }
    /// <summary>
    /// Represents a static class that provides all named colors as individual <see cref="NamedColor"/> and as <see cref="NameColorList"/>
    /// </summary>
    public static class NamedColors
    {
        #region Lists
        private static NameColorList _list;
        /// <summary>
        /// Gets a <see cref="NameColorList"/> with all named colors
        /// </summary>
        public static NameColorList List
        {
            get
            {
                if (_list != null) return _list;
                _list = new NameColorList
                {
                    new NamedColor {ColorName = "AliceBlue", ColorCode = 0xFFF0F8FF},
                    new NamedColor {ColorName = "AntiqueWhite", ColorCode = 0xFFFAEBD7},
                    new NamedColor {ColorName = "Aqua", ColorCode = 0xFF00FFFF},
                    new NamedColor {ColorName = "Aquamarine", ColorCode = 0xFF7FFFD4},
                    new NamedColor {ColorName = "Azure", ColorCode = 0xFFF0FFFF},
                    new NamedColor {ColorName = "Beige", ColorCode = 0xFFF5F5DC},
                    new NamedColor {ColorName = "Bisque", ColorCode = 0xFFFFE4C4},
                    new NamedColor {ColorName = "Black", ColorCode = 0xFF000000},
                    new NamedColor {ColorName = "BlanchedAlmond", ColorCode = 0xFFFFEBCD},
                    new NamedColor {ColorName = "Blue", ColorCode = 0xFF0000FF},
                    new NamedColor {ColorName = "BlueViolet", ColorCode = 0xFF8A2BE2},
                    new NamedColor {ColorName = "Brown", ColorCode = 0xFFA52A2A},
                    new NamedColor {ColorName = "BurlyWood", ColorCode = 0xFFDEB887},
                    new NamedColor {ColorName = "CadetBlue", ColorCode = 0xFF5F9EA0},
                    new NamedColor {ColorName = "Chartreuse", ColorCode = 0xFF7FFF00},
                    new NamedColor {ColorName = "Chocolate", ColorCode = 0xFFD2691E},
                    new NamedColor {ColorName = "Coral", ColorCode = 0xFFFF7F50},
                    new NamedColor {ColorName = "CornflowerBlue", ColorCode = 0xFF6495ED},
                    new NamedColor {ColorName = "CornSilk", ColorCode = 0xFFFFF8DC},
                    new NamedColor {ColorName = "Crimson", ColorCode = 0xFFDC143C},
                    new NamedColor {ColorName = "Cyan", ColorCode = 0xFF00FFFF},
                    new NamedColor {ColorName = "DarkBlue", ColorCode = 0xFF00008B},
                    new NamedColor {ColorName = "DarkCyan", ColorCode = 0xFF008B8B},
                    new NamedColor {ColorName = "DarkGoldenrod", ColorCode = 0xFFB8860B},
                    new NamedColor {ColorName = "DarkGray", ColorCode = 0xFFA9A9A9},
                    new NamedColor {ColorName = "DarkGreen", ColorCode = 0xFF006400},
                    new NamedColor {ColorName = "DarkKhaki", ColorCode = 0xFFBDB76B},
                    new NamedColor {ColorName = "DarkMagenta", ColorCode = 0xFF8B008B},
                    new NamedColor {ColorName = "DarkOliveGreen", ColorCode = 0xFF556B2F},
                    new NamedColor {ColorName = "DarkOrange", ColorCode = 0xFFFF8C00},
                    new NamedColor {ColorName = "DarkOrchid", ColorCode = 0xFF9932CC},
                    new NamedColor {ColorName = "DarkRed", ColorCode = 0xFF8B0000},
                    new NamedColor {ColorName = "DarkSalmon", ColorCode = 0xFFE9967A},
                    new NamedColor {ColorName = "DarkSeaGreen", ColorCode = 0xFF8FBC8F},
                    new NamedColor {ColorName = "DarkSlateBlue", ColorCode = 0xFF483D8B},
                    new NamedColor {ColorName = "DarkSlateGray", ColorCode = 0xFF2F4F4F},
                    new NamedColor {ColorName = "DarkTurquoise", ColorCode = 0xFF00CED1},
                    new NamedColor {ColorName = "DarkViolet", ColorCode = 0xFF9400D3},
                    new NamedColor {ColorName = "DeepPink", ColorCode = 0xFFFF1493},
                    new NamedColor {ColorName = "DeepSkyBlue", ColorCode = 0xFF00BFFF},
                    new NamedColor {ColorName = "DimGray", ColorCode = 0xFF696969},
                    new NamedColor {ColorName = "DodgerBlue", ColorCode = 0xFF1E90FF},
                    new NamedColor {ColorName = "Firebrick", ColorCode = 0xFFB22222},
                    new NamedColor {ColorName = "FloralWhite", ColorCode = 0xFFFFFAF0},
                    new NamedColor {ColorName = "ForestGreen", ColorCode = 0xFF228B22},
                    new NamedColor {ColorName = "Fuchsia", ColorCode = 0xFFFF00FF},
                    new NamedColor {ColorName = "Gainesboro", ColorCode = 0xFFDCDCDC},
                    new NamedColor {ColorName = "GhostWhite", ColorCode = 0xFFF8F8FF},
                    new NamedColor {ColorName = "Gold", ColorCode = 0xFFFFD700},
                    new NamedColor {ColorName = "Goldenrod", ColorCode = 0xFFDAA520},
                    new NamedColor {ColorName = "Gray", ColorCode = 0xFF808080},
                    new NamedColor {ColorName = "Green", ColorCode = 0xFF008000},
                    new NamedColor {ColorName = "GreenYellow", ColorCode = 0xFFADFF2F},
                    new NamedColor {ColorName = "Honeydew", ColorCode = 0xFFF0FFF0},
                    new NamedColor {ColorName = "HotPink", ColorCode = 0xFFFF69B4},
                    new NamedColor {ColorName = "IndianRed", ColorCode = 0xFFCD5C5C},
                    new NamedColor {ColorName = "Indigo", ColorCode = 0xFF4B0082},
                    new NamedColor {ColorName = "Ivory", ColorCode = 0xFFFFFFF0},
                    new NamedColor {ColorName = "Khaki", ColorCode = 0xFFF0E68C},
                    new NamedColor {ColorName = "Lavender", ColorCode = 0xFFE6E6FA},
                    new NamedColor {ColorName = "LavenderBlush", ColorCode = 0xFFFFF0F5},
                    new NamedColor {ColorName = "LawnGreen", ColorCode = 0xFF7CFC00},
                    new NamedColor {ColorName = "LemonChiffon", ColorCode = 0xFFFFFACD},
                    new NamedColor {ColorName = "LightBlue", ColorCode = 0xFFADD8E6},
                    new NamedColor {ColorName = "LightCoral", ColorCode = 0xFFF08080},
                    new NamedColor {ColorName = "LightCyan", ColorCode = 0xFFE0FFFF},
                    new NamedColor {ColorName = "LightGoldenrodYellow", ColorCode = 0xFFFAFAD2},
                    new NamedColor {ColorName = "LightGray", ColorCode = 0xFFD3D3D3},
                    new NamedColor {ColorName = "LightGreen", ColorCode = 0xFF90EE90},
                    new NamedColor {ColorName = "LightPink", ColorCode = 0xFFFFB6C1},
                    new NamedColor {ColorName = "LightSalmon", ColorCode = 0xFFFFA07A},
                    new NamedColor {ColorName = "LightSeaGreen", ColorCode = 0xFF20B2AA},
                    new NamedColor {ColorName = "LightSkyBlue", ColorCode = 0xFF87CEFA},
                    new NamedColor {ColorName = "LightSlateGray", ColorCode = 0xFF778899},
                    new NamedColor {ColorName = "LightSteelBlue", ColorCode = 0xFFB0C4DE},
                    new NamedColor {ColorName = "LightYellow", ColorCode = 0xFFFFFFE0},
                    new NamedColor {ColorName = "Lime", ColorCode = 0xFF00FF00},
                    new NamedColor {ColorName = "LimeGreen", ColorCode = 0xFF32CD32},
                    new NamedColor {ColorName = "Linen", ColorCode = 0xFFFAF0E6},
                    new NamedColor {ColorName = "Magenta", ColorCode = 0xFFFF00FF},
                    new NamedColor {ColorName = "Maroon", ColorCode = 0xFF800000},
                    new NamedColor {ColorName = "MediumAquamarine", ColorCode = 0xFF66CDAA},
                    new NamedColor {ColorName = "MediumBlue", ColorCode = 0xFF0000CD},
                    new NamedColor {ColorName = "MediumOrchid", ColorCode = 0xFFBA55D3},
                    new NamedColor {ColorName = "MediumPurple", ColorCode = 0xFF9370DB},
                    new NamedColor {ColorName = "MediumSeaGreen", ColorCode = 0xFF3CB371},
                    new NamedColor {ColorName = "MediumSlateBlue", ColorCode = 0xFF7B68EE},
                    new NamedColor {ColorName = "MediumSpringGreen", ColorCode = 0xFF00FA9A},
                    new NamedColor {ColorName = "MediumTurquoise", ColorCode = 0xFF48D1CC},
                    new NamedColor {ColorName = "MediumVioletRed", ColorCode = 0xFFC71585},
                    new NamedColor {ColorName = "MidnightBlue", ColorCode = 0xFF191970},
                    new NamedColor {ColorName = "MintCream", ColorCode = 0xFFF5FFFA},
                    new NamedColor {ColorName = "MistyRose", ColorCode = 0xFFFFE4E1},
                    new NamedColor {ColorName = "Moccasin", ColorCode = 0xFFFFE4B5},
                    new NamedColor {ColorName = "NavajoWhite", ColorCode = 0xFFFFDEAD},
                    new NamedColor {ColorName = "Navy", ColorCode = 0xFF000080},
                    new NamedColor {ColorName = "OldLace", ColorCode = 0xFFFDF5E6},
                    new NamedColor {ColorName = "Olive", ColorCode = 0xFF808000},
                    new NamedColor {ColorName = "OliveDrab", ColorCode = 0xFF6B8E23},
                    new NamedColor {ColorName = "Orange", ColorCode = 0xFFFFA500},
                    new NamedColor {ColorName = "OrangeRed", ColorCode = 0xFFFF4500},
                    new NamedColor {ColorName = "Orchid", ColorCode = 0xFFDA70D6},
                    new NamedColor {ColorName = "PaleGoldenrod", ColorCode = 0xFFEEE8AA},
                    new NamedColor {ColorName = "PaleGreen", ColorCode = 0xFF98FB98},
                    new NamedColor {ColorName = "PaleTurquoise", ColorCode = 0xFFAFEEEE},
                    new NamedColor {ColorName = "PaleVioletRed", ColorCode = 0xFFDB7093},
                    new NamedColor {ColorName = "PapayaWhip", ColorCode = 0xFFFFEFD5},
                    new NamedColor {ColorName = "PeachPuff", ColorCode = 0xFFFFDAB9},
                    new NamedColor {ColorName = "Peru", ColorCode = 0xFFCD853F},
                    new NamedColor {ColorName = "Pink", ColorCode = 0xFFFFC0CB},
                    new NamedColor {ColorName = "Plum", ColorCode = 0xFFDDA0DD},
                    new NamedColor {ColorName = "PowderBlue", ColorCode = 0xFFB0E0E6},
                    new NamedColor {ColorName = "Purple", ColorCode = 0xFF800080},
                    new NamedColor {ColorName = "Red", ColorCode = 0xFFFF0000},
                    new NamedColor {ColorName = "RosyBrown", ColorCode = 0xFFBC8F8F},
                    new NamedColor {ColorName = "RoyalBlue", ColorCode = 0xFF4169E1},
                    new NamedColor {ColorName = "SaddleBrown", ColorCode = 0xFF8B4513},
                    new NamedColor {ColorName = "Salmon", ColorCode = 0xFFFA8072},
                    new NamedColor {ColorName = "SandyBrown", ColorCode = 0xFFF4A460},
                    new NamedColor {ColorName = "SeaGreen", ColorCode = 0xFF2E8B57},
                    new NamedColor {ColorName = "SeaShell", ColorCode = 0xFFFFF5EE},
                    new NamedColor {ColorName = "Sienna", ColorCode = 0xFFA0522D},
                    new NamedColor {ColorName = "Silver", ColorCode = 0xFFC0C0C0},
                    new NamedColor {ColorName = "SkyBlue", ColorCode = 0xFF87CEEB},
                    new NamedColor {ColorName = "SlateBlue", ColorCode = 0xFF6A5ACD},
                    new NamedColor {ColorName = "SlateGray", ColorCode = 0xFF708090},
                    new NamedColor {ColorName = "Snow", ColorCode = 0xFFFFFAFA},
                    new NamedColor {ColorName = "SpringGreen", ColorCode = 0xFF00FF7F},
                    new NamedColor {ColorName = "SteelBlue", ColorCode = 0xFF4682B4},
                    new NamedColor {ColorName = "Tan", ColorCode = 0xFFD2B48C},
                    new NamedColor {ColorName = "Teal", ColorCode = 0xFF008080},
                    new NamedColor {ColorName = "Thistle", ColorCode = 0xFFD8BFD8},
                    new NamedColor {ColorName = "Tomato", ColorCode = 0xFFFF6347},
                    new NamedColor {ColorName = "Transparent", ColorCode = 0x00FFFFFF},
                    new NamedColor {ColorName = "Turquoise", ColorCode = 0xFF40E0D0},
                    new NamedColor {ColorName = "Violet", ColorCode = 0xFFEE82EE},
                    new NamedColor {ColorName = "Wheat", ColorCode = 0xFFF5DEB3},
                    new NamedColor {ColorName = "White", ColorCode = 0xFFFFFFFF},
                    new NamedColor {ColorName = "WhiteSmoke", ColorCode = 0xFFF5F5F5},
                    new NamedColor {ColorName = "Yellow", ColorCode = 0xFFFFFF00},
                    new NamedColor {ColorName = "YellowGreen", ColorCode = 0xFF9ACD32}
                };
                return _list;
            }
        }

        /// <summary>
        /// Gets a list of names of all named colors
        /// </summary>
        public static List<string> Names
        {
            get
            {
                return List.Names;
            }
        }
        /// <summary>
        /// Gets a list of codes (ARGB values) of all named colors
        /// </summary>
        public static List<uint> Codes
        {
            get
            {
                return List.Codes;
            }
        }
        /// <summary>
        /// Gets a list of <see cref="Color"/> of all named colors
        /// </summary>
        public static List<Color> Colors
        {
            get
            {
                return List.Colors;
            }
        } 
        #endregion

    #region NamedColors
	
        //TODO: add more lists
        public class Collections
        {
      
            public static NameColorList GrayColors
            {
                get
                {
                    var list = new NameColorList
                    {
                        LightGray, DarkGray, Gray, DimGray, Black,
                    };
                    return list;
                }
            }
            public static NameColorList BlueColors
            {
                get
                {
                    var list = new NameColorList
                    {
                          DeepSkyBlue, DodgerBlue, RoyalBlue, Blue, MediumBlue, DarkBlue, 
                     //  LightBlue, DodgerBlue, DarkBlue,
                    };
                    return list;
                }
            }
            public static NameColorList BlueToRedColors
            {
                get
                {
                    var list = new NameColorList
                    {
                        NamedColor.FromHex("#FF1AA1E2"),
                        NamedColor.FromHex("#FF4AC4FF"),
                        NamedColor.FromHex("#FFB5CC2E"),
                        NamedColor.FromHex("#FFFFD034"),
                        NamedColor.FromHex("#FFFC8612"),
                        NamedColor.FromHex("#FFED4840"),
                        NamedColor.FromHex("#FF7D120D"),
                    };
                    return list;
                }
            }
            public static NameColorList RedColors
            {
                get
                {
                    var list = new NameColorList
                    {
                        NamedColor.FromHex("#FFDFCC41"),
                        NamedColor.FromHex("#FFE58220"),
                        NamedColor.FromHex("#FFA11A13"),
                    };
                    return list;
                }
            }
            public static NameColorList GreenColors
            {
                get
                {
                    var list = new NameColorList
                    {
                        NamedColor.FromHex("#FF7EE874"),
                        NamedColor.FromHex("#FF409F37"),
                        NamedColors.DarkGreen,
                        //NamedColor.FromHex("DarkGreen"),
                    };
                    return list;
                }
            }
            public static NameColorList PurpleColors
            {
                get
                {
                    var list = new NameColorList
                    {
                        NamedColor.FromHex("#FF9886D1"),
                        NamedColor.FromHex("#FF6F5BAF"),
                        NamedColor.FromHex("#FF2E1A6F"),
                    };
                    return list;
                }
            }
            public static NameColorList BrownColors
            {
                get
                {
                    var list = new NameColorList
                    {
                        NamedColor.FromHex("#FFE8C87B"),
                        NamedColor.FromHex("#FFBF9E51"),
                        NamedColor.FromHex("#FF785A11"),
                    };
                    return list;
                }
            }
          
        }
    #region Gray Colors
        
        public static NamedColor Black = new NamedColor { ColorName = "Black", ColorCode = 0xFF000000 };
        public static NamedColor DimGray = new NamedColor { ColorName = "DimGray", ColorCode = 0xFF696969 };
        public static NamedColor Gray = new NamedColor { ColorName = "Gray", ColorCode = 0xFF808080};
        public static NamedColor DarkGray = new NamedColor { ColorName = "DarkGray", ColorCode = 0xFFA9A9A9 };
        public static NamedColor Silver = new NamedColor { ColorName = "Silver", ColorCode = 0xFFC0C0C0 };
        public static NamedColor Gainsboro = new NamedColor { ColorName = "Gainsboro", ColorCode = 0xFFDCDCDC };
        public static NamedColor LightGray = new NamedColor { ColorName = "LightGray", ColorCode = 0xFFD3D3D3};
        public static NamedColor SlateGray = new NamedColor { ColorName = "SlateGray", ColorCode = 0xFF708090};
        public static NamedColor LightSlateGray = new NamedColor { ColorName = "LightSlateGray", ColorCode = 0xFF778899};
        public static NamedColor Transparent = new NamedColor { ColorName = "Transparent", ColorCode = 0x00FFFFFF };
     
    #endregion
    #region Light Colors
  
        public static NamedColor AliceBlue = new NamedColor { ColorName = "AliceBlue", ColorCode = 0xFFF0F8FF};
     
        public static NamedColor AntiqueWhite = new NamedColor { ColorName = "AntiqueWhite", ColorCode = 0xFFFAEBD7};
        public static NamedColor Azure = new NamedColor { ColorName = "Azure", ColorCode = 0xFFF0FFFF};
        public static NamedColor Bisque = new NamedColor { ColorName = "Bisque", ColorCode = 0xFFFFE4C4};
        public static NamedColor BlanchedAlmond = new NamedColor { ColorName = "BlanchedAlmond", ColorCode = 0xFFFFEBCD};
        public static NamedColor CornSilk = new NamedColor { ColorName = "CornSilk", ColorCode = 0xFFFFF8DC};
        public static NamedColor Ivory = new NamedColor { ColorName = "Ivory", ColorCode = 0xFFFFFFF0};
        public static NamedColor Lavender = new NamedColor { ColorName = "Lavender", ColorCode = 0xFFE6E6FA};
        public static NamedColor LavenderBlush = new NamedColor { ColorName = "LavenderBlush", ColorCode = 0xFFFFF0F5};
        public static NamedColor FloralWhite = new NamedColor { ColorName = "FloralWhite", ColorCode = 0xFFFFFAF0};
        public static NamedColor GhostWhite = new NamedColor { ColorName = "GhostWhite", ColorCode = 0xFFF8F8FF};
        public static NamedColor Honeydew = new NamedColor { ColorName = "Honeydew", ColorCode = 0xFFF0FFF0};
        public static NamedColor Linen = new NamedColor { ColorName = "Linen", ColorCode = 0xFFFAF0E6};
        public static NamedColor MintCream = new NamedColor { ColorName = "MintCream", ColorCode = 0xFFF5FFFA};
        public static NamedColor MistyRose = new NamedColor { ColorName = "MistyRose", ColorCode = 0xFFFFE4E1};
        public static NamedColor NavajoWhite = new NamedColor { ColorName = "NavajoWhite", ColorCode = 0xFFFFDEAD};
        public static NamedColor OldLace = new NamedColor { ColorName = "OldLace", ColorCode = 0xFFFDF5E6};
        public static NamedColor PapayaWhip = new NamedColor { ColorName = "PapayaWhip", ColorCode = 0xFFFFEFD5};
        public static NamedColor Wheat = new NamedColor { ColorName = "Wheat", ColorCode = 0xFFF5DEB3};
        public static NamedColor White = new NamedColor { ColorName = "White", ColorCode = 0xFFFFFFFF};
        public static NamedColor WhiteSmoke = new NamedColor { ColorName = "WhiteSmoke", ColorCode = 0xFFF5F5F5};
   
        public static NamedColor Snow = new NamedColor { ColorName = "Snow", ColorCode = 0xFFFFFAFA};
        public static NamedColor SeaShell = new NamedColor { ColorName = "SeaShell", ColorCode = 0xFFFFF5EE};
      
    #endregion
    #region Red Colors

        public static NamedColor Chocolate = new NamedColor { ColorName = "Chocolate", ColorCode = 0xFFD2691E};
        public static NamedColor Coral = new NamedColor { ColorName = "Coral", ColorCode = 0xFFFF7F50};
        public static NamedColor Crimson = new NamedColor { ColorName = "Crimson", ColorCode = 0xFFDC143C};
        public static NamedColor DarkRed = new NamedColor { ColorName = "DarkRed", ColorCode = 0xFF8B0000};
        public static NamedColor DarkSalmon = new NamedColor { ColorName = "DarkSalmon", ColorCode = 0xFFE9967A};
        public static NamedColor Firebrick = new NamedColor { ColorName = "Firebrick", ColorCode = 0xFFB22222};
        public static NamedColor DeepPink = new NamedColor { ColorName = "DeepPink", ColorCode = 0xFFFF1493};
        public static NamedColor LightCoral = new NamedColor { ColorName = "LightCoral", ColorCode = 0xFFF08080};
        public static NamedColor IndianRed = new NamedColor { ColorName = "IndianRed", ColorCode = 0xFFCD5C5C};
        public static NamedColor HotPink = new NamedColor { ColorName = "HotPink", ColorCode = 0xFFFF69B4};
        public static NamedColor Maroon = new NamedColor { ColorName = "Maroon", ColorCode = 0xFF800000};
        public static NamedColor MediumVioletRed = new NamedColor { ColorName = "MediumVioletRed", ColorCode = 0xFFC71585};
        public static NamedColor OrangeRed = new NamedColor { ColorName = "OrangeRed", ColorCode = 0xFFFF4500};
        public static NamedColor Red = new NamedColor { ColorName = "Red", ColorCode = 0xFFFF0000};
        public static NamedColor Tomato = new NamedColor { ColorName = "Tomato", ColorCode = 0xFFFF6347};
        public static NamedColor Salmon = new NamedColor { ColorName = "Salmon", ColorCode = 0xFFFA8072};
        public static NamedColor PaleVioletRed = new NamedColor { ColorName = "PaleVioletRed", ColorCode = 0xFFDB7093};
      
    #endregion
    #region Brown Colors

        public static NamedColor Brown = new NamedColor { ColorName = "Brown", ColorCode = 0xFFA52A2A };
        public static NamedColor DarkGoldenrod = new NamedColor { ColorName = "DarkGoldenrod", ColorCode = 0xFFB8860B };
        public static NamedColor RosyBrown = new NamedColor { ColorName = "RosyBrown", ColorCode = 0xFFBC8F8F };
        public static NamedColor SaddleBrown = new NamedColor { ColorName = "SaddleBrown", ColorCode = 0xFF8B4513 };
        public static NamedColor Peru = new NamedColor { ColorName = "Peru", ColorCode = 0xFFCD853F };
        public static NamedColor SandyBrown = new NamedColor { ColorName = "SandyBrown", ColorCode = 0xFFF4A460 };
        public static NamedColor Sienna = new NamedColor { ColorName = "Sienna", ColorCode = 0xFFA0522D };
        public static NamedColor Tan = new NamedColor { ColorName = "Tan", ColorCode = 0xFFD2B48C };
        public static NamedColor PeachPuff = new NamedColor { ColorName = "PeachPuff", ColorCode = 0xFFFFDAB9 };
        public static NamedColor BurlyWood = new NamedColor { ColorName = "BurlyWood", ColorCode = 0xFFDEB887 };
   
    #endregion
    #region Yellow Colors

        public static NamedColor Beige = new NamedColor { ColorName = "Beige", ColorCode = 0xFFF5F5DC};
        public static NamedColor LightYellow = new NamedColor { ColorName = "LightYellow", ColorCode = 0xFFFFFFE0};
        public static NamedColor LightGoldenrodYellow = new NamedColor { ColorName = "LightGoldenrodYellow", ColorCode = 0xFFFAFAD2};
        public static NamedColor Khaki = new NamedColor { ColorName = "Khaki", ColorCode = 0xFFF0E68C};
        public static NamedColor LemonChiffon = new NamedColor { ColorName = "LemonChiffon", ColorCode = 0xFFFFFACD};
        public static NamedColor DarkKhaki = new NamedColor { ColorName = "DarkKhaki", ColorCode = 0xFFBDB76B};
        public static NamedColor Moccasin = new NamedColor { ColorName = "Moccasin", ColorCode = 0xFFFFE4B5};
        public static NamedColor Gold = new NamedColor { ColorName = "Gold", ColorCode = 0xFFFFD700};
        public static NamedColor Goldenrod = new NamedColor { ColorName = "Goldenrod", ColorCode = 0xFFDAA520};
        public static NamedColor DarkOrange = new NamedColor { ColorName = "DarkOrange", ColorCode = 0xFFFF8C00};
        public static NamedColor LightPink = new NamedColor { ColorName = "LightPink", ColorCode = 0xFFFFB6C1};
        public static NamedColor LightSalmon = new NamedColor { ColorName = "LightSalmon", ColorCode = 0xFFFFA07A};
        public static NamedColor Yellow = new NamedColor { ColorName = "Yellow", ColorCode = 0xFFFFFF00};
        public static NamedColor Orange = new NamedColor { ColorName = "Orange", ColorCode = 0xFFFFA500};
        public static NamedColor PaleGoldenrod = new NamedColor { ColorName = "PaleGoldenrod", ColorCode = 0xFFEEE8AA};
     

    #endregion

    #region Purple Colors

        public static NamedColor Purple = new NamedColor { ColorName = "Purple", ColorCode = 0xFF800080};
        public static NamedColor Fuchsia = new NamedColor { ColorName = "Fuchsia", ColorCode = 0xFFFF00FF};
        public static NamedColor Magenta = new NamedColor { ColorName = "Magenta", ColorCode = 0xFFFF00FF};
        public static NamedColor BlueViolet = new NamedColor { ColorName = "BlueViolet", ColorCode = 0xFF8A2BE2};
        public static NamedColor DarkMagenta = new NamedColor { ColorName = "DarkMagenta", ColorCode = 0xFF8B008B};
        public static NamedColor DarkOrchid = new NamedColor { ColorName = "DarkOrchid", ColorCode = 0xFF9932CC};
        public static NamedColor DarkSlateBlue = new NamedColor { ColorName = "DarkSlateBlue", ColorCode = 0xFF483D8B};
        public static NamedColor DarkViolet = new NamedColor { ColorName = "DarkViolet", ColorCode = 0xFF9400D3};
        public static NamedColor Indigo = new NamedColor { ColorName = "Indigo", ColorCode = 0xFF4B0082};
        public static NamedColor MediumOrchid = new NamedColor { ColorName = "MediumOrchid", ColorCode = 0xFFBA55D3};
        public static NamedColor MediumPurple = new NamedColor { ColorName = "MediumPurple", ColorCode = 0xFF9370DB};
        public static NamedColor MediumSlateBlue = new NamedColor { ColorName = "MediumSlateBlue", ColorCode = 0xFF7B68EE};
        public static NamedColor SlateBlue = new NamedColor { ColorName = "SlateBlue", ColorCode = 0xFF6A5ACD};
        public static NamedColor Orchid = new NamedColor { ColorName = "Orchid", ColorCode = 0xFFDA70D6};
        public static NamedColor Violet = new NamedColor { ColorName = "Violet", ColorCode = 0xFFEE82EE};
        public static NamedColor Plum = new NamedColor { ColorName = "Plum", ColorCode = 0xFFDDA0DD};
        public static NamedColor Thistle = new NamedColor { ColorName = "Thistle", ColorCode = 0xFFD8BFD8};
        public static NamedColor Pink = new NamedColor { ColorName = "Pink", ColorCode = 0xFFFFC0CB};
      
    #endregion
  
    #region Blue Colors

        public static NamedColor DarkBlue = new NamedColor { ColorName = "DarkBlue", ColorCode = 0xFF00008B };
        public static NamedColor MediumBlue = new NamedColor { ColorName = "MediumBlue", ColorCode = 0xFF0000CD };
        public static NamedColor Blue = new NamedColor { ColorName = "Blue", ColorCode = 0xFF0000FF };
        public static NamedColor RoyalBlue = new NamedColor { ColorName = "RoyalBlue", ColorCode = 0xFF4169E1 };
        public static NamedColor CornflowerBlue = new NamedColor { ColorName = "CornflowerBlue", ColorCode = 0xFF6495ED };
        public static NamedColor DodgerBlue = new NamedColor { ColorName = "DodgerBlue", ColorCode = 0xFF1E90FF };
        public static NamedColor DeepSkyBlue = new NamedColor { ColorName = "DeepSkyBlue", ColorCode = 0xFF00BFFF };
      

        public static NamedColor LightCyan = new NamedColor { ColorName = "LightCyan", ColorCode = 0xFFE0FFFF};
        public static NamedColor Aqua = new NamedColor { ColorName = "Aqua", ColorCode = 0xFF00FFFF};
        public static NamedColor Aquamarine = new NamedColor { ColorName = "Aquamarine", ColorCode = 0xFF7FFFD4};
        public static NamedColor CadetBlue = new NamedColor { ColorName = "CadetBlue", ColorCode = 0xFF5F9EA0};
        public static NamedColor Cyan = new NamedColor { ColorName = "Cyan", ColorCode = 0xFF00FFFF};
        public static NamedColor DarkCyan = new NamedColor { ColorName = "DarkCyan", ColorCode = 0xFF008B8B};
        public static NamedColor MediumAquamarine = new NamedColor { ColorName = "MediumAquamarine", ColorCode = 0xFF66CDAA};
        public static NamedColor MediumTurquoise = new NamedColor { ColorName = "MediumTurquoise", ColorCode = 0xFF48D1CC};
        public static NamedColor DarkTurquoise = new NamedColor { ColorName = "DarkTurquoise", ColorCode = 0xFF00CED1};
        public static NamedColor MidnightBlue = new NamedColor { ColorName = "MidnightBlue", ColorCode = 0xFF191970};
        public static NamedColor Navy = new NamedColor { ColorName = "Navy", ColorCode = 0xFF000080};
        public static NamedColor LightBlue = new NamedColor { ColorName = "LightBlue", ColorCode = 0xFFADD8E6};
        public static NamedColor LightSkyBlue = new NamedColor { ColorName = "LightSkyBlue", ColorCode = 0xFF87CEFA};
        public static NamedColor LightSteelBlue = new NamedColor { ColorName = "LightSteelBlue", ColorCode = 0xFFB0C4DE};
        public static NamedColor SkyBlue = new NamedColor { ColorName = "SkyBlue", ColorCode = 0xFF87CEEB};
        public static NamedColor SteelBlue = new NamedColor { ColorName = "SteelBlue", ColorCode = 0xFF4682B4};
        public static NamedColor PaleTurquoise = new NamedColor { ColorName = "PaleTurquoise", ColorCode = 0xFFAFEEEE};
        public static NamedColor PowderBlue = new NamedColor { ColorName = "PowderBlue", ColorCode = 0xFFB0E0E6};
        public static NamedColor Turquoise = new NamedColor { ColorName = "Turquoise", ColorCode = 0xFF40E0D0};
        public static NamedColor Teal = new NamedColor { ColorName = "Teal", ColorCode = 0xFF008080};
     
    #endregion
    #region Green Colors
        public static NamedColor Chartreuse = new NamedColor { ColorName = "Chartreuse", ColorCode = 0xFF7FFF00};
        public static NamedColor DarkGreen = new NamedColor { ColorName = "DarkGreen", ColorCode = 0xFF006400};
        public static NamedColor DarkOliveGreen = new NamedColor { ColorName = "DarkOliveGreen", ColorCode = 0xFF556B2F};
        public static NamedColor DarkSeaGreen = new NamedColor { ColorName = "DarkSeaGreen", ColorCode = 0xFF8FBC8F};
        public static NamedColor DarkSlateGray = new NamedColor { ColorName = "DarkSlateGray", ColorCode = 0xFF2F4F4F};
        public static NamedColor ForestGreen = new NamedColor { ColorName = "ForestGreen", ColorCode = 0xFF228B22};
        public static NamedColor Green = new NamedColor { ColorName = "Green", ColorCode = 0xFF008000};
        public static NamedColor GreenYellow = new NamedColor { ColorName = "GreenYellow", ColorCode = 0xFFADFF2F};
        public static NamedColor LawnGreen = new NamedColor { ColorName = "LawnGreen", ColorCode = 0xFF7CFC00};
        public static NamedColor LightGreen = new NamedColor { ColorName = "LightGreen", ColorCode = 0xFF90EE90};
        public static NamedColor Lime = new NamedColor { ColorName = "Lime", ColorCode = 0xFF00FF00};
        public static NamedColor LimeGreen = new NamedColor { ColorName = "LimeGreen", ColorCode = 0xFF32CD32};
        public static NamedColor MediumSeaGreen = new NamedColor { ColorName = "MediumSeaGreen", ColorCode = 0xFF3CB371};
        public static NamedColor MediumSpringGreen = new NamedColor { ColorName = "MediumSpringGreen", ColorCode = 0xFF00FA9A};
        public static NamedColor PaleGreen = new NamedColor { ColorName = "PaleGreen", ColorCode = 0xFF98FB98};
        public static NamedColor SeaGreen = new NamedColor { ColorName = "SeaGreen", ColorCode = 0xFF2E8B57};
        public static NamedColor SpringGreen = new NamedColor { ColorName = "SpringGreen", ColorCode = 0xFF00FF7F};
        public static NamedColor YellowGreen = new NamedColor {ColorName = "YellowGreen", ColorCode = 0xFF9ACD32};
        public static NamedColor LightSeaGreen = new NamedColor { ColorName = "LightSeaGreen", ColorCode = 0xFF20B2AA};
        public static NamedColor Olive = new NamedColor { ColorName = "Olive", ColorCode = 0xFF808000};
        public static NamedColor OliveDrab = new NamedColor { ColorName = "OliveDrab", ColorCode = 0xFF6B8E23};
      
    #endregion

   	#endregion

        //public NamedColors()
        //{
        //    Colors = new List<NamedColor> 
        //    {
        //        new NamedColor{ColorName = "AliceBlue", ColorCode = 0xFFF0F8FF},
        //        new NamedColor{ColorName = "AntiqueWhite", ColorCode = 0xFFFAEBD7},
        //        new NamedColor{ColorName = "Aqua", ColorCode = 0xFF00FFFF},
        //        new NamedColor{ColorName = "Aquamarine", ColorCode = 0xFF7FFFD4},
        //        new NamedColor{ColorName = "Azure", ColorCode = 0xFFF0FFFF},
        //        new NamedColor{ColorName = "Beige", ColorCode = 0xFFF5F5DC},
        //        new NamedColor{ColorName = "Bisque", ColorCode = 0xFFFFE4C4},
        //        new NamedColor{ColorName = "Black", ColorCode = 0xFF000000},
        //        new NamedColor{ColorName = "BlanchedAlmond", ColorCode = 0xFFFFEBCD},
        //        new NamedColor{ColorName = "Blue", ColorCode = 0xFF0000FF},
        //        new NamedColor{ColorName = "BlueViolet", ColorCode = 0xFF8A2BE2},
        //        new NamedColor{ColorName = "Brown", ColorCode = 0xFFA52A2A},
        //        new NamedColor{ColorName = "BurlyWood", ColorCode = 0xFFDEB887},
        //        new NamedColor{ColorName = "CadetBlue", ColorCode = 0xFF5F9EA0},
        //        new NamedColor{ColorName = "Chartreuse", ColorCode = 0xFF7FFF00},
        //        new NamedColor{ColorName = "Chocolate", ColorCode = 0xFFD2691E},
        //        new NamedColor{ColorName = "Coral", ColorCode = 0xFFFF7F50},
        //        new NamedColor{ColorName = "CornflowerBlue", ColorCode = 0xFF6495ED},
        //        new NamedColor{ColorName = "CornSilk", ColorCode = 0xFFFFF8DC},
        //        new NamedColor{ColorName = "Crimson", ColorCode = 0xFFDC143C},
        //        new NamedColor{ColorName = "Cyan", ColorCode = 0xFF00FFFF},
        //        new NamedColor{ColorName = "DarkBlue", ColorCode = 0xFF00008B},
        //        new NamedColor{ColorName = "DarkCyan", ColorCode = 0xFF008B8B},
        //        new NamedColor{ColorName = "DarkGoldenrod", ColorCode = 0xFFB8860B},
        //        new NamedColor{ColorName = "DarkGray", ColorCode = 0xFFA9A9A9},
        //        new NamedColor{ColorName = "DarkGreen", ColorCode = 0xFF006400},
        //        new NamedColor{ColorName = "DarkKhaki", ColorCode = 0xFFBDB76B},
        //        new NamedColor{ColorName = "DarkMagenta", ColorCode = 0xFF8B008B},
        //        new NamedColor{ColorName = "DarkOliveGreen", ColorCode = 0xFF556B2F},
        //        new NamedColor{ColorName = "DarkOrange", ColorCode = 0xFFFF8C00},
        //        new NamedColor{ColorName = "DarkOrchid", ColorCode = 0xFF9932CC},
        //        new NamedColor{ColorName = "DarkRed", ColorCode = 0xFF8B0000},
        //        new NamedColor{ColorName = "DarkSalmon", ColorCode = 0xFFE9967A},
        //        new NamedColor{ColorName = "DarkSeaGreen", ColorCode = 0xFF8FBC8F},
        //        new NamedColor{ColorName = "DarkSlateBlue", ColorCode = 0xFF483D8B},
        //        new NamedColor{ColorName = "DarkSlateGray", ColorCode = 0xFF2F4F4F},
        //        new NamedColor{ColorName = "DarkTurquoise", ColorCode = 0xFF00CED1},
        //        new NamedColor{ColorName = "DarkViolet", ColorCode = 0xFF9400D3},
        //        new NamedColor{ColorName = "DeepPink", ColorCode = 0xFFFF1493},
        //        new NamedColor{ColorName = "DeepSkyBlue", ColorCode = 0xFF00BFFF},
        //        new NamedColor{ColorName = "DimGray", ColorCode = 0xFF696969},
        //        new NamedColor{ColorName = "DodgerBlue", ColorCode = 0xFF1E90FF},
        //        new NamedColor{ColorName = "Firebrick", ColorCode = 0xFFB22222},
        //        new NamedColor{ColorName = "FloralWhite", ColorCode = 0xFFFFFAF0},
        //        new NamedColor{ColorName = "ForestGreen", ColorCode = 0xFF228B22},
        //        new NamedColor{ColorName = "Fuchsia", ColorCode = 0xFFFF00FF},
        //        new NamedColor{ColorName = "Gainesboro", ColorCode = 0xFFDCDCDC},
        //        new NamedColor{ColorName = "GhostWhite", ColorCode = 0xFFF8F8FF},
        //        new NamedColor{ColorName = "Gold", ColorCode = 0xFFFFD700},
        //        new NamedColor{ColorName = "Goldenrod", ColorCode = 0xFFDAA520},
        //        new NamedColor{ColorName = "Gray", ColorCode = 0xFF808080},
        //        new NamedColor{ColorName = "Green", ColorCode = 0xFF008000},
        //        new NamedColor{ColorName = "GreenYellow", ColorCode = 0xFFADFF2F},
        //        new NamedColor{ColorName = "Honeydew", ColorCode = 0xFFF0FFF0},
        //        new NamedColor{ColorName = "HotPink", ColorCode = 0xFFFF69B4},
        //        new NamedColor{ColorName = "IndianRed", ColorCode = 0xFFCD5C5C},
        //        new NamedColor{ColorName = "Indigo", ColorCode = 0xFF4B0082},
        //        new NamedColor{ColorName = "Ivory", ColorCode = 0xFFFFFFF0},
        //        new NamedColor{ColorName = "Khaki", ColorCode = 0xFFF0E68C},
        //        new NamedColor{ColorName = "Lavender", ColorCode = 0xFFE6E6FA},
        //        new NamedColor{ColorName = "LavenderBlush", ColorCode = 0xFFFFF0F5},
        //        new NamedColor{ColorName = "LawnGreen", ColorCode = 0xFF7CFC00},
        //        new NamedColor{ColorName = "LemonChiffon", ColorCode = 0xFFFFFACD},
        //        new NamedColor{ColorName = "LightBlue", ColorCode = 0xFFADD8E6},
        //        new NamedColor{ColorName = "LightCoral", ColorCode = 0xFFF08080},
        //        new NamedColor{ColorName = "LightCyan", ColorCode = 0xFFE0FFFF},
        //        new NamedColor{ColorName = "LightGoldenrodYellow", ColorCode = 0xFFFAFAD2},
        //        new NamedColor{ColorName = "LightGray", ColorCode = 0xFFD3D3D3},
        //        new NamedColor{ColorName = "LightGreen", ColorCode = 0xFF90EE90},
        //        new NamedColor{ColorName = "LightPink", ColorCode = 0xFFFFB6C1},
        //        new NamedColor{ColorName = "LightSalmon", ColorCode = 0xFFFFA07A},
        //        new NamedColor{ColorName = "LightSeaGreen", ColorCode = 0xFF20B2AA},
        //        new NamedColor{ColorName = "LightSkyBlue", ColorCode = 0xFF87CEFA},
        //        new NamedColor{ColorName = "LightSlateGray", ColorCode = 0xFF778899},
        //        new NamedColor{ColorName = "LightSteelBlue", ColorCode = 0xFFB0C4DE},
        //        new NamedColor{ColorName = "LightYellow", ColorCode = 0xFFFFFFE0},
        //        new NamedColor{ColorName = "Lime", ColorCode = 0xFF00FF00},
        //        new NamedColor{ColorName = "LimeGreen", ColorCode = 0xFF32CD32},
        //        new NamedColor{ColorName = "Linen", ColorCode = 0xFFFAF0E6},
        //        new NamedColor{ColorName = "Magenta", ColorCode = 0xFFFF00FF},
        //        new NamedColor{ColorName = "Maroon", ColorCode = 0xFF800000},
        //        new NamedColor{ColorName = "MediumAquamarine", ColorCode = 0xFF66CDAA},
        //        new NamedColor{ColorName = "MediumBlue", ColorCode = 0xFF0000CD},
        //        new NamedColor{ColorName = "MediumOrchid", ColorCode = 0xFFBA55D3},
        //        new NamedColor{ColorName = "MediumPurple", ColorCode = 0xFF9370DB},
        //        new NamedColor{ColorName = "MediumSeaGreen", ColorCode = 0xFF3CB371},
        //        new NamedColor{ColorName = "MediumSlateBlue", ColorCode = 0xFF7B68EE},
        //        new NamedColor{ColorName = "MediumSpringGreen", ColorCode = 0xFF00FA9A},
        //        new NamedColor{ColorName = "MediumTurquoise", ColorCode = 0xFF48D1CC},
        //        new NamedColor{ColorName = "MediumVioletRed", ColorCode = 0xFFC71585},
        //        new NamedColor{ColorName = "MidnightBlue", ColorCode = 0xFF191970},
        //        new NamedColor{ColorName = "MintCream", ColorCode = 0xFFF5FFFA},
        //        new NamedColor{ColorName = "MistyRose", ColorCode = 0xFFFFE4E1},
        //        new NamedColor{ColorName = "Moccasin", ColorCode = 0xFFFFE4B5},
        //        new NamedColor{ColorName = "NavajoWhite", ColorCode = 0xFFFFDEAD},
        //        new NamedColor{ColorName = "Navy", ColorCode = 0xFF000080},
        //        new NamedColor{ColorName = "OldLace", ColorCode = 0xFFFDF5E6},
        //        new NamedColor{ColorName = "Olive", ColorCode = 0xFF808000},
        //        new NamedColor{ColorName = "OliveDrab", ColorCode = 0xFF6B8E23},
        //        new NamedColor{ColorName = "Orange", ColorCode = 0xFFFFA500},
        //        new NamedColor{ColorName = "OrangeRed", ColorCode = 0xFFFF4500},
        //        new NamedColor{ColorName = "Orchid", ColorCode = 0xFFDA70D6},
        //        new NamedColor{ColorName = "PaleGoldenrod", ColorCode = 0xFFEEE8AA},
        //        new NamedColor{ColorName = "PaleGreen", ColorCode = 0xFF98FB98},
        //        new NamedColor{ColorName = "PaleTurquoise", ColorCode = 0xFFAFEEEE},
        //        new NamedColor{ColorName = "PaleVioletRed", ColorCode = 0xFFDB7093},
        //        new NamedColor{ColorName = "PapayaWhip", ColorCode = 0xFFFFEFD5},
        //        new NamedColor{ColorName = "PeachPuff", ColorCode = 0xFFFFDAB9},
        //        new NamedColor{ColorName = "Peru", ColorCode = 0xFFCD853F},
        //        new NamedColor{ColorName = "Pink", ColorCode = 0xFFFFC0CB},
        //        new NamedColor{ColorName = "Plum", ColorCode = 0xFFDDA0DD},
        //        new NamedColor{ColorName = "PowderBlue", ColorCode = 0xFFB0E0E6},
        //        new NamedColor{ColorName = "Purple", ColorCode = 0xFF800080},
        //        new NamedColor{ColorName = "Red", ColorCode = 0xFFFF0000},
        //        new NamedColor{ColorName = "RosyBrown", ColorCode = 0xFFBC8F8F},
        //        new NamedColor{ColorName = "RoyalBlue", ColorCode = 0xFF4169E1},
        //        new NamedColor{ColorName = "SaddleBrown", ColorCode = 0xFF8B4513},
        //        new NamedColor{ColorName = "Salmon", ColorCode = 0xFFFA8072},
        //        new NamedColor{ColorName = "SandyBrown", ColorCode = 0xFFF4A460},
        //        new NamedColor{ColorName = "SeaGreen", ColorCode = 0xFF2E8B57},
        //        new NamedColor{ColorName = "SeaShell", ColorCode = 0xFFFFF5EE},
        //        new NamedColor{ColorName = "Sienna", ColorCode = 0xFFA0522D},
        //        new NamedColor{ColorName = "Silver", ColorCode = 0xFFC0C0C0},
        //        new NamedColor{ColorName = "SkyBlue", ColorCode = 0xFF87CEEB},
        //        new NamedColor{ColorName = "SlateBlue", ColorCode = 0xFF6A5ACD},
        //        new NamedColor{ColorName = "SlateGray", ColorCode = 0xFF708090},
        //        new NamedColor{ColorName = "Snow", ColorCode = 0xFFFFFAFA},
        //        new NamedColor{ColorName = "SpringGreen", ColorCode = 0xFF00FF7F},
        //        new NamedColor{ColorName = "SteelBlue", ColorCode = 0xFF4682B4},
        //        new NamedColor{ColorName = "Tan", ColorCode = 0xFFD2B48C},
        //        new NamedColor{ColorName = "Teal", ColorCode = 0xFF008080},
        //        new NamedColor{ColorName = "Thistle", ColorCode = 0xFFD8BFD8},
        //        new NamedColor{ColorName = "Tomato", ColorCode = 0xFFFF6347},
        //        new NamedColor{ColorName = "Transparent", ColorCode = 0x00FFFFFF},
        //        new NamedColor{ColorName = "Turquoise", ColorCode = 0xFF40E0D0},
        //        new NamedColor{ColorName = "Violet", ColorCode = 0xFFEE82EE},
        //        new NamedColor{ColorName = "Wheat", ColorCode = 0xFFF5DEB3},
        //        new NamedColor{ColorName = "White", ColorCode = 0xFFFFFFFF},
        //        new NamedColor{ColorName = "WhiteSmoke", ColorCode = 0xFFF5F5F5},
        //        new NamedColor{ColorName = "Yellow", ColorCode = 0xFFFFFF00},
        //        new NamedColor{ColorName = "YellowGreen", ColorCode = 0xFF9ACD32}
        //    };
        //}
  
    }

} 