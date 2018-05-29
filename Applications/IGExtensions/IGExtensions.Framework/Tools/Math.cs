using System;
using System.Diagnostics.CodeAnalysis;

namespace IGExtensions.Framework.Tools
{
    /// <summary>
    /// Represents a tool with mathematical calculations and operations
    /// </summary>
    public static partial class MathTool
    {
        public static readonly double PHI = (1.0 + Math.Sqrt(5.0)) / 2.0;
        public static readonly double SQRT2 = Math.Sqrt(2.0);

        public static double Radians(double degrees)
        {
            return Math.PI*degrees / 180.0;
        }

        public static double CopySign(double a, double b)
        {
            return Math.Sign(b) >= 0 ? Math.Abs(a) : -Math.Abs(a);
        }

        public static double Sqr(double a)
        {
            return a * a;
        }
        public static double Ramp(double a)
        {
            return a - Math.Floor(a);
        }

        public static double Frac(double a)
        {
            return a - Math.Floor(a);
        }
        public static int GCD(int a, int b)
        {
            while(b!=0)
            {
                int t=b;

                b=a-b*(a/b);
                a=t;
            }
            return a;
        }
        public static int LCM(int a, int b)
        {
            return (a * b) / GCD(a, b);
        }
        public static double Hypot2(double a, double b)
        {
            return a * a + b * b;
        }
        public static double Hypot2(params double[] a)
        {
            double m = 0.0;

            foreach (double d in a)
            {
                m += d * d;
            }

            return m;
        }
        public static double Hypot(double a, double b)
        {
            return Math.Sqrt(a * a + b * b);
        }
        public static double Hypot(params double[] a)
        {
            return Math.Sqrt(Hypot2(a));
        }
        public static double Clamp(double a, double min, double max)
        {
            return Math.Max(Math.Min(a, max), min);
        }
        public static double Step(double a, double step)
        {
            return a < step ? 0.0 : 1.0;
        }
        public static double SmoothStep(double a, double stepMin, double stepMax)
        {
            if (a <= stepMin)
            {
                return 0.0;
            }

            if (a >= stepMax)
            {
                return 1.0;
            }

            a = (a - stepMin) / (stepMax - stepMin)-0.5;

            return Math.Sin(Math.PI * a);
        }
        public static double Logistic(double a, double b)
        {
            return 1.0 / (1.0 + Math.Exp(-2.0 * b * a));
        }
        public static double Min(params double[] a)
        {
            double min = a[0];

            for (int i = 1; i < a.Length; ++i)
            {
                min = Math.Min(min, a[i]);
            }

            return min;
        }
        public static int Min(params int[] a)
        {
            int min = a[0];

            for (int i = 1; i < a.Length; ++i)
            {
                min = Math.Min(min, a[i]);
            }

            return min;
        }
        public static double Max(params double[] a)
        {
            double max = a[0];

            for (int i = 1; i < a.Length; ++i)
            {
                max = Math.Max(max, a[i]);
            }

            return max;
        }
        public static int Max(params int[] a)
        {
            int max = a[0];

            for (int i = 1; i < a.Length; ++i)
            {
                max = Math.Max(max, a[i]);
            }

            return max;
        }
        #region Nice Rounding
        /// <summary>
        /// Returns a nicely rounded value less than or equal to the specified value
        /// </summary>
        /// <param name="value">Value to round.</param>
        /// <returns></returns>
        public static double NiceFloor(double value)
        {
            if (value == 0.0) { return 0.0; }
            if (value < 0.0) { return -NiceCeiling(-value); }

            int expv = (int)Math.Floor(Math.Log10(value));
            double f = value / Expt(10.0, expv);
            double nf = f < 2.0 ? 1.0 : (f < 5.0 ? 2.0 : (f < 10.0 ? 5.0 : 10.0));
            return nf * Expt(10.0, expv);
        }

        /// <summary>
        /// Rounds a decimal value to the nearest nice number.
        /// </summary>
        /// <param name="value">Value to round.</param>
        /// <returns></returns>
        public static double NiceRound(double value)
        {
            if (value == 0.0) { return 0.0; }
            if (value < 0.0) { return -NiceRound(-value); }

            var expv = (int)Math.Floor(Math.Log10(value));
            double f = value / Expt(10.0, expv);
            double nf = f < 1.0f ? 1.0 : (f < 3.0 ? 2.0 : (f < 7.0 ? 5.0 : 10.0));

            return nf * Expt(10.0, expv);
        }

        /// <summary>
        /// Returns a nicely rounded value greater than or equal to the specified value
        /// </summary>
        /// <param name="value">Value to round.</param>
        /// <returns></returns>
        public static double NiceCeiling(double value)
        {
            if (value == 0.0) { return 0.0; }
            if (value < 0.0) { return -NiceFloor(-value); }

            var expv = (int)Math.Floor(Math.Log10(value));
            double f = value / Expt(10.0, expv);
            double nf = f <= 1.0 ? 1.0 : (f <= 2.0 ? 2.0 : (f <= 5.0 ? 5.0 : 10.0));

            return nf * Expt(10.0, expv);
        }
        private static double Expt(double a, int n)
        {
            double x = 1.0;

            for (; n > 0; --n)
            {
                x *= a;
            }
            for (; n < 0; ++n)
            {
                x /= a;
            }
            return x;
        }
        #endregion
    }
}
