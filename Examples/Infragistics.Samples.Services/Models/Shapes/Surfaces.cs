using System;
using System.Collections.Generic;
using System.Diagnostics; 

namespace Infragistics.Models
{
    public class Surfaces : List<Shape3D>
    {
        public Surfaces()
        {
            this.Add(new AbsSinXSinY());
            this.Add(new SinXPlusSinY());
            this.Add(new TanXPlusTanY());
            this.Add(new XCuPlusYCu());
            this.Add(new XSqPlusYSq());
            this.Add(new SqrtXSqPlusYSq());
            this.Add(new XSqMinusYSq());
            this.Add(new TwoXPlusThreeY());
            this.Add(new CosSqrtXSqPlusYSq());
            this.Add(new XCuMinus3XPlusYCuMinus3Y());
            this.Add(new SinXSqPlusYSqOverXSqPlusYSq());
            this.Add(new CosXPlusCosY());
            //this.Add(new SqrtXY());
            this.Add(new XPow4Minus2YPow4Minus2YPow2Plus5());
            this.Add(new YPlusSinXSqPlus3Y());
            this.Add(new SinXPlusCosY());
        }
    }

    public class XCuPlusYCu : Surface3D
    {
        protected override double Z(double x, double y)
        {
            return Math.Pow(x, 3) + Math.Pow(y, 3);
        }
    }
    public class XSqPlusYSq : Surface3D
    {
        protected override double Z(double x, double y)
        {
            return Math.Pow(x, 2) + Math.Pow(y, 2);
        }
    }
   
    public class SqrtXSqPlusYSq : Surface3D
    {
        protected override double Z(double x, double y)
        {
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }
    }
    public class XSqMinusYSq : Surface3D
    {
        protected override double Z(double x, double y)
        {
            return -Math.Pow(x, 2) - Math.Pow(y, 2);
        }
    }
    public class TwoXPlusThreeY : Surface3D
    {
        protected override double Z(double x, double y)
        {
            return 2 * x + 3 * y;
        }
    }
    public class CosSqrtXSqPlusYSq : Surface3D
    {
        protected override double Z(double x, double y)
        {
            return Math.Cos(Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)));
        }
    }
    public class XCuMinus3XPlusYCuMinus3Y : Surface3D
    {
        protected override double Z(double x, double y)
        {
            return Math.Pow(x, 3) - 3 * x + Math.Pow(y, 3) - 3 * y;
        }
    }

    public class YMinusXSq : Surface3D
    {
        protected override double Z(double x, double y)
        {
            return y - Math.Pow(x, 2);
        }
    }
    public class SinXSqPlusYSqOverXSqPlusYSq : Surface3D
    {
        protected override double Z(double x, double y)
        {
            return Math.Sin(
                 Math.Pow(x, 2) + Math.Pow(y, 2)) / 
                (Math.Pow(x, 2) + Math.Pow(y, 2));
        }
    }
    public class CosXPlusCosY : Surface3D
    {
        protected override double Z(double x, double y)
        {
            return Math.Cos(x) + Math.Cos(y);
        }
    }
    public class AbsSinXSinY : Surface3D
    {
        protected override double Z(double x, double y)
        {
            return Math.Abs(Math.Sin(x) * Math.Sin(y));
        }
    }
    public class SqrtXY : Surface3D
    {
        protected override double Z(double x, double y)
        {

            return Math.Sqrt(x * y);
        }
    }
    public class XPow4Minus2YPow4Minus2YPow2Plus5 : Surface3D
    {
        protected override double Z(double x, double y)
        {
            return Math.Pow(x, 4) - 2 * 
                   Math.Pow(y, 4) - 2 * 
                   Math.Pow(y, 2) + 5;
        }
    }
    public class SinXPlusCosY : Surface3D
    {
        protected override double Z(double x, double y)
        {

            return Math.Sin(x + Math.Cos(y));
        }
    }
    public class YPlusSinXSqPlus3Y : Surface3D
    {
        protected override double Z(double x, double y)
        {
            return y + Math.Sin(Math.Pow(x, 2) + 3 * y);
        }
    }
    public class SinXPlusSinY : Surface3D
    {
        protected override double Z(double x, double y)
        {
            return Math.Sin(x) + Math.Sin(y);
        }
    }

    public class TanXPlusTanY : Surface3D
    {
        protected override double Z(double x, double y)
        {
            return Math.Tan(x) + Math.Tan(y);
        }
    }


 }