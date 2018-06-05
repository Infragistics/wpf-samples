using System;

namespace IGExtensions.Framework.Tools
{
    public static partial class MathTool
    {
        /// <summary>
        /// Evaluates the error function.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double erf(double x)
        {
            return x >= 0.0 ? 1.0 - erfccheb(x) : erfccheb(-x) - 1.0;
        }

        /// <summary>
        /// Evaluates the complementary error function.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double erfc(double x)
        {
            return x >= 0.0 ? erfccheb(x) : 2.0 - erfccheb(-x);
        }

        /// <summary>
        /// Evaluates the inverse error function.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static double inverf(double p)
        {
            return inverfc(1.0 - p);
        }

        /// <summary>
        /// Evaluates the inverse complementary error function.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static double inverfc(double p)
        {
            if (p >= 2.0)
            {
                return -100.0;
            }

            if (p <= 0.0)
            {
                return 100.0;
            }

            double pp = (p < 1.0) ? p : 2.0 - p;
            double t = Math.Sqrt(-2.0 * Math.Log(pp / 2.0));
            double x = -0.70711 * ((2.30753 + t * 0.27061) / (1.0 + t * (0.99229 + t * 0.04481)) - t);

            for (int j = 0; j < 2; j++)
            {
                double err = erfc(x) - pp;

                x += err / (1.12837916709551257 * Math.Exp(-MathTool.Sqr(x)) - x * err);
            }

            return p < 1.0 ? x : -x;
        }

        private static double erfccheb(double z)
        {
            if (z < 0.0)
            {
                throw new ArgumentException("erfccheb requires nonnegative argument");
            }

            double t = 2.0 / (2.0 + z);
            double ty = 4.0 * t - 2.0;
            double d = 0.0;
            double dd = 0.0;

            for (int j = erf_cof.Length - 1; j > 0; j--)
            {
                double tmp = d;
                d = ty * d - dd + erf_cof[j];
                dd = tmp;
            }

            return t * Math.Exp(-z * z + 0.5 * (erf_cof[0] + ty * d) - dd);
        }
        private static double erfcc(double x)
        {
            double z = Math.Abs(x), ans;
            double t = 2.0 / (2.0 + z);

            ans = t * Math.Exp(-z * z - 1.26551223 + t * (1.00002368 + t * (0.37409196 + t * (0.09678418 +
                t * (-0.18628806 + t * (0.27886807 + t * (-1.13520398 + t * (1.48851587 +
                t * (-0.82215223 + t * 0.17087277)))))))));
            return x >= 0.0 ? ans : 2.0 - ans;
        }
        private static double[] erf_cof =
        {
            -1.3026537197817094, 6.4196979235649026e-1,
	        1.9476473204185836e-2,-9.561514786808631e-3,-9.46595344482036e-4,
	        3.66839497852761e-4,4.2523324806907e-5,-2.0278578112534e-5,
	        -1.624290004647e-6,1.303655835580e-6,1.5626441722e-8,-8.5238095915e-8,
	        6.529054439e-9,5.059343495e-9,-9.91364156e-10,-2.27365122e-10,
	        9.6467911e-11, 2.394038e-12,-6.886027e-12,8.94487e-13, 3.13092e-13,
	        -1.12708e-13,3.81e-16,7.106e-15,-1.523e-15,-9.4e-17,1.21e-16,-2.8e-17
        };
    }
}
