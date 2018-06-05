namespace System.Windows
{
    public static class PointEx
    {
        /// <summary>
        /// Calculates mid point between two points
        /// </summary>
        public static Point MidPoint(this Point point, Point other)
        {
            //var distance = point.DistanceVector(other);
            var deltaX = System.Math.Abs(point.X - other.X);
            var deltaY = System.Math.Abs(point.Y - other.Y);
            var min = point.Min(other);
            return new Point(min.X + (deltaX / 2), min.Y + (deltaY / 2));
            //return new Point(min.X + (distance.X / 2), min.Y + (distance.Y / 2));
        }
        /// <summary>
        /// Calculates a vector that represents distance from the current point to the other point
        /// </summary>
        public static Vector DistanceVector(this Point point, Point other)
        {
            var deltaX = System.Math.Abs(point.X - other.X);
            var deltaY = System.Math.Abs(point.Y - other.Y);
            if (point.X > other.X) deltaX *= -1;
            if (point.Y > other.Y) deltaY *= -1;
            return new Vector(deltaX, deltaY);
        }
        /// <summary>
        /// Calculates distance from the current point to the other point
        /// </summary>
        public static double Distance(this Point point, Point other)
        {
            var distance = point.DistanceVector(other);
            return distance.Length;
        }
        /// <summary>
        /// Calculates distance squared from the current point to the other point
        /// </summary>
        public static double DistanceSquared(this Point point, Point other)
        {
            var distance = point.DistanceVector(other);
            return distance.LengthSquared;
        }
        #region Shift methods
        /// <summary>
        /// Shifts a point by the offset value 
        /// </summary>
        public static Point Shift(this Point point, double offset)
        {
            return point.Shift(offset, offset);
        }
        /// <summary>
        /// Shifts a point by the offset values 
        /// </summary>
        public static Point Shift(this Point point, double offsetX, double offsetY)
        {
            return new Point(point.X + offsetX, point.Y + offsetY);
        }
        /// <summary>
        /// Shifts a point by the offset Point 
        /// </summary>
        public static Point Shift(this Point point, Point offset)
        {
            return point.Shift(offset.X, offset.Y);
        }
        /// <summary>
        /// Shifts a point by the offset Point within bounds Rect
        /// </summary>
        public static Point Shift(this Point point, Point offset, Rect bounds)
        {
            return point.Shift(offset.X, offset.Y, bounds);
        }
        /// <summary>
        /// Shifts a point by the offset value within bounds Rect
        /// </summary>
        public static Point Shift(this Point point, double offset, Rect bounds)
        {
            return point.Shift(offset, offset, bounds);
        }
        /// <summary>
        /// Shifts a point by the offset values within bounds Rect
        /// </summary>
        public static Point Shift(this Point point, double offsetX, double offsetY, Rect bounds)
        {
            if (point.IsEmpty()) return point;
            var x = point.X + offsetX;
            var y = point.Y + offsetY;

            if (x < bounds.Left) x = bounds.Left;
            if (x > bounds.Right) x = bounds.Right;
            if (y < bounds.Top) y = bounds.Top;
            if (y > bounds.Bottom) y = bounds.Bottom;

            return new Point(x, y);
        } 
        #endregion
        public static bool IsEmpty(this Point point)
        {
            if (double.IsNaN(point.X) || double.IsNaN(point.Y)) return true;
            return false;
        }
        public static Point Clone(this Point point)
        {
            return new Point(point.X, point.Y);
        }
        /// <summary>
        /// Gets a point calculated from minimum of X and Y values for two points 
        /// </summary>
        public static Point Min(this Point point, Point other)
        {
            double xMin = System.Math.Min(point.X, other.X);
            double yMin = System.Math.Min(point.Y, other.Y);
            var location = new Point(xMin, yMin);
            return location;
        }
        /// <summary>
        /// Gets a point calculated from maximum of X and Y values for two points 
        /// </summary>
        public static Point Max(this Point point, Point other)
        {
            double xMax = System.Math.Max(point.X, other.X);
            double yMax = System.Math.Max(point.Y, other.Y);
            var location = new Point(xMax, yMax);
            return location;
        }
  }
}