using System;
using System.Windows.Media;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Media.Media3D;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace IGExtensions.Framework.Tools
{
    
    public static class ColorTool
    {
        public static Color Lightened(this Color color, double p)
        {
            double[] ahsl = color.AHSL();

            if (p < 0.0)
            {
                return FromAHSL(ahsl[0], ahsl[1], ahsl[2], ahsl[3] * (1.0 - MathTool.Clamp(-p, 0.0, 1.0)));
            }
            else
            {
                return FromAHSL(ahsl[0], ahsl[1], ahsl[2], ahsl[3] + MathTool.Clamp(p, 0.0, 1.0) * (1.0 - ahsl[3]));
            }
        }

        public static Color RandomHue(this Color color, double range=1.0)
        {
            double[] ahsv = AHSV(color);

            return FromAHSV(ahsv[0], ahsv[1]+(random.NextDouble()-0.5)*range*180.0, ahsv[2], ahsv[3]);
        }
        public static Color RandomSaturation(this Color color, double range = 1.0)
        {
            double[] ahsv = AHSV(color);

            return FromAHSV(ahsv[0], ahsv[1], ahsv[2] + (random.NextDouble() - 0.5) * range, ahsv[3]);
        }
        public static Color RandomValue(this Color color, double range = 1.0)
        {
            double[] ahsv = AHSV(color);

            return FromAHSV(ahsv[0], ahsv[1], ahsv[2], ahsv[3] + (random.NextDouble() - 0.5) * range);
        }
        public static Color RandomLightness(this Color color, double range = 1.0)
        {
            double[] ahsl = AHSL(color);

            return FromAHSL(ahsl[0], ahsl[1], ahsl[2], ahsl[3] + (random.NextDouble() - 0.5) * range);
        }

        private static Random random=new Random(1);

        #region String
        /// <summary>
        /// Gets a color matching the input string.
        /// </summary>
        /// <param name="value">The string to convert to a color.</param>
        /// <returns>A color matching the input string.</returns>
        public static Color FromString(string value)
        {
            if (value == null)
            {
                return Colors.Transparent;
            }

            string valueLower = value.ToLower(CultureInfo.InvariantCulture);

            if (standardColors.ContainsKey(valueLower))
            {
                return standardColors[valueLower];
            }

            if (value[0] == '#')
            {
                value = value.Remove(0, 1);
            }

            Color color;

            if (FromHexitsString(value, out color))
            {
                return color;
            }

            return Colors.Transparent;
        }

        static ColorTool()
        {
            standardColors.Add("aliceblue", new Color() { A = 255, R = 240, G = 248, B = 255 });
            standardColors.Add("antiquewhite", new Color() { A = 255, R = 250, G = 235, B = 215 });
            standardColors.Add("aqua", new Color() { A = 255, R = 0, G = 255, B = 255 });
            standardColors.Add("aquamarine", new Color() { A = 255, R = 127, G = 255, B = 212 });
            standardColors.Add("azure", new Color() { A = 255, R = 240, G = 255, B = 255 });
            standardColors.Add("beige", new Color() { A = 255, R = 245, G = 245, B = 220 });
            standardColors.Add("bisque", new Color() { A = 255, R = 255, G = 228, B = 196 });
            standardColors.Add("black", new Color() { A = 255, R = 0, G = 0, B = 0 });
            standardColors.Add("blanchedalmond", new Color() { A = 255, R = 255, G = 235, B = 205 });
            standardColors.Add("blue", new Color() { A = 255, R = 0, G = 0, B = 255 });
            standardColors.Add("blueviolet", new Color() { A = 255, R = 138, G = 43, B = 226 });
            standardColors.Add("brown", new Color() { A = 255, R = 165, G = 42, B = 42 });
            standardColors.Add("burgesswood", new Color() { A = 255, R = 222, G = 184, B = 135 });
            standardColors.Add("cadetblue", new Color() { A = 255, R = 95, G = 158, B = 160 });
            standardColors.Add("chartreuse", new Color() { A = 255, R = 127, G = 255, B = 0 });
            standardColors.Add("chocolate", new Color() { A = 255, R = 210, G = 105, B = 30 });
            standardColors.Add("coral", new Color() { A = 255, R = 255, G = 127, B = 80 });
            standardColors.Add("cornflowerblue", new Color() { A = 255, R = 100, G = 149, B = 237 });
            standardColors.Add("cornsilk", new Color() { A = 255, R = 255, G = 248, B = 220 });
            standardColors.Add("crimson", new Color() { A = 255, R = 220, G = 20, B = 60 });
            standardColors.Add("cyan", new Color() { A = 255, R = 0, G = 255, B = 255 });
            standardColors.Add("darkblue", new Color() { A = 255, R = 0, G = 0, B = 139 });
            standardColors.Add("darkcyan", new Color() { A = 255, R = 0, G = 139, B = 139 });
            standardColors.Add("darkgoldenrod", new Color() { A = 255, R = 184, G = 134, B = 11 });
            standardColors.Add("darkgray", new Color() { A = 255, R = 169, G = 169, B = 169 });
            standardColors.Add("darkgreen", new Color() { A = 255, R = 0, G = 100, B = 0 });
            standardColors.Add("darkkhaki", new Color() { A = 255, R = 189, G = 183, B = 107 });
            standardColors.Add("darkmagenta", new Color() { A = 255, R = 139, G = 0, B = 139 });
            standardColors.Add("darkolivegreen", new Color() { A = 255, R = 85, G = 107, B = 47 });
            standardColors.Add("darkorange", new Color() { A = 255, R = 255, G = 140, B = 0 });
            standardColors.Add("darkorchid", new Color() { A = 255, R = 153, G = 50, B = 204 });
            standardColors.Add("darkred", new Color() { A = 255, R = 139, G = 0, B = 0 });
            standardColors.Add("darksalmon", new Color() { A = 255, R = 233, G = 150, B = 122 });
            standardColors.Add("darkseagreen", new Color() { A = 255, R = 143, G = 188, B = 143 });
            standardColors.Add("darkslateblue", new Color() { A = 255, R = 72, G = 61, B = 139 });
            standardColors.Add("darkslategray", new Color() { A = 255, R = 47, G = 79, B = 79 });
            standardColors.Add("darkturquoise", new Color() { A = 255, R = 0, G = 206, B = 209 });
            standardColors.Add("darkviolet", new Color() { A = 255, R = 148, G = 0, B = 211 });
            standardColors.Add("deeppink", new Color() { A = 255, R = 255, G = 20, B = 147 });
            standardColors.Add("deepskyblue", new Color() { A = 255, R = 0, G = 191, B = 255 });
            standardColors.Add("dimgray", new Color() { A = 255, R = 105, G = 105, B = 105 });
            standardColors.Add("dodgerblue", new Color() { A = 255, R = 30, G = 144, B = 255 });
            standardColors.Add("firebrick", new Color() { A = 255, R = 178, G = 34, B = 34 });
            standardColors.Add("floralwhite", new Color() { A = 255, R = 255, G = 250, B = 240 });
            standardColors.Add("forestgreen", new Color() { A = 255, R = 34, G = 139, B = 34 });
            standardColors.Add("fuchsia", new Color() { A = 255, R = 255, G = 0, B = 255 });
            standardColors.Add("gainsboro", new Color() { A = 255, R = 220, G = 220, B = 220 });
            standardColors.Add("ghostwhite", new Color() { A = 255, R = 248, G = 248, B = 255 });
            standardColors.Add("gold", new Color() { A = 255, R = 255, G = 215, B = 0 });
            standardColors.Add("goldenrod", new Color() { A = 255, R = 218, G = 165, B = 32 });
            standardColors.Add("gray", new Color() { A = 255, R = 128, G = 128, B = 128 });
            standardColors.Add("green", new Color() { A = 255, R = 0, G = 128, B = 0 });
            standardColors.Add("greenyellow", new Color() { A = 255, R = 173, G = 255, B = 47 });
            standardColors.Add("honeydew", new Color() { A = 255, R = 240, G = 255, B = 240 });
            standardColors.Add("hotpink", new Color() { A = 255, R = 255, G = 105, B = 180 });
            standardColors.Add("indianred", new Color() { A = 255, R = 205, G = 92, B = 92 });
            standardColors.Add("indigo", new Color() { A = 255, R = 75, G = 0, B = 130 });
            standardColors.Add("ivory", new Color() { A = 255, R = 255, G = 255, B = 240 });
            standardColors.Add("khaki", new Color() { A = 255, R = 240, G = 230, B = 140 });
            standardColors.Add("lavender", new Color() { A = 255, R = 230, G = 230, B = 250 });
            standardColors.Add("lavenderblush", new Color() { A = 255, R = 255, G = 240, B = 245 });
            standardColors.Add("lawngreen", new Color() { A = 255, R = 124, G = 252, B = 0 });
            standardColors.Add("lemonchiffon", new Color() { A = 255, R = 255, G = 250, B = 205 });
            standardColors.Add("lightblue", new Color() { A = 255, R = 173, G = 216, B = 230 });
            standardColors.Add("lightcoral", new Color() { A = 255, R = 240, G = 128, B = 128 });
            standardColors.Add("lightcyan", new Color() { A = 255, R = 224, G = 255, B = 255 });
            standardColors.Add("lightgoldenrodyellow", new Color() { A = 255, R = 250, G = 250, B = 210 });
            standardColors.Add("lightgreen", new Color() { A = 255, R = 144, G = 238, B = 144 });
            standardColors.Add("lightgrey", new Color() { A = 255, R = 211, G = 211, B = 211 });
            standardColors.Add("lightpink", new Color() { A = 255, R = 255, G = 182, B = 193 });
            standardColors.Add("lightsalmon", new Color() { A = 255, R = 255, G = 160, B = 122 });
            standardColors.Add("lightseagreen", new Color() { A = 255, R = 32, G = 178, B = 170 });
            standardColors.Add("lightskyblue", new Color() { A = 255, R = 135, G = 206, B = 250 });
            standardColors.Add("lightslategray", new Color() { A = 255, R = 119, G = 136, B = 153 });
            standardColors.Add("lightsteelblue", new Color() { A = 255, R = 176, G = 196, B = 222 });
            standardColors.Add("lightyellow", new Color() { A = 255, R = 255, G = 255, B = 224 });
            standardColors.Add("lime", new Color() { A = 255, R = 0, G = 255, B = 0 });
            standardColors.Add("limegreen", new Color() { A = 255, R = 50, G = 205, B = 50 });
            standardColors.Add("linen", new Color() { A = 255, R = 250, G = 240, B = 230 });
            standardColors.Add("magenta", new Color() { A = 255, R = 255, G = 0, B = 255 });
            standardColors.Add("maroon", new Color() { A = 255, R = 128, G = 0, B = 0 });
            standardColors.Add("mediumaquamarine", new Color() { A = 255, R = 102, G = 205, B = 170 });
            standardColors.Add("mediumblue", new Color() { A = 255, R = 0, G = 0, B = 205 });
            standardColors.Add("mediumorchid", new Color() { A = 255, R = 186, G = 85, B = 211 });
            standardColors.Add("mediumpurple", new Color() { A = 255, R = 147, G = 112, B = 219 });
            standardColors.Add("mediumseagreen", new Color() { A = 255, R = 60, G = 179, B = 113 });
            standardColors.Add("mediumslateblue", new Color() { A = 255, R = 123, G = 104, B = 238 });
            standardColors.Add("mediumspringgreen", new Color() { A = 255, R = 0, G = 250, B = 154 });
            standardColors.Add("mediumturquoise", new Color() { A = 255, R = 72, G = 209, B = 204 });
            standardColors.Add("mediumvioletred", new Color() { A = 255, R = 199, G = 21, B = 133 });
            standardColors.Add("midnightblue", new Color() { A = 255, R = 25, G = 25, B = 112 });
            standardColors.Add("mintcream", new Color() { A = 255, R = 245, G = 255, B = 250 });
            standardColors.Add("mistyrose", new Color() { A = 255, R = 255, G = 228, B = 225 });
            standardColors.Add("moccasin", new Color() { A = 255, R = 255, G = 228, B = 181 });
            standardColors.Add("navajowhite", new Color() { A = 255, R = 255, G = 222, B = 173 });
            standardColors.Add("navy", new Color() { A = 255, R = 0, G = 0, B = 128 });
            standardColors.Add("oldlace", new Color() { A = 255, R = 253, G = 245, B = 230 });
            standardColors.Add("olive", new Color() { A = 255, R = 128, G = 128, B = 0 });
            standardColors.Add("olivedrab", new Color() { A = 255, R = 107, G = 142, B = 35 });
            standardColors.Add("orange", new Color() { A = 255, R = 255, G = 165, B = 0 });
            standardColors.Add("orangered", new Color() { A = 255, R = 255, G = 69, B = 0 });
            standardColors.Add("orchid", new Color() { A = 255, R = 218, G = 112, B = 214 });
            standardColors.Add("palegoldenrod", new Color() { A = 255, R = 238, G = 232, B = 170 });
            standardColors.Add("palegreen", new Color() { A = 255, R = 152, G = 251, B = 152 });
            standardColors.Add("paleturquoise", new Color() { A = 255, R = 175, G = 238, B = 238 });
            standardColors.Add("palevioletred", new Color() { A = 255, R = 219, G = 112, B = 147 });
            standardColors.Add("papayawhip", new Color() { A = 255, R = 255, G = 239, B = 213 });
            standardColors.Add("peachpuff", new Color() { A = 255, R = 255, G = 218, B = 185 });
            standardColors.Add("peru", new Color() { A = 255, R = 205, G = 133, B = 63 });
            standardColors.Add("pink", new Color() { A = 255, R = 255, G = 192, B = 203 });
            standardColors.Add("plum", new Color() { A = 255, R = 221, G = 160, B = 221 });
            standardColors.Add("powderblue", new Color() { A = 255, R = 176, G = 224, B = 230 });
            standardColors.Add("purple", new Color() { A = 255, R = 128, G = 0, B = 128 });
            standardColors.Add("purwablue", new Color() { A = 255, R = 155, G = 225, B = 255 });
            standardColors.Add("red", new Color() { A = 255, R = 255, G = 0, B = 0 });
            standardColors.Add("rosybrown", new Color() { A = 255, R = 188, G = 143, B = 143 });
            standardColors.Add("royalblue", new Color() { A = 255, R = 65, G = 105, B = 225 });
            standardColors.Add("saddlebrown", new Color() { A = 255, R = 139, G = 69, B = 19 });
            standardColors.Add("salmon", new Color() { A = 255, R = 250, G = 128, B = 114 });
            standardColors.Add("sandybrown", new Color() { A = 255, R = 244, G = 164, B = 96 });
            standardColors.Add("seagreen", new Color() { A = 255, R = 46, G = 139, B = 87 });
            standardColors.Add("seashell", new Color() { A = 255, R = 255, G = 245, B = 238 });
            standardColors.Add("sienna", new Color() { A = 255, R = 160, G = 82, B = 45 });
            standardColors.Add("silver", new Color() { A = 255, R = 192, G = 192, B = 192 });
            standardColors.Add("skyblue", new Color() { A = 255, R = 135, G = 206, B = 235 });
            standardColors.Add("slateblue", new Color() { A = 255, R = 106, G = 90, B = 205 });
            standardColors.Add("slategray", new Color() { A = 255, R = 112, G = 128, B = 144 });
            standardColors.Add("snow", new Color() { A = 255, R = 255, G = 250, B = 250 });
            standardColors.Add("springgreen", new Color() { A = 255, R = 0, G = 255, B = 127 });
            standardColors.Add("steelblue", new Color() { A = 255, R = 70, G = 130, B = 180 });
            standardColors.Add("tan", new Color() { A = 255, R = 210, G = 180, B = 140 });
            standardColors.Add("teal", new Color() { A = 255, R = 0, G = 128, B = 128 });
            standardColors.Add("thistle", new Color() { A = 255, R = 216, G = 191, B = 216 });
            standardColors.Add("tomato", new Color() { A = 255, R = 255, G = 99, B = 71 });
            standardColors.Add("turquoise", new Color() { A = 255, R = 64, G = 224, B = 208 });
            standardColors.Add("violet", new Color() { A = 255, R = 238, G = 130, B = 238 });
            standardColors.Add("wheat", new Color() { A = 255, R = 245, G = 222, B = 179 });
            standardColors.Add("white", new Color() { A = 255, R = 255, G = 255, B = 255 });
            standardColors.Add("whitesmoke", new Color() { A = 255, R = 245, G = 245, B = 245 });
            standardColors.Add("yellow", new Color() { A = 255, R = 255, G = 255, B = 0 });
            standardColors.Add("yellowgreen", new Color() { A = 255, R = 154, G = 205, B = 50 });
        }
        private static Dictionary<string, Color> standardColors = new Dictionary<string, Color>();
        private static bool IsHexits(string value)
        {
            if (value == null)
            {
                return false;
            }
            bool result = true;
            char[] chars = value.ToLower(CultureInfo.InvariantCulture).ToCharArray();
            foreach (char c in chars)
            {
                result &= char.IsDigit(c) || c == 'a' || c == 'b' || c == 'c' || c == 'd' || c == 'e' || c == 'f';

            }
            return result;
        }
        private static bool FromHexitsString(string value, out Color color)
        {
            if (IsHexits(value))
            {
                if (value.Length == 8)
                {
                    color = Color.FromArgb(
                        byte.Parse(value.Substring(0, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture),
                        byte.Parse(value.Substring(2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture),
                        byte.Parse(value.Substring(4, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture),
                        byte.Parse(value.Substring(6, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture));
                    return true;
                }
                else if (value.Length == 6)
                {

                    color = Color.FromArgb(255,
                        byte.Parse(value.Substring(0, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture),
                        byte.Parse(value.Substring(2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture),
                        byte.Parse(value.Substring(4, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture));
                    return true;
                }
            }

            color = Color.FromArgb(255, 0, 0, 0);
            return false;
        }
        #endregion

        #region Vector3D
        /// <summary>
        /// Packs a vector of max length 0.5 into a color.
        /// it would probably be a better idea to use the alpha channel as a scale and RGB as the (semi-unit) direction
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Color FromVector(Vector3D vector)
        {
            return Color.FromArgb(0xff, (byte)Math.Floor(255.0 * (vector.X + 0.5)), (byte)Math.Floor(255.0 * (vector.Y + 0.5)), (byte)Math.Floor(255.0 * (vector.Z + 0.5)));
        }
        #endregion

        #region RGB
        public static Color FromARGBInterpolation(Color min, double p, Color max)
        {
            double q = MathTool.Clamp(p, 0.0, 1.0);

#if false
            double amin = min.A/255.0;
            double rmin = amin > 0 ? min.R * amin : min.R;
            double gmin = amin > 0 ? min.G * amin : min.G;
            double bmin = amin > 0 ? min.B * amin : min.B;

            double amax = max.A/255.0;
            double rmax = amax > 0 ? max.R * amax : max.R;
            double gmax = amax > 0 ? max.G * amax : max.G;
            double bmax = amax > 0 ? max.B * amax : max.B;

            double a = amin * (1.0 - q) + amax * q;
            double r = (rmin * (1.0 - q) + rmax * q)/a;
            double g = (gmin * (1.0 - q) + gmax * q) / a;
            double b = (bmin * (1.0 - q) + bmax * q) / a;
            return Color.FromArgb((byte)(a*255.0), (byte)r, (byte)g, (byte)b);
#else
            double a = min.A * (1.0 - q) + max.A * q;
            double r = min.R * (1.0 - q) + max.R * q;
            double g = min.G * (1.0 - q) + max.G * q;
            double b = min.B * (1.0 - q) + max.B * q;

            return Color.FromArgb((byte)a, (byte)r, (byte)g, (byte)b);
#endif
        }
        #endregion

        #region AHSV
        public static Color FromAHSV(double a, double h, double s, double v)
        {
            double r;
            double g;
            double b;

            while (h >= 360)
            {
                h -= 360.0;
            }

            if (s == 0)
            {
                r = v;
                g = v;
                b = v;
            }
            else
            {
                h /= 60.0;

                double i = Math.Floor(h);
                double f = h - i;
                double p = v * (1 - s);
                double q = v * (1 - s * f);
                double t = v * (1 - s * (1 - f));

                switch ((int)i)
                {
                    case 0: r = v; g = t; b = p; break;
                    case 1: r = q; g = v; b = p; break;
                    case 2: r = p; g = v; b = t; break;
                    case 3: r = p; g = q; b = v; break;
                    case 4: r = t; g = p; b = v; break;
                    default: r = v; g = p; b = q; break;
                }
            }

            return Color.FromArgb((byte)(a * 255.0), (byte)(r * 255.0), (byte)(g * 255.0), (byte)(b * 255.0));
        }
        public static double[] AHSV(Color color)
        {
            double a = color.A / 255.0;
            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;

            double min = Math.Min(r, Math.Min(g, b));
            double max = Math.Max(r, Math.Max(g, b));
            double delta = max - min;

            double[] ahsv = new double[4];

            ahsv[0] = a;
            ahsv[3] = max;

            if (delta == 0)
            {
                ahsv[1] = -1;
                ahsv[2] = 0;
            }
            else
            {
                ahsv[1] = H(max, delta, r, g, b);
                ahsv[2] = delta / max;
            }

            return ahsv;
        }

        public static double[] FromAHSVInterpolation(double[] minimum, double p, double[] maximum)
        {
            double[] b = minimum;
            double[] e = maximum;

            double b1 = b[1] >= 0 ? b[1] : e[1];
            double e1 = e[1] >= 0 ? e[1] : b[1];

            if (b1 >= 0 && e1 >= 0 && System.Math.Abs(e1 - b1) > 180)
            {
                if (e1 > b1)
                {
                    b1 += 360;
                }
                else
                {
                    e1 += 360;
                }
            }

            p = MathTool.Clamp(p, 0.0, 1.0);

            double[] ahsv =
            {
                b[0]*(1.0-p)+e[0]*p,
                b1*(1.0-p) + e1*p,
                b[2]*(1.0-p) + e[2]*p,
                b[3]*(1.0-p) + e[3]*p
            };

            return ahsv;
        }
        public static Color FromAHSVInterpolation(Color min, double p, Color max)
        {
            double[] ahsv=FromAHSVInterpolation(AHSV(min), p, AHSV(max));

            return FromAHSV(ahsv[0], ahsv[1], ahsv[2], ahsv[3]);
        }
        #endregion

        #region AHSL
        public static double[] AHSL(this Color color)
        {
            double[] ahsl = new double[4];

            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;

            double min = Math.Min(Math.Min(r, g), b);
            double max = Math.Max(Math.Max(r, g), b);
            double delta = max - min;

            ahsl[0] = color.A / 255.0;
            ahsl[3] = (max + min) / 2.0;

            if (delta == 0)
            {
                ahsl[1] = -1;
                ahsl[2] = 0;
            }
            else
            {
                ahsl[1] = H(max, delta, r, g, b);
                ahsl[2] = ahsl[3] < 0.5 ? delta / (max + min) : delta / (2 - max - min);
            }

            return ahsl;
        }

        /// <summary>
        /// Gets a color from the specified ahsl components 
        /// </summary>
        public static Color FromAHSL(double alpha, double hue, double saturation, double lightness)
        {
            double r;
            double g;
            double b;

            if (saturation == 0)                       // HSL values = From 0 to 1
            {
                r = lightness;
                g = lightness;
                b = lightness;
            }
            else
            {
                double q = lightness < 0.5 ? lightness * (1 + saturation) : lightness + saturation - (lightness * saturation);
                double p = 2 * lightness - q;
                double hk = hue / 360.0;

                r = C(p, q, hk + 1.0 / 3.0);
                g = C(p, q, hk);
                b = C(p, q, hk - 1.0 / 3.0);
            }

            return Color.FromArgb((byte)(alpha * 255.0), (byte)(r * 255.0), (byte)(g * 255.0), (byte)(b * 255.0));
        }
        private static double C(double p, double q, double t)
        {
            t = t < 0 ? t + 1.0 : t > 1 ? t - 1.0 : t;

            if (t < 1.0 / 6.0)
            {
                return p + ((q - p) * 6.0 * t);
            }

            if (t < 1.0 / 2.0)
            {
                return q;
            }

            if (t < 2.0 / 3.0)
            {
                return p + ((q - p) * 6 * (2.0 / 3.0 - t));
            }

            return p;
        }
        #endregion

        private static double H(double max, double delta, double r, double g, double b)
        {
            double h = r == max ? (g - b) / delta :
                        g == max ? 2 + (b - r) / delta :
                        4 + (r - g) / delta;

            h *= 60.0;

            if (h < 0.0)
            {
                h += 360.0;
            }

            return h;
        }
    }
}
