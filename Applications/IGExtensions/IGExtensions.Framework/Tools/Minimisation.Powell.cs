using System;
using System.Diagnostics.CodeAnalysis;

namespace IGExtensions.Framework.Tools
{
    
    public static partial class Minimisation
    {
        public static double[] Bracket(Func<double, double> function, double a, double b)
        {
            double xa;
            double xb;
            double xc;
            double fa;
            double fb;
            double fc;

            double GLIMIT = 100.0;
            double TINY = 1.0e-20;

            xa = a;
            fa = function(xa);

            xb = b;
            fb = function(xb);

            if (fb > fa)
            {
                swap(ref xa, ref xb);
                swap(ref fa, ref fb);
            }

            xc = xb + MathTool.PHI * (xb - xa);
            fc = function(xc);

            double fu;

            while (fb > fc)
            {
                double r = (xb - xa) * (fb - fc);
                double q = (xb - xc) * (fb - fa);
                double u = xb - ((xb - xc) * q - (xb - xa) * r) / (2.0 * MathTool.CopySign(Math.Max(Math.Abs(q - r), TINY), q - r));
                double ulim = xb + GLIMIT * (xc - xb);

                if ((xb - u) * (u - xc) > 0.0)
                {
                    fu = function(u);

                    if (fu < fc)
                    {
                        xa = xb;
                        fa = fb;

                        xb = u;
                        fb = fu;

                        return new double[] { xa, fa, xb, fb, xc, fc };
                    }

                    if (fu > fb)
                    {
                        xc = u;
                        fc = fu;

                        return new double[] { xa, fa, xb, fb, xc, fc };
                    }

                    u = xc + MathTool.PHI * (xc - xb);
                    fu = function(u);
                }
                else
                {
                    if ((xc - u) * (u - ulim) > 0.0)
                    {
                        fu = function(u);

                        if (fu < fc)
                        {
                            shift(ref xb, ref xc, ref u, u + MathTool.PHI * (u - xc));
                            shift(ref fb, ref fc, ref fu, function(u));
                        }
                    }
                    else
                    {
                        if ((u - ulim) * (ulim - xc) >= 0.0)
                        {
                            u = ulim;
                            fu = function(u);
                        }
                        else
                        {
                            u = xc + MathTool.PHI * (xc - xb);
                            fu = function(u);
                        }
                    }
                }

                shift(ref xa, ref xb, ref xc, u);
                shift(ref fa, ref fb, ref fc, fu);
            }

            return new double[] { xa, fa, xb, fb, xc, fc };
        }
        public static double[] Brent(Func<double, double> function, double xa, double xb, double xc, double tolerance)
        {
            const int ITMAX = 100;
            const double CGOLD = 0.3819660;
            const double ZEPS = 0.00001;// Double.Epsilon;// *1.0e-3;

            double a = (xa < xc ? xa : xc);
            double b = (xa > xc ? xa : xc);
            double d = 0.0;
            double e = 0.0;

            double u;
            double fu;

            double v = xb;
            double fv = function(v);

            double w = v;
            double fw = fv;

            double x = v;
            double fx = fv;

            double xmin;
            double fmin;

            for (int i = 0; i < ITMAX; ++i)
            {
                double xm = 0.5 * (a + b);
                double tol1 = tolerance * Math.Abs(x) + ZEPS;
                double tol2 = 2.0 * tol1;

                if (Math.Abs(x - xm) <= (tol2 - 0.5 * (b - a)))
                {
                    xmin = x;
                    fmin = fx;

                    return new double[] { xmin, fmin };
                }

                if (Math.Abs(e) > tol1)
                {
                    double r = (x - w) * (fx - fv);
                    double q = (x - v) * (fx - fw);
                    double p = (x - v) * q - (x - w) * r;

                    q = 2.0 * (q - r);

                    if (q > 0.0)
                    {
                        p = -p;
                    }

                    q = Math.Abs(q);
                    double etemp = e;
                    e = d;

                    if (Math.Abs(p) >= Math.Abs(0.5 * q * etemp) || p <= q * (a - x) || p >= q * (b - x))
                    {
                        d = CGOLD * (e = (x >= xm ? a - x : b - x));
                    }
                    else
                    {
                        d = p / q;
                        u = x + d;

                        if (u - a < tol2 || b - u < tol2)
                        {
                            d = MathTool.CopySign(tol1, xm - x);
                        }
                    }
                }
                else
                {
                    d = CGOLD * (e = (x >= xm ? a - x : b - x));
                }

                u = (Math.Abs(d) >= tol1 ? x + d : x + MathTool.CopySign(tol1, d));
                fu = function(u);

                if (fu <= fx)
                {
                    if (u >= x)
                    {
                        a = x;
                    }
                    else
                    {
                        b = x;
                    }

                    shift(ref v, ref w, ref x, u);
                    shift(ref fv, ref fw, ref fx, fu);
                }
                else
                {
                    if (u < x)
                    {
                        a = u;
                    }
                    else
                    {
                        b = u;
                    }

                    if (fu <= fw || w == x)
                    {
                        v = w;
                        w = u;
                        fv = fw;
                        fw = fu;
                    }
                    else
                    {
                        if (fu <= fv || v == x || v == w)
                        {
                            v = u;
                            fv = fu;
                        }
                    }
                }
            }

            xmin = double.NaN;
            fmin = double.NaN;

            return null;
        }
        public static double[] Minimise(Func<double[], double> function, double[] pp, double tolerance)
        {
            int n = pp.Length;
            int ITMAX = 200;
            double TINY = 1.0e-25;

            double[,] ximat = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                ximat[i, i] = 1.0;
            }

