using System.Collections.Generic;

namespace System
{

    /// <summary>
    /// Represents a paint class similar to a Color but additional functionalities 
    /// </summary>
    public class Paint
    {
        public Paint()
            : this(255, 255, 255, 255)
        { }
        public Paint(byte r, byte g, byte b)
            : this(r, g, b, 255)
        { }

        public Paint(byte r, byte g, byte b, byte a)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }
        public Paint(string name)
        {
            var p = FromString(name);
            A = p.A;
            R = p.R;
            G = p.G;
            B = p.B;
        }

        #region Properties
        /// <summary> Gets or sets R component (0-255) </summary>
        public byte R { get; set; }
        /// <summary> Gets or sets G component (0-255) </summary>
        public byte G { get; set; }
        /// <summary> Gets or sets B component (0-255) </summary>
        public byte B { get; set; }
        /// <summary> Gets or sets A component (0-255) </summary>
        public byte A { get; set; }
        #endregion

        #region Convertion
        ///// <summary> 
        ///// Converts ARGB bytes to a <see cref="Paint"/> 
        ///// </summary>
        //public static Paint FromArgb(byte a, byte r, byte g, byte b)
        //{
        //    return new Paint(a, r, g, b);
        //}
        /// <summary> 
        /// Converts RGB bytes to a <see cref="Paint"/> 
        /// </summary>
        public static Paint FromRgb(byte r, byte g, byte b)
        {
            return new Paint(r, g, b);
        }
        /// <summary> 
        /// Converts RGBA bytes to a <see cref="Paint"/> 
        /// </summary>
        public static Paint FromRgba(byte r, byte g, byte b, byte a)
        {
            return new Paint(r, g, b, a);
        }
        /// <summary> 
        /// Converts ARGB bytes to a <see cref="Paint"/> 
        /// </summary>
        public static Paint FromArgb(byte a, byte r, byte g, byte b)
        {
            return new Paint(r, g, b, a);
        }
        /// <summary> 
        /// Converts integer pixel (ARGB) to a <see cref="Paint"/>  
        /// </summary>
        public static Paint FromInt(int value)
        {
            return FromPixel(value);
        }
        /// <summary> 
        /// Converts integer pixel (ARGB) to a <see cref="Paint"/>  
        /// </summary>
        public static Paint FromPixel(int pixel)
        {
            var b = (byte)(pixel & 0xFF); pixel >>= 8;
            var g = (byte)(pixel & 0xFF); pixel >>= 8;
            var r = (byte)(pixel & 0xFF); pixel >>= 8;
            var a = (byte)pixel; // alpha
            var ret = FromRgba(r, g, b, a);
            return ret;
        }
        /// <summary> 
        /// Converts current <see cref="Paint"/> to its integer representation for use in bitmaps 
        /// </summary>
        public int ToPixel()
        {
            var aa = A / 255.0;
            var rr = (int)(R * aa);
            var gg = (int)(G * aa);
            var bb = (int)(B * aa);
            return this.A << 24 | rr << 16 | gg << 8 | bb;
        }
        /// <summary> 
        /// Converts current <see cref="Paint"/> to its rgba string representation
        /// </summary>
        public string ToRgba()
        {
            var ret = "rgba(" + R + "," + G + "," + B + "," + A / 255.0 + ")";
            return ret;
        }
        /// <summary>
        /// Converts current <see cref="Paint"/> to its hex string representation
        /// </summary>
        /// <returns>eg: "#FFFFFFFF", "#AAAB12E9"</returns>
        public string ToHex(string format = "ARGB")
        {
            var a = A.ToString("X").Length == 1 ? String.Format("0{0}", A.ToString("X")) : A.ToString("X");
            var r = R.ToString("X").Length == 1 ? String.Format("0{0}", R.ToString("X")) : R.ToString("X");
            var g = G.ToString("X").Length == 1 ? String.Format("0{0}", G.ToString("X")) : G.ToString("X");
            var b = B.ToString("X").Length == 1 ? String.Format("0{0}", B.ToString("X")) : B.ToString("X");

            var hex = "#";
            if (format.ToUpper().Contains("A")) hex += a;
            if (format.ToUpper().Contains("R")) hex += r;
            if (format.ToUpper().Contains("G")) hex += g;
            if (format.ToUpper().Contains("B")) hex += b;

            return hex;
            //return String.Format("#{0}{1}{2}{3}",
            //    A.ToString("X").Length == 1 ? String.Format("0{0}", A.ToString("X")) : A.ToString("X"),
            //    R.ToString("X").Length == 1 ? String.Format("0{0}", R.ToString("X")) : R.ToString("X"),
            //    G.ToString("X").Length == 1 ? String.Format("0{0}", G.ToString("X")) : G.ToString("X"),
            //    B.ToString("X").Length == 1 ? String.Format("0{0}", B.ToString("X")) : B.ToString("X"));
        }

