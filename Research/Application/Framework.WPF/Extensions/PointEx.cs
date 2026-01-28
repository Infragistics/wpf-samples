using System.Windows; 

namespace System
{
    public static class PointEx
    {
        /// <summary>
        /// Tolerance for comparing points
        /// </summary>
        /// 
        public static double Tolerance = 0.0001;
        /// <summary>
        /// Checks is the current point between points A and B (or in a rectangle created from A and B)
        /// </summary>
        public static bool IsBetween(this Point p, Point a, Point b)
        {
            if (!p.IsPlottable()) return false;
            if (!a.IsPlottable()) return false;
            if (!b.IsPlottable()) return false;

            if (a.X <= p.X && p.X >= b.X &&
                a.Y <= p.Y && p.Y >= b.Y)
                return true;

            if (b.X <= p.X && p.X >= a.X &&
                b.Y <= p.Y && p.Y >= a.Y)
                return true;

            return false;
        }

        public static bool IsBetween(this double p, double a, double b)
        {
            return (a <= p && p <= b) || 
                   (b <= p && p <= a);
        }

        /// <summary>
        /// Checks is the current point is plottable (not Nan and not Infinity)
        /// </summary>
        public static bool IsPlottable(this Point p)
        {
            return !double.IsNaN(p.X) && !double.IsInfinity(p.X) 
                && !double.IsNaN(p.Y) && !double.IsInfinity(p.Y);
        }


        /// <summary>
        /// Checks is the current point on a line passing between points A and B
        /// </summary>
        public static bool IsOnLine(this Point p, Point a, Point b)
        {
            if (!p.IsPlottable()) return false;
         
            if (a == p) return true;
            if (b == p) return true;

            var productA = (b.X - a.X) * (p.Y - a.Y);
            var productB = (p.X - a.X) * (b.Y - a.Y);

            return System.Math.Abs(productA - productB) < Tolerance;

            //var crossProduct = (p.Y - a.Y) * (b.X - a.X) - (p.X - a.X) * (b.Y - a.Y);
            //if (System.Math.Abs(crossProduct) > 0.0001) return false;

            //return true;
        }

        /// <summary>
        /// Checks is the current point is on a line segment between points A and B
        /// </summary>
        public static bool IsOnSegment(this Point p, Point a, Point b)
        {
            var isOnLine = p.IsOnLine(a, b);
            var isSame = System.Math.Abs(a.X - b.X) > Tolerance;
            var isBetween = isSame ? p.X.IsBetween(a.X, b.X) : p.Y.IsBetween(a.Y, b.Y);

            return isOnLine && isBetween;
        
            //if (a == p) return true;
            //if (b == p) return true;

            //var crossProduct = (p.Y - a.Y) * (b.X - a.X) - (p.X - a.X) * (b.Y - a.Y);
            //if (System.Math.Abs(crossProduct) > 0.0001) return false;

            //var dotProduct = (p.X - a.X) * (b.X - a.X) + (p.Y - a.Y) * (b.Y - a.Y);
            //if (dotProduct < 0) return false;

            //var squaredlengthba = (b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y);
            //if (dotProduct > squaredlengthba) return false;

            //return true;
        }

        /// <summary>
        /// Gets intersection from the current point to a line passing between points A and B
        /// </summary>
        public static Point GetIntersectionWith(this Point p, Point a, Point b)
        {
            if (a == b) return a;

            var ap = new Point(p.X - a.X, p.Y - a.Y); // vector A -> P
            var ab = new Point(b.X - a.X, b.Y - a.Y); // vector A -> B

            var squareAB = (ab.X * ab.X) + (ab.Y * ab.Y);

            // The dot product of A -> P and A -> B vectors
            var product = (ap.X * ab.X) + (ap.Y * ab.Y);
            // The normalized "distance" from a to closest point
            var distance = product / squareAB;
            // add the distance to A, moving towards B
            var x = a.X + ab.X * distance;
            var y = a.Y + ab.Y * distance;
            return new Point(x, y);
        }

    }
} 