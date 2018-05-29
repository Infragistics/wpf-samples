using System;
using System.Diagnostics.CodeAnalysis;

namespace IGExtensions.Framework.Tools
{
    
    public static partial class MathTool
    {
        /// <summary>
        /// Find the real roots of the quadratic equation ax^2+bx+c=0
        /// </summary>
        /// <param name="a">quadratic coefficient</param>
        /// <param name="b">linear coefficient</param>
        /// <param name="c">constant coefficient</param>
        /// <returns>Two real roots or two double.NaNs if no real roots exist.</returns>
        public static double[] Solve(double a, double b, double c)
        {
            double Δ = b * b - 4.0 * a * c;
            double q = -0.5 * (b + Math.Sign(b) * Math.Sqrt(Δ));

            return new double[] { q / a, c / q };
        }

        /// <summary>
        /// Find the real roots of the cubic equation ax^3+bx^2+cx+d=0
        /// </summary>
        /// <param name="a">cubic coefficient</param>
        /// <param name="b">quadratic coefficient</param>
        /// <param name="c">linear coefficient</param>
        /// <param name="d"constant coefficient></param>
        /// <returns>Three real roots or one real root and two double.NaNs</returns>
        public static double[] Solve(double a, double b, double c, double d)
        {
            double Q = (a * a - 3 * b) / 9;
            double R = (2 * a * a * a - 9 * a * b + 27 * c) / 54;
            double[] ret = null;

            if (R * R < Q * Q * Q)
            {
                double ϑ = Math.Acos(R / Math.Sqrt(Q * Q * Q));

                ret = new double[] 
                { 
                    -2.0 * Math.Sqrt(Q) * Math.Cos(ϑ / 3.0) - a / 3.0,
                    -2.0 * Math.Sqrt(Q) * Math.Cos((ϑ + 2.0 * Math.PI) / 3.0) - a / 3.0,
                    -2.0 * Math.Sqrt(Q) * Math.Cos((ϑ - 2.0 * Math.PI) / 3.0) - a / 3.0
                };
            }
            else
            {
                double A = -Math.Sign(R) * Math.Pow(Math.Abs(R) + Math.Sqrt(R * R - Q * Q), 1.0 / 3.0);
                double B = A != 0.0 ? Q / A : 0.0;

                ret = new double[] 
                { 
                    (A + B) - a / 3.0,
                    double.NaN,
                    double.NaN
                };
            }

            return ret;
        }

        /// <summary>
        /// Solve a system of linear equations using gauss-jordan eliminiation
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>True if system has been correctly solved.</returns>
        internal static bool Solve(double[,] a, double[] b)
        {
            int n = a.GetLength(0);

            int[] indxc = new int[n];
            int[] indxr = new int[n];
            int[] ipiv = new int[n];

            for (int i = 0; i < n; i++)
            {
                ipiv[i] = 0;
            }

            for (int i = 0; i < n; i++)
            {
                double big = 0.0;
                int irow = 0;
                int icol = 0;

                for (int j = 0; j < n; j++)
                {
                    if (ipiv[j] != 1)
                    {
                        for (int k = 0; k < n; k++)
                        {
                            if (ipiv[k] == 0)
                            {
                                if (Math.Abs(a[j, k]) >= big)
                                {
                                    big = Math.Abs(a[j, k]);
                                    irow = j;
                                    icol = k;
                                }
                            }
                        }
                    }
                }

                ++(ipiv[icol]);

                if (irow != icol)
                {
                    for (int j = 0; j < n; j++)
                    {
                        double t = a[irow, j];

                        a[irow, j] = a[icol, j];
                        a[icol, j] = t;
                    }

                    {
                        double t = b[irow];

                        b[irow] = b[icol];
                        b[icol] = t;
                    }
                }

                indxr[i] = irow;
                indxc[i] = icol;

                if (a[icol, icol] == 0.0)
                {
                    return false;   // matrix is singular
                }

                double pivinv = 1.0 / a[icol, icol];
                a[icol, icol] = 1.0;

                for (int j = 0; j < n; j++)
                {
                    a[icol, j] *= pivinv;
                }

                b[icol] *= pivinv;

                for (int j = 0; j < n; j++)
                {
                    if (j != icol)
                    {
                        double dum = a[j, icol];

                        a[j, icol] = 0.0;

                        for (int l = 0; l < n; l++)
                        {
                            a[j, l] -= a[icol, l] * dum;
                        }

                        b[j] -= b[icol] * dum;
                    }
                }
            }

            for (int i = n - 1; i >= 0; i--)
            {
                if (indxr[i] != indxc[i])
                {
                    for (int j = 0; j < n; j++)
                    {
                        double t = a[j, indxr[i]];

                        a[j, indxr[i]] = a[j, indxc[i]];
                        a[j, indxc[i]] = t;
                    }
                }
            }

            return true;
        }
    }
}