        /// <summary>
        /// Converts sting to a <see cref="Paint"/> with support of semi-transparency
        /// </summary>
        /// <param name="name">For example: 'Known Paint', '#AARRGGBB', '#RRGGBB', 'RGBA, R,G,B,A', 'RGB, R,G,B'</param>
        public static Paint FromString(string name)
        {
            var ret = new Paint { A = 255 };
            var key = name.Replace(" ", "").ToLower();
            if (key == "transparent")
            {
                return new Paint { A = 0, R = 0, G = 0, B = 0 };
            }
            if (Paints.ByNames.ContainsKey(key))
            {
                name = Paints.ByNames[key];
            }
            if (name.IndexOf("rgba", 0) == 0)
            {
                name = name.Remove(new[] { "rgba", " ", "(", ")" });
                var parts = name.Split(',');
                ret.R = (Byte)int.Parse(parts[0]);
                ret.G = (Byte)int.Parse(parts[1]);
                ret.B = (Byte)int.Parse(parts[2]);
                ret.A = (Byte)(double.Parse(parts[3]) * 255.0);
            }
            else if (name.IndexOf("rgb", 0) == 0)
            {
                name = name.Remove(new[] { "rgb", " ", "(", ")" });
                var parts = name.Split(',');
                ret.R = (Byte)int.Parse(parts[0]);
                ret.G = (Byte)int.Parse(parts[1]);
                ret.B = (Byte)int.Parse(parts[2]);
            }
            else if (name.IndexOf("#", 0) == 0)
            {
                name = name.Remove(new[] { "#", " " });
                if (name.Length == 8)
                {
                    ret.A = (Byte)Convert.ToInt32(name.Substring(0, 2), 16);
                    ret.R = (Byte)Convert.ToInt32(name.Substring(2, 2), 16);
                    ret.G = (Byte)Convert.ToInt32(name.Substring(4, 2), 16);
                    ret.B = (Byte)Convert.ToInt32(name.Substring(6, 2), 16);
                }
                else if (name.Length == 6)
                {
                    ret.R = (Byte)Convert.ToInt32(name.Substring(0, 2), 16);
                    ret.G = (Byte)Convert.ToInt32(name.Substring(2, 2), 16);
                    ret.B = (Byte)Convert.ToInt32(name.Substring(4, 2), 16);
                }
                else if (name.Length == 3)
                {
                    ret.R = (Byte)Convert.ToInt32(name.Substring(0, 1) + name.Substring(0, 1), 16);
                    ret.G = (Byte)Convert.ToInt32(name.Substring(1, 1) + name.Substring(1, 1), 16);
                    ret.B = (Byte)Convert.ToInt32(name.Substring(2, 1) + name.Substring(2, 1), 16);
                }
            }
            else
            {
                throw new Exception("failed to convert string Paint (" + name + ") " +
                                    "to a Paint object because is not formatted correctly.");
            }
            return ret;
        }

        
        /// <summary>
        /// Converts the current <see cref="Paint"/> to a name of known Paint or returns Paint hex: '#AARRGGBB'
        /// </summary>
        public string ToName()
        {
            var name = ToHex().ToLower();
            if (name.StartsWith("#"))
            {
                var hex = name.Replace("#", "");
                if (hex.Length == 8)
                {
                    if (A == 0)
                        return "transparent";
                    if (A != 255) // check if "semi-transparent" Paint
                        return name;
                    hex = hex.Substring(2, 6);
                }
                if (hex.Length == 6)
                {
                    hex = "#" + hex;
                    if (Paints.ByHex.ContainsKey(hex))
                    {
                        name = Paints.ByHex[hex];
                        return name;
                    }
                }
            }
            return name;
        }
        #endregion