            double[] p = new double[n];
            double[] pt = new double[n];
            double[] ptt = new double[n];

            double[] xi = new double[n];
            double fret = function(p);

            for (int i = 0; i < n; ++i)
            {
                p[i] = pp[i];
                pt[i] = p[i];
            }

            for (int i = 0; ; ++i)
            {
                double fp = fret;
                double fptt;
                int ibig = 0;
                double del = 0.0;

                for (int j = 0; j < n; ++j)
                {
                    for (int k = 0; k < n; ++k)
                    {
                        xi[k] = ximat[k, j];
                    }

                    fptt = fret;
                    fret = linmin(function, p, xi);

                    if (fptt - fret > del)
                    {
                        del = fptt - fret;
                        ibig = j + 1;
                    }
                }

                if (2.0 * (fp - fret) <= tolerance * (Math.Abs(fp) + Math.Abs(fret)) + TINY)
                {
                    return p;
                }

                if (i == ITMAX)
                {
                    return null;    //throw ("powell exceeding maximum iterations.");
                }

                for (int j = 0; j < n; j++)
                {
                    ptt[j] = 2.0 * p[j] - pt[j];
                    xi[j] = p[j] - pt[j];
                    pt[j] = p[j];
                }

                fptt = function(ptt);

                if (fptt < fp)
                {
                    double t = 2.0 * (fp - 2.0 * fret + fptt) * MathTool.Sqr(fp - fret - del) - del * MathTool.Sqr(fp - fptt);

                    if (t < 0.0)
                    {
                        fret = linmin(function, p, xi);

                        for (int j = 0; j < n; j++)
                        {
                            ximat[j, ibig - 1] = ximat[j, n - 1];
                            ximat[j, n - 1] = xi[j];
                        }
                    }
                }
            }
        }
        private static double linmin(Func<double[], double> function, double[] p, double[] xi)
        {
            double[] xt = new double[p.Length];

            Func<double, double> linearFunction = (double x) =>
                {
                    for (int j = 0; j < p.Length; j++)
                    {
                        xt[j] = p[j] + x * xi[j];
                    }

                    return function(xt);
                };

            double[] bracket = Bracket(linearFunction, 0, 1);
            double[] linearMinimum = Brent(linearFunction, bracket[0], bracket[2], bracket[4], 3.0e-4);

            for (int j = 0; j < p.Length; j++)
            {
                xi[j] *= linearMinimum[0];
                p[j] += xi[j];
            }

            return linearMinimum[1];
        }

        private static void shift(ref double a, ref double b, ref double c, double d)
        {
            a = b;
            b = c;
            c = d;
        }
        private static void swap(ref double a, ref double b)
        {
            double t = a;

            a = b;
            b = t;
        }

#if false
        #region One Dimensional Minimisation
        class Bracket
        {
            public Bracket(Func<double, double> Function, double a, double b)
            {
                double GLIMIT = 100.0;
                double TINY = 1.0e-20;

                xa = a;
                fa = Function(xa);

                xb = b;
                fb = Function(xb);

                if (fb > fa)
                {
                    swap(ref xa, ref xb);
                    swap(ref fa, ref fb);
                }

                xc = xb + MathTool.PHI * (xb - xa);
                fc = Function(xc);

                double fu;

                while (fb > fc)
                {
                    double r = (xb - xa) * (fb - fc);
                    double q = (xb - xc) * (fb - fa);
                    double u = xb - ((xb - xc) * q - (xb - xa) * r) / (2.0 * MathTool.CopySign(Math.Max(Math.Abs(q - r), TINY), q - r));
                    double ulim = xb + GLIMIT * (xc - xb);

                    if ((xb - u) * (u - xc) > 0.0)
                    {
                        fu = Function(u);

                        if (fu < fc)
                        {
                            xa = xb;
                            fa = fb;

                            xb = u;
                            fb = fu;

                            return;
                        }

                        if (fu > fb)
                        {
                            xc = u;
                            fc = fu;

                            return;
                        }

                        u = xc + MathTool.PHI * (xc - xb);
                        fu = Function(u);
                    }
                    else
                    {
                        if ((xc - u) * (u - ulim) > 0.0)
                        {
                            fu = Function(u);

                            if (fu < fc)
                            {
                                shift(ref xb, ref xc, ref u, u + MathTool.PHI * (u - xc));
                                shift(ref fb, ref fc, ref fu, Function(u));
                            }
                        }
                        else
                        {
                            if ((u - ulim) * (ulim - xc) >= 0.0)
                            {
                                u = ulim;
                                fu = Function(u);
                            }
                            else
                            {
                                u = xc + MathTool.PHI * (xc - xb);
                                fu = Function(u);
                            }
                        }
                    }

                    shift(ref xa, ref xb, ref xc, u);
                    shift(ref fa, ref fb, ref fc, fu);
                }
            }
            private static void shift(ref double a, ref double b, ref double c, double d)
            {
                a = b;
                b = c;
                c = d;
            }
            private static void swap(ref double a, ref double b)
            {
                double t = a;

                a = b;
                b = t;
            }

