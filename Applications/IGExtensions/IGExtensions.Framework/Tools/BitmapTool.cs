using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace IGExtensions.Framework.Tools
{
    public static class BitmapTool
    {
        public static int ReadPixel(this WriteableBitmap bitmap, Point p)
        {
            if (p.X < 0 || p.X >= bitmap.PixelWidth || p.Y < 0 || p.Y >= bitmap.PixelHeight)
            {
                return 0;
            }

            double x = Math.Floor(p.X);
            double y = Math.Floor(p.Y);
            int address = (int)y * bitmap.PixelWidth + (int)x;

#if SILVERLIGHT
            int argb00 = bitmap.Pixels[address];
            int argb10 = x < bitmap.PixelWidth - 2 ? bitmap.Pixels[address + 1] : 0;
            int argb01 = y < bitmap.PixelHeight - 2 ? bitmap.Pixels[address + bitmap.PixelWidth] : 0;
            int argb11 = x < bitmap.PixelWidth - 2 && y < bitmap.PixelHeight - 2 ? bitmap.Pixels[address + bitmap.PixelWidth + 1] : 0;
#else
            int argb00 = 0;//bitmap.BackBuffer[address];
            int argb10 = 0;//x < bitmap.PixelWidth - 2 ? bitmap.BackBuffer[address + 1] : 0;
            int argb01 = 0;//y < bitmap.PixelHeight - 2 ? bitmap.BackBuffer[address + bitmap.PixelWidth] : 0;
            int argb11 = 0;//x < bitmap.PixelWidth - 2 && y < bitmap.PixelHeight - 2 ? bitmap.BackBuffer[address + bitmap.PixelWidth + 1] : 0;
#endif

            double c1 = p.X - x;
            double c0 = 1.0 - c1;
            double d1 = p.Y - y;
            double d0 = 1.0 - d1;
            double a = ((argb00 >> 24) & 0xff) * c0 * d0 + ((argb10 >> 24) & 0xff) * c1 * d0 + ((argb01 >> 24) & 0xff) * c0 * d1 + ((argb11 >> 24) & 0xff) * c1 * d1;
            double r = ((argb00 >> 16) & 0xff) * c0 * d0 + ((argb10 >> 16) & 0xff) * c1 * d0 + ((argb01 >> 16) & 0xff) * c0 * d1 + ((argb11 >> 16) & 0xff) * c1 * d1;
            double g = ((argb00 >> 8) & 0xff) * c0 * d0 + ((argb10 >> 8) & 0xff) * c1 * d0 + ((argb01 >> 8) & 0xff) * c0 * d1 + ((argb11 >> 8) & 0xff) * c1 * d1;
            double b = ((argb00 >> 0) & 0xff) * c0 * d0 + ((argb10 >> 0) & 0xff) * c1 * d0 + ((argb01 >> 0) & 0xff) * c0 * d1 + ((argb11 >> 0) & 0xff) * c1 * d1;
            int argb = (byte)a << 24 | (byte)r << 16 | (byte)g << 8 | (byte)b;

            return argb;
        }

        public static void WritePixel(this WriteableBitmap bitmap, Point p, Color c)
        {
            double x = Math.Floor(p.X);
            double y = Math.Floor(p.Y);

            double c1 = p.X - x;
            double c0 = 1.0 - c1;

            double d1 = p.Y - y;
            double d0 = 1.0 - d1;

            int a0 = (int)y * bitmap.PixelWidth + (int)x;

            WritePixel(bitmap, a0, c, c0 * d0);
            WritePixel(bitmap, a0 + 1, c, c1 * d0);
            WritePixel(bitmap, a0 + bitmap.PixelWidth, c, c0 * d1);
            WritePixel(bitmap, a0 + bitmap.PixelWidth + 1, c, c1 * d1);
        }
        private static void WritePixel(this WriteableBitmap bitmap, int address, Color c, double a)
        {
#if SILVERLIGHT
            int argb = bitmap.Pixels[address];
#else
            int argb = 0;//bitmap.BackBuffer[address];
#endif
            int r = (argb >> 16) & 0xff;
            int g = (argb >> 8) & 0xff;
            int b = argb & 0xff;

            r = (int)(c.R * a + r * (1.0 - a));
            g = (int)(c.G * a + g * (1.0 - a));
            b = (int)(c.B * a + b * (1.0 - a));
#if SILVERLIGHT
            bitmap.Pixels[address] = (0xff << 24) | (r << 16) | (g << 8) | b;
#else
            //bitmap.BackBuffer[address]=(0xff << 24) | (r << 16) | (g << 8) | b;;
#endif
        }

        #region Triangle 2D texture
        public static void Render(this WriteableBitmap bitmap,
                                        Point p0, Point c0,
                                        Point p1, Point c1,
                                        Point p2, Point c2,
                                        Func<Point, int> C)
        {
            int width = bitmap.PixelWidth;
            int height = bitmap.PixelHeight;

            if (Math.Min(p0.X, Math.Min(p1.X, p2.X)) > width || Math.Max(p0.X, Math.Max(p1.X, p2.X)) < 0)
            {
                return; // entire triangle is culled by left or right edge
            }

            if (Math.Min(p0.Y, Math.Min(p1.Y, p2.Y)) > height || Math.Max(p0.Y, Math.Max(p1.Y, p2.Y)) < 0)
            {
                return; // entire triangle is culled by top or bottom edge
            }

            #region Sort vertices by increasing y
            if (p0.Y > p1.Y)
            {
                Point tp = p0; p0 = p1; p1 = tp;
                Point tc = c0; c0 = c1; c1 = tc;
            }

            if (p1.Y > p2.Y)
            {
                Point tp = p1; p1 = p2; p2 = tp;
                Point tc = c1; c1 = c2; c2 = tc;
            }

            if (p0.Y > p1.Y)
            {
                Point tp = p0; p0 = p1; p1 = tp;
                Point tc = c0; c0 = c1; c1 = tc;
            }
            #endregion

            #region Scan convert subtriangles
            int[] X = new int[3] { (int)Math.Floor(p0.X), (int)Math.Floor(p1.X), (int)Math.Floor(p2.X) };
            int[] Y = new int[3] { (int)Math.Floor(p0.Y), (int)Math.Floor(p1.Y), (int)Math.Floor(p2.Y) };

            int Y0 = Math.Max(Y[0], 0);
            int Y1 = Math.Min(Y[1], bitmap.PixelHeight - 1);
            int Y2 = Math.Min(Y[2], bitmap.PixelHeight - 1);

            if (Y2 > Y0)
            {
                double m02 = (double)(X[2] - X[0]) / (double)(Y[2] - Y[0]);
                Point c02 = new Point((c2.X - c0.X) / (double)(Y[2] - Y[0]), (c2.Y - c0.Y) / (double)(Y[2] - Y[0]));

                if (Y1 > Y0)
                {
                    double m01 = (double)(X[1] - X[0]) / (double)(Y[1] - Y[0]);
                    Point c01 = new Point((c1.X - c0.X) / (double)(Y[1] - Y[0]), (c1.Y - c0.Y) / (double)(Y[1] - Y[0]));

                    for (int y = Y0; y < Y1; ++y)
                    {
                        int xa = (int)Math.Floor(X[0] + (y - Y[0]) * m02);
                        Point ca = new Point(c0.X + (y - Y[0]) * c02.X, c0.Y + (y - Y[0]) * c02.Y);

                        int xb = (int)Math.Floor(X[0] + (y - Y[0]) * m01);
                        Point cb = new Point(c0.X + (y - Y[0]) * c01.X, c0.Y + (y - Y[0]) * c01.Y);

                        if (y > 0)
                        {
                            ScanLine(bitmap, xa, ca, xb, cb, y, C);
                        }
                    }
                }

                if (Y2 > Y1)
                {
                    double m12 = (double)(X[2] - X[1]) / (double)(Y[2] - Y[1]);
                    Point c12 = new Point((double)(c2.X - c1.X) / (double)(Y[2] - Y[1]), (double)(c2.Y - c1.Y) / (double)(Y[2] - Y[1]));

                    for (int y = Y1; y < Y2; ++y)
                    {
                        int xa = (int)Math.Floor(X[0] + (y - Y[0]) * m02);
                        Point ca = new Point(c0.X + (y - Y[0]) * c02.X, c0.Y + (y - Y[0]) * c02.Y);

                        int xb = (int)Math.Floor(X[1] + (y - Y[1]) * m12);
                        Point cb = new Point(c1.X + (y - Y[1]) * c12.X, c1.Y + (y - Y[1]) * c12.Y);

                        if (y > 0)
                        {
                            ScanLine(bitmap, xa, ca, xb, cb, y, C);
                        }
                    }
                }
            }
            #endregion
        }

        private static void ScanLine(WriteableBitmap bitmap,
                                int x0, Point c0,
                                int x1, Point c1,
                                int y, Func<Point, int> C)
        {
            if (x0 > x1)
            {
                int i = x0; x0 = x1; x1 = i;
                Point d= c0; c0 = c1; c1 = d;
            }

            int width = bitmap.PixelWidth;
            int i0 = Math.Max(-x0, 0);
            int i1 = Math.Min(width - 1, x1) - x0;

            if (i1 > i0)
            {
                int p = y * width + x0 + i0;
                Point mc = new Point((c1.X - c0.X) / (double)(x1 - x0), (c1.Y - c0.Y) / (double)(x1 - x0));

                for (int i = i0; i <= i1; ++i)
                {
                    int c = C(new Point(c0.X + i * mc.X, c0.Y + i * mc.Y));

#if SILVERLIGHT
                    bitmap.Pixels[p] = c;
#else
                    //bitmap.BackBuffer[p]=c;
#endif
                    ++p;
                }
            }
        }

        #endregion

        #region Triangle 1D texture
        public static void Render(this WriteableBitmap bitmap,
                                    Point p0, double c0,
                                    Point p1, double c1,
                                    Point p2, double c2,
                                    Func<double, int> C)
        {
            int width = bitmap.PixelWidth;
            int height = bitmap.PixelHeight;

            if (Math.Min(p0.X, Math.Min(p1.X, p2.X)) > width || Math.Max(p0.X, Math.Max(p1.X, p2.X)) < 0)
            {
                return; // entire triangle is culled by left or right edge
            }

            if (Math.Min(p0.Y, Math.Min(p1.Y, p2.Y)) > height || Math.Max(p0.Y, Math.Max(p1.Y, p2.Y)) < 0)
            {
                return; // entire triangle is culled by top or bottom edge
            }

            #region Sort vertices by increasing y
            if (p0.Y > p1.Y)
            {
                Point tp = p0; p0 = p1; p1 = tp;
                double tc = c0; c0 = c1; c1 = tc;
            }

            if (p1.Y > p2.Y)
            {
                Point tp = p1; p1 = p2; p2 = tp;
                double tc = c1; c1 = c2; c2 = tc;
            }

            if (p0.Y > p1.Y)
            {
                Point tp = p0; p0 = p1; p1 = tp;
                double tc = c0; c0 = c1; c1 = tc;
            }
            #endregion

            #region Scan convert subtriangles
            int[] X = new int[3] { (int)Math.Floor(p0.X), (int)Math.Floor(p1.X), (int)Math.Floor(p2.X) };
            int[] Y = new int[3] { (int)Math.Floor(p0.Y), (int)Math.Floor(p1.Y), (int)Math.Floor(p2.Y) };

            int Y0 = Math.Max(Y[0], 0);
            int Y1 = Math.Min(Y[1], bitmap.PixelHeight - 1);
            int Y2 = Math.Min(Y[2], bitmap.PixelHeight - 1);

            if (Y2 > Y0)
            {
                double m02 = (double)(X[2] - X[0]) / (double)(Y[2] - Y[0]);
                double c02 = (c2 - c0) / (double)(Y[2] - Y[0]);

                if (Y1 > Y0)
                {
                    double m01 = (double)(X[1] - X[0]) / (double)(Y[1] - Y[0]);
                    double c01= (c1-c0)/(double)(Y[1] - Y[0]);

                    for (int y = Y0; y < Y1; ++y)
                    {
                        int xa = (int)Math.Floor(X[0] + (y - Y[0]) * m02);
                        double ca = c0 + (y - Y[0]) * c02;

                        int xb = (int)Math.Floor(X[0] + (y - Y[0]) * m01);
                        double cb = c0 + (y - Y[0]) * c01;

                        if (y > 0)
                        {
                            ScanLine(bitmap, xa, ca, xb, cb, y, C);
                        }
                    }
                }

                if (Y2 > Y1)
                {
                    double m12 = (double)(X[2] - X[1]) / (double)(Y[2] - Y[1]);
                    double c12 = (double)(c2 - c1) / (double)(Y[2] - Y[1]);

                    for (int y = Y1; y < Y2; ++y)
                    {
                        int xa = (int)Math.Floor(X[0] + (y - Y[0]) * m02);
                        double ca = c0 + (y - Y[0]) * c02;

                        int xb = (int)Math.Floor(X[1] + (y - Y[1]) * m12);
                        double cb = c1 + (y - Y[1]) * c12;

                        if (y > 0)
                        {
                            ScanLine(bitmap, xa, ca, xb, cb, y, C);
                        }
                    }
                }
            }
            #endregion
        }

        private static void ScanLine(WriteableBitmap bitmap,
                                        int x0, double c0, 
                                        int x1, double c1,
                                        int y, Func<double, int> C)
        {
            if (x0 > x1)
            {
                int i;
                double d;

                i = x0; x0 = x1; x1 = i;
                d = c0; c0 = c1; c1 = d;
            }

            int width = bitmap.PixelWidth;
            int i0 = Math.Max(-x0, 0);
            int i1 = Math.Min(width - 1, x1) - x0;

            if (i1 > i0)
            {
                int p = y * width + x0 + i0;
                double mc = (double)(c1 - c0) / (double)(x1 - x0);

                for (int i = i0; i <= i1; ++i)
                {
                    int c = C(c0 + i * mc);

#if SILVERLIGHT
                    bitmap.Pixels[p] = c;
#else
                    //bitmap.BackBuffer[p]=c;
#endif
                    ++p;
                }
            }
        }
        #endregion

        #region Triangle Color
        public static void Render(this WriteableBitmap bitmap,
                                    Point p0, Color c0,
                                    Point p1, Color c1,
                                    Point p2, Color c2)
        {
            int width = bitmap.PixelWidth;
            int height = bitmap.PixelHeight;

            if (Math.Min(p0.X, Math.Min(p1.X, p2.X)) > width || Math.Max(p0.X, Math.Max(p1.X, p2.X)) < 0)
            {
                return; // entire triangle is culled by left or right edge
            }

            if (Math.Min(p0.Y, Math.Min(p1.Y, p2.Y)) > height || Math.Max(p0.Y, Math.Max(p1.Y, p2.Y)) < 0)
            {
                return; // entire triangle is culled by top or bottom edge
            }

            #region Sort vertices by increasing y
            if (p0.Y > p1.Y)
            {
                Point tp = p0; p0 = p1; p1 = tp;
                Color tc = c0; c0 = c1; c1 = tc;
            }

            if (p1.Y > p2.Y)
            {
                Point tp = p1; p1 = p2; p2 = tp;
                Color tc = c1; c1 = c2; c2 = tc;
            }

            if (p0.Y > p1.Y)
            {
                Point tp = p0; p0 = p1; p1 = tp;
                Color tc = c0; c0 = c1; c1 = tc;
            }

            #endregion

            #region Scan convert subtriangles
            int[] X = new int[3] { (int)Math.Floor(p0.X), (int)Math.Floor(p1.X), (int)Math.Floor(p2.X) };
            int[] Y = new int[3] { (int)Math.Floor(p0.Y), (int)Math.Floor(p1.Y), (int)Math.Floor(p2.Y) };
            int[] A = new int[3] { c0.A, c1.A, c2.A };
            int[] R = new int[3] { c0.R, c1.R, c2.R };
            int[] G = new int[3] { c0.G, c1.G, c2.G };
            int[] B = new int[3] { c0.B, c1.B, c2.B };

            int Y0 = Math.Max(Y[0], 0);
            int Y1 = Math.Min(Y[1], bitmap.PixelHeight - 1);
            int Y2 = Math.Min(Y[2], bitmap.PixelHeight - 1);

            if (Y2 > Y0)
            {
                double m02 = (double)(X[2] - X[0]) / (double)(Y[2] - Y[0]);
                double a02 = (double)(A[2] - A[0]) / (double)(Y[2] - Y[0]);
                double r02 = (double)(R[2] - R[0]) / (double)(Y[2] - Y[0]);
                double g02 = (double)(G[2] - G[0]) / (double)(Y[2] - Y[0]);
                double b02 = (double)(B[2] - B[0]) / (double)(Y[2] - Y[0]);

                if (Y1 > Y0)
                {
                    double m01 = (double)(X[1] - X[0]) / (double)(Y[1] - Y[0]);
                    double a01 = (double)(A[1] - A[0]) / (double)(Y[1] - Y[0]);
                    double r01 = (double)(R[1] - R[0]) / (double)(Y[1] - Y[0]);
                    double g01 = (double)(G[1] - G[0]) / (double)(Y[1] - Y[0]);
                    double b01 = (double)(B[1] - B[0]) / (double)(Y[1] - Y[0]);

                    for (int y = Y0; y < Y1; ++y)
                    {
                        int xa = (int)Math.Floor(X[0] + (y - Y[0]) * m02);
                        double aa = A[0] + (y - Y[0]) * a02;
                        double ra = R[0] + (y - Y[0]) * r02;
                        double ga = G[0] + (y - Y[0]) * g02;
                        double ba = B[0] + (y - Y[0]) * b02;

                        int xb = (int)Math.Floor(X[0] + (y - Y[0]) * m01);
                        double ab = A[0] + (y - Y[0]) * a01;
                        double rb = R[0] + (y - Y[0]) * r01;
                        double gb = G[0] + (y - Y[0]) * g01;
                        double bb = B[0] + (y - Y[0]) * b01;

                        if (y > 0)
                        {
                            ScanLine(bitmap, xa, aa, ra, ga, ba, xb, ab, rb, gb, bb, y);
                        }
                    }
                }

                if (Y2 > Y1)
                {
                    double m12 = (double)(X[2] - X[1]) / (double)(Y[2] - Y[1]);
                    double a12 = (double)(A[2] - A[1]) / (double)(Y[2] - Y[1]);
                    double r12 = (double)(R[2] - R[1]) / (double)(Y[2] - Y[1]);
                    double g12 = (double)(G[2] - G[1]) / (double)(Y[2] - Y[1]);
                    double b12 = (double)(B[2] - B[1]) / (double)(Y[2] - Y[1]);

                    for (int y = Y1; y < Y2; ++y)
                    {
                        int xa = (int)Math.Floor(X[0] + (y - Y[0]) * m02);
                        double aa = A[0] + (y - Y[0]) * a02;
                        double ra = R[0] + (y - Y[0]) * r02;
                        double ga = G[0] + (y - Y[0]) * g02;
                        double ba = B[0] + (y - Y[0]) * b02;

                        int xb = (int)Math.Floor(X[1] + (y - Y[1]) * m12);
                        double ab = A[1] + (y - Y[1]) * a12;
                        double rb = R[1] + (y - Y[1]) * r12;
                        double gb = G[1] + (y - Y[1]) * g12;
                        double bb = B[1] + (y - Y[1]) * b12;

                        if (y > 0)
                        {
                            ScanLine(bitmap, xa, aa, ra, ga, ba, xb, ab, rb, gb, bb, y);
                        }
                    }
                }
            }

            #endregion
        }

        /// <summary>
        /// Scan convert a line segment with linear RGB color interpolation.
        /// </summary>
        /// <remarks>
        /// The specified scan line is clipped to the bitmap left and right edges.
        /// </remarks>
        /// <param name="bitmap">Target bitmap</param>
        /// <param name="x0">Line start (may be greater than x1)</param>
        /// <param name="a0"></param>
        /// <param name="r0">Red component of line start color (0.0 to 255.0)</param>
        /// <param name="g0">Green component of line start color (0.0 to 255.0)</param>
        /// <param name="b0">Blue component of line start color (0.0 to 255.0)</param>
        /// <param name="x1">Line end (may be less than x)</param>
        /// <param name="a1"></param>
        /// <param name="r1">Red component of line end color (0.0 to 255.0)</param>
        /// <param name="g1">Green component of line end color (0.0 to 255.0)</param>
        /// <param name="b1">Blue component of line end color (0.0 to 255.0)</param>
        /// <param name="y">Scan line.</param>
        private static void ScanLine(WriteableBitmap bitmap,
                                        int x0, double a0, double r0, double g0, double b0,
                                        int x1, double a1, double r1, double g1, double b1,
                                        int y)
        {
            if (x0 > x1)
            {
                int i;
                double d;

                i = x0; x0 = x1; x1 = i;
                d = r0; r0 = r1; r1 = d;
                d = g0; g0 = g1; g1 = d;
                d = b0; b0 = b1; b1 = d;
            }

            int width = bitmap.PixelWidth;
            int i0 = Math.Max(-x0, 0);
            int i1 = Math.Min(width - 1, x1) - x0;

            if (i1 > i0)
            {
                int p = y * width + x0 + i0;
                double ar = (double)(a1 - a0) / (double)(x1 - x0);
                double mr = (double)(r1 - r0) / (double)(x1 - x0);
                double mg = (double)(g1 - g0) / (double)(x1 - x0);
                double mb = (double)(b1 - b0) / (double)(x1 - x0);

                for (int i = i0; i <= i1; ++i)
                {
                    int a = (int)Math.Round(a0 + i * ar);
                    int r = (int)Math.Round(r0 + i * mr);
                    int g = (int)Math.Round(g0 + i * mg);
                    int b = (int)Math.Round(b0 + i * mb);
                    int c = (a << 24) + (r << 16) + (g << 8) + b;

#if SILVERLIGHT
                    bitmap.Pixels[p] = c;
#else
                    //bitmap.BackBuffer[p]=c;
#endif
                    ++p;
                }
            }
        }

        #endregion

        public static void Clear(this WriteableBitmap bitmap)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException("bitmap");
            }

            int length = bitmap.PixelWidth * bitmap.PixelHeight;
            System.Array.Clear(bitmap.GetPixels(), 0, length);
            return;

//            for (int i = 0; i < length; ++i)
//            {
//#if SILVERLIGHT
//                bitmap.Pixels[i] = 0;
//#else
//                //bitmap.BackBuffer[i]=c;
//#endif
//            }
        }

        public static void Clear(this WriteableBitmap bitmap, Color color)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException("bitmap");
            }

            int c = (color.A << 24) + (color.R << 16) + (color.G << 8) + color.B;
            int length = bitmap.PixelWidth*bitmap.PixelHeight;

            for (int i = 0; i < length; ++i)
            {
#if SILVERLIGHT
                bitmap.Pixels[i] = (int)0x000000ff;
#else
                //bitmap.BackBuffer[i]=c;
#endif
            }
        }

        public static void Clear(this WriteableBitmap bitmap, Color color, Rect rect)
        {
            int pixelRight = Math.Max(bitmap.PixelWidth-1, 0);
            int pixelBottom = Math.Max(bitmap.PixelHeight - 1, 0);
            int left = (int)MathTool.Clamp(Math.Floor(rect.Left), 0.0, pixelRight);
            int right = (int)MathTool.Clamp(Math.Floor(rect.Right), 0.0, pixelRight);
            int top = (int)MathTool.Clamp(Math.Floor(rect.Top), 0.0, pixelBottom);
            int bottom = (int)MathTool.Clamp(Math.Floor(rect.Left), 0.0, pixelBottom);

            int stride=bitmap.PixelWidth-(left+right);
            int a=top*bitmap.PixelWidth + left;
            int c = (color.A << 24) + (color.R << 16) + (color.G << 8) + color.B;

            for (int i = top; i <= bottom; ++i)
            {
                for (int j = left; j <= right; ++j)
                {
#if SILVERLIGHT
                    bitmap.Pixels[i] = c;
#else
                    //bitmap.BackBuffer[i]=c;
#endif
                }

                a += stride;
            }
        }
    }
}