        #region Methods
        //public Paint Opacity(double value)
        //{
        //    var a = 255 * value;
            
        //}

        public bool IsSolid()
        {
            return A == 255;
        }
        public bool IsTransparent()
        {
            return A == 0;
        }
        /// <summary>
        /// Checks if the current <see cref="Paint"/> can be represented as a name of known Paint
        /// </summary>
        public bool IsKnown()
        {
            var name = ToName();
            return !name.StartsWith("#");
        }
        #endregion
    }
    /// <summary>
    /// Represents a collection of <see cref="Paint"/> objects and provided methods for converting strings to Paint objects  
    /// </summary>
    public static class Paints
    {
        static Paints()
        {
            ByHex = new Dictionary<string, string>();
            foreach (var pair in ByNames)
            {
                if (!ByHex.ContainsKey(pair.Value))
                     ByHex.Add(pair.Value, pair.Key);
            }
        }
         
        public static class Palettes
        {
            public static readonly Paint Paint000 = Paint.FromString("#FF000000");
            public static readonly Paint Paint001 = Paint.FromString("#FF333333");
            public static readonly Paint Paint002 = Paint.FromString("#FF3E3E3E");
            public static readonly Paint Paint003 = Paint.FromString("#FF505050");
            public static readonly Paint Paint004 = Paint.FromString("#FF696969");
            public static readonly Paint Paint005 = Paint.FromString("#FF818181");
            public static readonly Paint Paint006 = Paint.FromString("#FF9B9B9B");
            public static readonly Paint Paint007 = Paint.FromString("#FFB1B1B1");
            public static readonly Paint Paint008 = Paint.FromString("#FFD0D0D0");
            public static readonly Paint Paint009 = Paint.FromString("#FFE0E0E0");
            public static readonly Paint Paint010 = Paint.FromString("#FFF1F1F1");
            public static readonly Paint Paint014 = Paint.FromString("#FF3BB7EB");
            public static readonly Paint Paint015 = Paint.FromString("#FF16A9E7");
            public static readonly Paint Paint016 = Paint.FromString("#FF2788B1");
            public static readonly Paint Paint018 = Paint.FromString("#FF1A5F7C");

            public static readonly Paint Paint031 = Paint.FromString("#FF6E7E16");
            public static readonly Paint Paint032 = Paint.FromString("#FFa4ba29");
            public static readonly Paint Paint033 = Paint.FromString("#FFfdbd48");
            public static readonly Paint Paint034 = Paint.FromString("#FFF7AA1B");
            public static readonly Paint Paint035 = Paint.FromString("#FFff888b");
            public static readonly Paint Paint036 = Paint.FromString("#FFff6a6f");
            public static readonly Paint Paint037 = Paint.FromString("#FF9e73c1");
            public static readonly Paint Paint038 = Paint.FromString("#FF714199");
            public static readonly Paint Paint039 = Paint.FromString("#FFf79036");
            public static readonly Paint Paint040 = Paint.FromString("#FFBC5900");
            public static readonly Paint Paint041 = Paint.FromString("#FF69299D");
            public static readonly Paint Paint042 = Paint.FromString("#FF371356");
            public static readonly Paint Paint043 = Paint.FromString("#FF48892d");
            public static readonly Paint Paint044 = Paint.FromString("#FF285017");
            public static readonly Paint Paint045 = Paint.FromString("#FFDC8F00");
            public static readonly Paint Paint046 = Paint.FromString("#FFCA4E52");
            public static readonly Paint Paint047 = Paint.FromString("#DDFFFFFF");

            private static List<Paint> _charts;
            /// <summary> Gets or sets Charts </summary>
            public static List<Paint> Charts
            {
                get
                {
                    if (_charts != null) return _charts;
                    _charts = new List<Paint>();
                    _charts.Add(Paint.FromString("#FF329EFC"));
                    _charts.Add(Paint.FromString("#FF43433C"));
                    _charts.Add(Paint.FromString("#FF8C8C89"));
                    _charts.Add(Paint.FromString("#FF1CCF28"));
                    _charts.Add(Paint.FromString("#FFCFB11C"));
                    _charts.Add(Paint.FromString("#FFCF4F1C"));
                    _charts.Add(Paint.FromString("#FF741CCF"));
                    _charts.Add(Paint.FromString("#FFCF1CBA"));
                
                    //_charts.Add(Paint016);
                    //_charts.Add(Paint001);
                    //_charts.Add(Paint005);
                    //_charts.Add(Paint018);
                    //_charts.Add(Paint031);
                    //_charts.Add(Paint045);
                    //_charts.Add(Paint046);
                    //_charts.Add(Paint038);
                    //_charts.Add(Paint040);
                    //_charts.Add(Paint042);
                    //_charts.Add(Paint044);
                    return _charts;
                }
            }
        }

        public static readonly Dictionary<string, string> ByHex;

        #region Known Paints
        public static readonly Dictionary<string, string> ByNames = new Dictionary<string, string> {
		{ "aliceblue",              "#f0f8ff" },
		{ "antiquewhite",           "#faebd7" },
		{ "aqua",                   "#00ffff" },
		{ "aquamarine",             "#7fffd4" },
		{ "azure",                  "#f0ffff" },
		{ "beige",                  "#f5f5dc" },
		{ "bisque",                 "#ffe4c4" },
		{ "black",                  "#000000" },
		{ "blanchedalmond",         "#ffebcd" },
		{ "blue",                   "#0000ff" },
		{ "blueviolet",             "#8a2be2" },
		{ "brown",                  "#a52a2a" },
		{ "burlywood",              "#deb887" },
		{ "cadetblue",              "#5f9ea0" },
		{ "chartreuse",             "#7fff00" },
		{ "chocolate",              "#d2691e" },
		{ "coral",                  "#ff7f50" },
		{ "cornflowerblue",         "#6495ed" },
		{ "cornsilk",               "#fff8dc" },
		{ "crimson",                "#dc143c" },
		{ "cyan",                   "#00ffff" },
		{ "darkblue",               "#00008b" },
		{ "darkcyan",               "#008b8b" },
		{ "darkgoldenrod",          "#b8860b" },
		{ "darkgray",               "#a9a9a9" },
		{ "darkgreen",              "#006400" },
		{ "darkkhaki",              "#bdb76b" },
		{ "darkmagenta",            "#8b008b" },
		{ "darkolivegreen",         "#556b2f" },
		{ "darkorange",             "#ff8c00" },
		{ "darkorchid",             "#9932cc" },
		{ "darkred",                "#8b0000" },
		{ "darksalmon",             "#e9967a" },
		{ "darkseagreen",           "#8fbc8f" },
		{ "darkslateblue",          "#483d8b" },
		{ "darkslategray",          "#2f4f4f" },
		{ "darkturquoise",          "#00ced1" },
		{ "darkviolet",             "#9400d3" },
		{ "deeppink",               "#ff1493" },
		{ "deepskyblue",            "#00bfff" },
		{ "dimgray",                "#696969" },
		{ "dodgerblue",             "#1e90ff" },
		{ "feldspar",               "#d19275" },
		{ "firebrick",              "#b22222" },
		{ "floralwhite",            "#fffaf0" },
		{ "forestgreen",            "#228b22" },
		{ "fuchsia",                "#ff00ff" },
		{ "gainsboro",		        "#dcdcdc" },
		{ "ghostwhite",		        "#f8f8ff" },
		{ "gold",			        "#ffd700" },
		{ "goldenrod",			    "#daa520" },
		{ "gray",			        "#808080" },
		{ "green",			        "#008000" },
		{ "greenyellow",            "#adff2f" },
		{ "honeydew",			    "#f0fff0" },
		{ "hotpink",			    "#ff69b4" },
		{ "indianred",			    "#cd5c5c" },
		{ "indigo",			        "#4b0082" },
		{ "ivory",			        "#fffff0" },
		{ "khaki",			        "#f0e68c" },
		{ "lavender",			    "#e6e6fa" },
		{ "lavenderblush",          "#fff0f5" },
		{ "lawngreen",			    "#7cfc00" },
		{ "lemonchiffon",           "#fffacd" },
		{ "lightblue",			    "#add8e6" },
		{ "lightcoral",			    "#f08080" },
		{ "lightcyan",			    "#e0ffff" },
		{ "lightgoldenrodyellow",   "#fafad2" },
		{ "lightgray",			    "#d3d3d3" },
		{ "lightgreen",			    "#90ee90" },
		{ "lightpink",			    "#ffb6c1" },
		{ "lightsalmon",			"#ffa07a" },
		{ "lightseagreen",			"#20b2aa" },
		{ "lightskyblue",			"#87cefa" },
		{ "lightslateblue",			"#8470ff" },
		{ "lightslategray",			"#778899" },
		{ "lightsteelblue",			"#b0c4de" },
		{ "lightyellow",			"#ffffe0" },
		{ "lime",			        "#00ff00" },
		{ "limegreen",			    "#32cd32" },
		{ "linen",		        	"#faf0e6" },
		{ "magenta",		    	"#ff00ff" },
		{ "maroon",			        "#800000" },
		{ "mediumaquamarine",		"#66cdaa" },
		{ "mediumblue",			    "#0000cd" },
		{ "mediumorchid",			"#ba55d3" },
		{ "mediumpurple",			"#9370d8" },
		{ "mediumseagreen",			"#3cb371" },
		{ "mediumslateblue",		"#7b68ee" },
		{ "mediumspringgreen",		"#00fa9a" },
		{ "mediumturquoise",		"#48d1cc" },
		{ "mediumvioletred",		"#c71585" },
		{ "midnightblue",			"#191970" },
		{ "mintcream",			    "#f5fffa" },
		{ "mistyrose",			    "#ffe4e1" },
		{ "moccasin",			    "#ffe4b5" },
		{ "navajowhite",			"#ffdead" },
		{ "navy",			        "#000080" },
		{ "oldlace",			    "#fdf5e6" },
		{ "olive",			        "#808000" },
		{ "olivedrab",			    "#6b8e23" },
		{ "orange",			        "#ffa500" },
		{ "orangered",			    "#ff4500" },
		{ "orchid",			        "#da70d6" },
		{ "palegoldenrod",			"#eee8aa" },
		{ "palegreen",			    "#98fb98" },
		{ "paleturquoise",			"#afeeee" },
		{ "palevioletred",			"#d87093" },
		{ "papayawhip",			    "#ffefd5" },
		{ "peachpuff",			    "#ffdab9" },
		{ "peru",			        "#cd853f" },
		{ "pink",			        "#ffc0cb" },
		{ "plum",			        "#dda0dd" },
		{ "powderblue",			    "#b0e0e6" },
		{ "purple",			        "#800080" },
		{ "red",			        "#ff0000" },
		{ "rosybrown",			    "#bc8f8f" },
		{ "royalblue",			    "#4169e1" },
		{ "saddlebrown",			"#8b4513" },
		{ "salmon",			        "#fa8072" },
		{ "sandybrown",			    "#f4a460" },
		{ "seagreen",			    "#2e8b57" },
		{ "seashell",			    "#fff5ee" },
		{ "sienna",			        "#a0522d" },
		{ "silver",			        "#c0c0c0" },
		{ "skyblue",			    "#87ceeb" },
		{ "slateblue",			    "#6a5acd" },
		{ "slategray",			    "#708090"},
		{ "snow",			        "#fffafa"},
		{ "springgreen",			"#00ff7f"},
		{ "steelblue",			    "#4682b4"},
		{ "tan",			        "#d2b48c"},
		{ "teal",			        "#008080"},
		{ "thistle",			    "#d8bfd8"},
		{ "tomato",			        "#ff6347"},
		{ "turquoise",			    "#40e0d0"},
		{ "violet",			        "#ee82ee"},
		{ "violetred",			    "#d02090"},
		{ "wheat",			        "#f5deb3"},
		{ "white",			        "#ffffff" },
		{ "whitesmoke",			    "#f5f5f5"},
		{ "yellow",			        "#ffff00"},
		{ "yellowgreen",			"#9acd32" },
		};
        #endregion

        #region Static Properties
        public static readonly Paint AliceBlue = Paint.FromRgb(240, 248, 255);
        public static readonly Paint AntiqueWhite = Paint.FromRgb(250, 235, 215);
        public static readonly Paint Aqua = Paint.FromRgb(0, 255, 255);
        public static readonly Paint Aquamarine = Paint.FromRgb(127, 255, 212);
        public static readonly Paint Azure = Paint.FromRgb(240, 255, 255);
        public static readonly Paint Beige = Paint.FromRgb(245, 245, 220);
        public static readonly Paint Bisque = Paint.FromRgb(255, 228, 196);
        public static readonly Paint Black = Paint.FromRgb(0, 0, 0);
        public static readonly Paint BlanchedAlmond = Paint.FromRgb(255, 235, 205);
        public static readonly Paint Blue = Paint.FromRgb(0, 0, 255);
        public static readonly Paint BlueViolet = Paint.FromRgb(138, 43, 226);
        public static readonly Paint Brown = Paint.FromRgb(165, 42, 42);
        public static readonly Paint Burlywood = Paint.FromRgb(222, 184, 135);
        public static readonly Paint CadetBlue = Paint.FromRgb(95, 158, 160);
        public static readonly Paint Chartreuse = Paint.FromRgb(127, 255, 0);
        public static readonly Paint Chocolate = Paint.FromRgb(210, 105, 30);
        public static readonly Paint Coral = Paint.FromRgb(255, 127, 80);
        public static readonly Paint CornflowerBlue = Paint.FromRgb(100, 149, 237);
        public static readonly Paint Cornsilk = Paint.FromRgb(255, 248, 220);
        public static readonly Paint Crimson = Paint.FromRgb(220, 20, 60);
        public static readonly Paint Cyan = Paint.FromRgb(0, 255, 255);
        public static readonly Paint DarkBlue = Paint.FromRgb(0, 0, 139);
        public static readonly Paint DarkCyan = Paint.FromRgb(0, 139, 139);
        public static readonly Paint DarkGoldenrod = Paint.FromRgb(184, 134, 11);
        public static readonly Paint DarkGray = Paint.FromRgb(169, 169, 169);
        public static readonly Paint DarkGreen = Paint.FromRgb(0, 100, 0);
        public static readonly Paint DarkGrey = Paint.FromRgb(169, 169, 169);
        public static readonly Paint DarkKhaki = Paint.FromRgb(189, 183, 107);
        public static readonly Paint DarkMagenta = Paint.FromRgb(139, 0, 139);
        public static readonly Paint DarkOliveGreen = Paint.FromRgb(85, 107, 47);
        public static readonly Paint DarkOrange = Paint.FromRgb(255, 140, 0);
        public static readonly Paint DarkOrchid = Paint.FromRgb(153, 50, 204);
        public static readonly Paint DarkRed = Paint.FromRgb(139, 0, 0);
        public static readonly Paint DarkSalmon = Paint.FromRgb(233, 150, 122);
        public static readonly Paint DarkSeaGreen = Paint.FromRgb(143, 188, 143);
        public static readonly Paint DarkSlateBlue = Paint.FromRgb(72, 61, 139);
        public static readonly Paint DarkSlateGray = Paint.FromRgb(47, 79, 79);
        public static readonly Paint DarkSlateGrey = Paint.FromRgb(47, 79, 79);
        public static readonly Paint DarkTurquoise = Paint.FromRgb(0, 206, 209);
        public static readonly Paint DarkViolet = Paint.FromRgb(148, 0, 211);
        public static readonly Paint DeepPink = Paint.FromRgb(255, 20, 147);
        public static readonly Paint DeepSkyBlue = Paint.FromRgb(0, 191, 255);
        public static readonly Paint DimGray = Paint.FromRgb(105, 105, 105);
        public static readonly Paint DimGrey = Paint.FromRgb(105, 105, 105);
        public static readonly Paint DodgerBlue = Paint.FromRgb(30, 144, 255);
        public static readonly Paint Firebrick = Paint.FromRgb(178, 34, 34);
        public static readonly Paint FloralWhite = Paint.FromRgb(255, 250, 240);
        public static readonly Paint ForestGreen = Paint.FromRgb(34, 139, 34);
        public static readonly Paint Fuchsia = Paint.FromRgb(255, 0, 255);
        public static readonly Paint Gainsboro = Paint.FromRgb(220, 220, 220);
        public static readonly Paint GhostWhite = Paint.FromRgb(248, 248, 255);
        public static readonly Paint Gold = Paint.FromRgb(255, 215, 0);
        public static readonly Paint Goldenrod = Paint.FromRgb(218, 165, 32);
        public static readonly Paint Gray = Paint.FromRgb(128, 128, 128);
        public static readonly Paint Green = Paint.FromRgb(0, 128, 0);
        public static readonly Paint GreenYellow = Paint.FromRgb(173, 255, 47);
        public static readonly Paint Grey = Paint.FromRgb(128, 128, 128);
        public static readonly Paint Honeydew = Paint.FromRgb(240, 255, 240);
        public static readonly Paint HotPink = Paint.FromRgb(255, 105, 180);
        public static readonly Paint IndianRed = Paint.FromRgb(205, 92, 92);
        public static readonly Paint Indigo = Paint.FromRgb(75, 0, 130);
        public static readonly Paint Ivory = Paint.FromRgb(255, 255, 240);
        public static readonly Paint Khaki = Paint.FromRgb(240, 230, 140);
        public static readonly Paint Lavender = Paint.FromRgb(230, 230, 250);
        public static readonly Paint LavenderBlush = Paint.FromRgb(255, 240, 245);
        public static readonly Paint LawnGreen = Paint.FromRgb(124, 252, 0);
        public static readonly Paint LemonChiffon = Paint.FromRgb(255, 250, 205);
        public static readonly Paint LightBlue = Paint.FromRgb(173, 216, 230);
        public static readonly Paint LightCoral = Paint.FromRgb(240, 128, 128);
        public static readonly Paint LightCyan = Paint.FromRgb(224, 255, 255);
        public static readonly Paint LightGoldenrodYellow = Paint.FromRgb(250, 250, 210);
        public static readonly Paint LightGray = Paint.FromRgb(211, 211, 211);
        public static readonly Paint LightGreen = Paint.FromRgb(144, 238, 144);
        public static readonly Paint LightGrey = Paint.FromRgb(211, 211, 211);
        public static readonly Paint LightPink = Paint.FromRgb(255, 182, 193);
        public static readonly Paint LightSalmon = Paint.FromRgb(255, 160, 122);
        public static readonly Paint LightSeaGreen = Paint.FromRgb(32, 178, 170);
        public static readonly Paint LightSkyBlue = Paint.FromRgb(135, 206, 250);
        public static readonly Paint LightSlateGray = Paint.FromRgb(119, 136, 153);
        public static readonly Paint LightSlateGrey = Paint.FromRgb(119, 136, 153);
        public static readonly Paint LightSteelBlue = Paint.FromRgb(176, 196, 222);
        public static readonly Paint LightYellow = Paint.FromRgb(255, 255, 224);
        public static readonly Paint Lime = Paint.FromRgb(0, 255, 0);
        public static readonly Paint LimeGreen = Paint.FromRgb(50, 205, 50);
        public static readonly Paint Linen = Paint.FromRgb(250, 240, 230);
        public static readonly Paint Magenta = Paint.FromRgb(255, 0, 255);
        public static readonly Paint Maroon = Paint.FromRgb(128, 0, 0);
        public static readonly Paint MediumAquamarine = Paint.FromRgb(102, 205, 170);
        public static readonly Paint MediumBlue = Paint.FromRgb(0, 0, 205);
        public static readonly Paint MediumOrchid = Paint.FromRgb(186, 85, 211);
        public static readonly Paint MediumPurple = Paint.FromRgb(147, 112, 219);
        public static readonly Paint MediumSeaGreen = Paint.FromRgb(60, 179, 113);
        public static readonly Paint MediumSlateBlue = Paint.FromRgb(123, 104, 238);
        public static readonly Paint MediumSpringGreen = Paint.FromRgb(0, 250, 154);
        public static readonly Paint MediumTurquoise = Paint.FromRgb(72, 209, 204);
        public static readonly Paint MediumVioletRed = Paint.FromRgb(199, 21, 133);
        public static readonly Paint MidnightBlue = Paint.FromRgb(25, 25, 112);
        public static readonly Paint MintCream = Paint.FromRgb(245, 255, 250);
        public static readonly Paint MistyRose = Paint.FromRgb(255, 228, 225);
        public static readonly Paint Moccasin = Paint.FromRgb(255, 228, 181);
        public static readonly Paint NavajoWhite = Paint.FromRgb(255, 222, 173);
        public static readonly Paint Navy = Paint.FromRgb(0, 0, 128);
        public static readonly Paint OldLace = Paint.FromRgb(253, 245, 230);
        public static readonly Paint Olive = Paint.FromRgb(128, 128, 0);
        public static readonly Paint OliveDrab = Paint.FromRgb(107, 142, 35);
        public static readonly Paint Orange = Paint.FromRgb(255, 165, 0);
        public static readonly Paint OrangeRed = Paint.FromRgb(255, 69, 0);
        public static readonly Paint Orchid = Paint.FromRgb(218, 112, 214);
        public static readonly Paint PaleGoldenrod = Paint.FromRgb(238, 232, 170);
        public static readonly Paint PaleGreen = Paint.FromRgb(152, 251, 152);
        public static readonly Paint PaleTurquoise = Paint.FromRgb(175, 238, 238);
        public static readonly Paint PaleVioletRed = Paint.FromRgb(219, 112, 147);
        public static readonly Paint PapayaWhip = Paint.FromRgb(255, 239, 213);
        public static readonly Paint PeachPuff = Paint.FromRgb(255, 218, 185);
        public static readonly Paint Peru = Paint.FromRgb(205, 133, 63);
        public static readonly Paint Pink = Paint.FromRgb(255, 192, 203);
        public static readonly Paint Plum = Paint.FromRgb(221, 160, 221);
        public static readonly Paint PowderBlue = Paint.FromRgb(176, 224, 230);
        public static readonly Paint Purple = Paint.FromRgb(128, 0, 128);
        public static readonly Paint Red = Paint.FromRgb(255, 0, 0);
        public static readonly Paint RosyBrown = Paint.FromRgb(188, 143, 143);
        public static readonly Paint RoyalBlue = Paint.FromRgb(65, 105, 225);
        public static readonly Paint SaddleBrown = Paint.FromRgb(139, 69, 19);
        public static readonly Paint Salmon = Paint.FromRgb(250, 128, 114);
        public static readonly Paint SandyBrown = Paint.FromRgb(244, 164, 96);
        public static readonly Paint SeaGreen = Paint.FromRgb(46, 139, 87);
        public static readonly Paint SeaShell = Paint.FromRgb(255, 245, 238);
        public static readonly Paint Sienna = Paint.FromRgb(160, 82, 45);
        public static readonly Paint Silver = Paint.FromRgb(192, 192, 192);
        public static readonly Paint SkyBlue = Paint.FromRgb(135, 206, 235);
        public static readonly Paint SlateBlue = Paint.FromRgb(106, 90, 205);
        public static readonly Paint SlateGray = Paint.FromRgb(112, 128, 144);
        public static readonly Paint SlateGrey = Paint.FromRgb(112, 128, 144);
        public static readonly Paint Snow = Paint.FromRgb(255, 250, 250);
        public static readonly Paint SpringGreen = Paint.FromRgb(0, 255, 127);
        public static readonly Paint SteelBlue = Paint.FromRgb(70, 130, 180);
        public static readonly Paint Tan = Paint.FromRgb(210, 180, 140);
        public static readonly Paint Teal = Paint.FromRgb(0, 128, 128);
        public static readonly Paint Thistle = Paint.FromRgb(216, 191, 216);
        public static readonly Paint Tomato = Paint.FromRgb(255, 99, 71);
        public static readonly Paint Turquoise = Paint.FromRgb(64, 224, 208);
        public static readonly Paint Violet = Paint.FromRgb(238, 130, 238);
        public static readonly Paint Wheat = Paint.FromRgb(245, 222, 179);
        public static readonly Paint White = Paint.FromRgb(255, 255, 255);
        public static readonly Paint WhiteSmoke = Paint.FromRgb(245, 245, 245);
        public static readonly Paint Yellow = Paint.FromRgb(255, 255, 0);
        public static readonly Paint YellowGreen = Paint.FromRgb(154, 205, 50);
        public static readonly Paint Transparent = Paint.FromRgba(255, 255, 255, 0);
        #endregion

        /// <summary> Converts integer pixel (ARGB) to a <see cref="Paint"/>  </summary>
        public static Paint ToPaint(this int pixel)
        {
            return Paint.FromInt(pixel);
        }
        public static Paint ToPaint(this string name)
        {
            return Paint.FromString(name);
        }
    }

}