            #region Xa and Fa Properties
            public double Xa
            { 
                get { return xa; }
            }
            private readonly double xa;

            public double Fa
            {
                get { return fa; }
            }
            private readonly double fa;
            #endregion

            #region Xb and Fb Properties
            public double Xb
            {
                get { return xb; }
            }
            private readonly double xb;

            public double Fb
            {
                get { return fb; }
            }
            private readonly double fb;
            #endregion

            #region Xc and Fc Properties
            public double Xc
            {
                get { return xc; }
            }
            private readonly double xc;

            public double Fc
            {
                get { return fc; }
            }
            private readonly double fc;
            #endregion
        }

        class Brent
        {
            public Brent(Func<double, double> function, double xa, double xb, double xc, double tolerance)
            {
                const int ITMAX = 100;
                const double CGOLD = 0.3819660;
                const double ZEPS = 0.00001;// Double.Epsilon;// *1.0e-3;

                double a = (xa < xc ? xa : xc);
                double b = (xa > xc ? xa : xc);
                double d = 0.0;
                double e = 0.0;

                double u;
                double fu;

                double v = xb;
                double fv = function(v);

                double w = v;
                double fw = fv;

                double x = v;
                double fx = fv;

                for (int i = 0; i < ITMAX; ++i)
                {
                    double xm = 0.5 * (a + b);
                    double tol1 = tolerance * Math.Abs(x) + ZEPS;
                    double tol2 = 2.0 * tol1;

                    if (Math.Abs(x - xm) <= (tol2 - 0.5 * (b - a)))
                    {
                        xmin = x;
                        fmin = fx;

                        return;
                    }

                    if (Math.Abs(e) > tol1)
                    {
                        double r = (x - w) * (fx - fv);
                        double q = (x - v) * (fx - fw);
                        double p = (x - v) * q - (x - w) * r;

                        q = 2.0 * (q - r);

                        if (q > 0.0)
                        {
                            p = -p;
                        }

                        q = Math.Abs(q);
                        double etemp = e;
                        e = d;

                        if (Math.Abs(p) >= Math.Abs(0.5 * q * etemp) || p <= q * (a - x) || p >= q * (b - x))
                        {
                            d = CGOLD * (e = (x >= xm ? a - x : b - x));
                        }
                        else
                        {
                            d = p / q;
                            u = x + d;

                            if (u - a < tol2 || b - u < tol2)
                            {
                                d = MathTool.CopySign(tol1, xm - x);
                            }
                        }
                    }
                    else
                    {
                        d = CGOLD * (e = (x >= xm ? a - x : b - x));
                    }

                    u = (Math.Abs(d) >= tol1 ? x + d : x + MathTool.CopySign(tol1, d));
                    fu = function(u);

                    if (fu <= fx)
                    {
                        if (u >= x)
                        {
                            a = x;
                        }
                        else
                        {
                            b = x;
                        }

                        shift(ref v, ref w, ref x, u);
                        shift(ref fv, ref fw, ref fx, fu);
                    }
                    else
                    {
                        if (u < x)
                        {
                            a = u;
                        }
                        else
                        {
                            b = u;
                        }

                        if (fu <= fw || w == x)
                        {
                            v = w;
                            w = u;
                            fv = fw;
                            fw = fu;
                        }
                        else
                        {
                            if (fu <= fv || v == x || v == w)
                            {
                                v = u;
                                fv = fu;
                            }
                        }
                    }
                }

                //throw("Too many iterations in brent");
                xmin = double.NaN;
                fmin = double.NaN;
            }
            private static void shift(ref double a, ref double b, ref double c, double d)
            {
                a = b;
                b = c;
                c = d;
            }

            public double XMin
            {
                get { return xmin; }
            }
            private double xmin;

            public double FMin
            {
                get { return fmin; }
            }
            private double fmin;

        }
        #endregion
#endif
    }
}
