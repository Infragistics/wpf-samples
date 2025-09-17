using System;
using System.Collections.Generic;
using System.Diagnostics; 

namespace Infragistics.Framework
{
    public class Surfaces : List<Shape3D>
    {
        public Surfaces()
        {
            this.Add(new SinXPlusSinY());
            this.Add(new TanXPlusTanY());
            this.Add(new XSqPlusYSq());
            this.Add(new XCuPlusYCu());
            this.Add(new SqrtXSqPlusYSq());
            this.Add(new XSqMinusYSq());
            this.Add(new TwoXPlusThreeY());
            this.Add(new CosSqrtXSqPlusYSq());
            this.Add(new XCuMinus3XPlusYCuMinus3Y());
            this.Add(new SinXSqPlusYSqOverXSqPlusYSq());
            this.Add(new CosXPlusCosY());
            this.Add(new AbsSinXSinY());
            this.Add(new SqrtXY());
            this.Add(new XPow4Minus2YPow4Minus2YPow2Plus5());
            this.Add(new SinXPlusCosY());
            this.Add(new YPlusSinXSqPlus3Y()); 
        }
    }
    
    public class XSqPlusYSq : SurfaceBase3D
    {
        protected override double Z(double x, double y)
        {
            return Math.Pow(x, 2) + Math.Pow(y, 2);
        }
    }
    public class XCuPlusYCu : SurfaceBase3D
    {
        protected override double Z(double x, double y)
        {
            return Math.Pow(x, 3) + Math.Pow(y, 3);
        }
    }
    public class SqrtXSqPlusYSq : SurfaceBase3D
    {
        protected override double Z(double x, double y)
        {
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }
    }
    public class XSqMinusYSq : SurfaceBase3D
    {
        public XSqMinusYSq(int xCount = 10, int yCount = 10)
            : base(xCount, yCount)
        {
        }
        protected override double Z(double x, double y)
        {
            return Math.Pow(x, 2) - Math.Pow(y, 2);
        }
    }
    public class TwoXPlusThreeY : SurfaceBase3D
    {
        protected override double Z(double x, double y)
        {
            return 2 * x + 3 * y;
        }
    }
    public class CosSqrtXSqPlusYSq : SurfaceBase3D
    {
        protected override double Z(double x, double y)
        {
            return Math.Cos(Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)));
        }
    }
    public class XCuMinus3XPlusYCuMinus3Y : SurfaceBase3D
    {
        protected override double Z(double x, double y)
        {
            return Math.Pow(x, 3) - 3 * x + Math.Pow(y, 3) - 3 * y;
        }
    }

    public class YMinusXSq : SurfaceBase3D
    {
        protected override double Z(double x, double y)
        {
            return y - Math.Pow(x, 2);
        }
    }
    public class SinXSqPlusYSqOverXSqPlusYSq : SurfaceBase3D
    {
        protected override double Z(double x, double y)
        {
            return Math.Sin(
                 Math.Pow(x, 2) + Math.Pow(y, 2)) / 
                (Math.Pow(x, 2) + Math.Pow(y, 2));
        }
    }
    public class CosXPlusCosY : SurfaceBase3D
    {
        protected override double Z(double x, double y)
        {
            return Math.Cos(x) + Math.Cos(y);
        }
    }
    public class AbsSinXSinY : SurfaceBase3D
    {
        protected override double Z(double x, double y)
        {
            return Math.Abs(Math.Sin(x) * Math.Sin(y));
        }
    }
    public class SqrtXY : SurfaceBase3D
    {
        protected override double Z(double x, double y)
        {

            return Math.Sqrt(x * y);
        }
    }
    public class XPow4Minus2YPow4Minus2YPow2Plus5 : SurfaceBase3D
    {
        protected override double Z(double x, double y)
        {

            return Math.Pow(x, 4) - 2 * Math.Pow(y, 4) - 2 * Math.Pow(y, 2) + 5;
        }
    }
    public class SinXPlusCosY : SurfaceBase3D
    {
        protected override double Z(double x, double y)
        {

            return Math.Sin(x + Math.Cos(y));
        }
    }
    public class YPlusSinXSqPlus3Y : SurfaceBase3D
    {
        protected override double Z(double x, double y)
        {
            return y + Math.Sin(Math.Pow(x, 2) + 3 * y);
        }
    }
    public class SinXPlusSinY : SurfaceBase3D
    {
        protected override double Z(double x, double y)
        {
            return Math.Sin(x) + Math.Sin(y);
        }
    }

    public class TanXPlusTanY : SurfaceBase3D
    {
        protected override double Z(double x, double y)
        {
            return Math.Tan(x) + Math.Tan(y);
        }
    }


    public abstract class SurfaceBase3D : Shape3D
    {

        protected SurfaceBase3D(int xCount = 10, int yCount = 10)
            : this(-20, 20, -20, 20, xCount, yCount)
        {
        }

        protected SurfaceBase3D(double xMin, double xMax, double yMin, double yMax, int xCount = 10, int yCount = 10)
        { 
            var xStep = (xMax - xMin) / (xCount - 1);
            var yStep = (yMax - yMin) / (yCount - 1);
            // increased data points to ~40k
            for (var x = xMin; x <= xMax; x += xStep)
            {
                for (var y = yMin; y <= yMax; y += yStep)
                {
                    this.Add(new Point3D { X = x, Y = y, Z = this.Z(x, y) });
                }
            }
            Debug.WriteLine("Data points = " + this.Count);
        }
        protected abstract new double Z(double x, double y);
    }
}