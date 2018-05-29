namespace System.Windows
{
#if SILVERLIGHT //WPF //SILVERLIGHT
    public struct Vector
    {
        public Vector(double x, double y)
            : this()
        {
            X = x;
            Y = y;
        }
        public Vector(Point point1, Point point2)
            : this()
        {
            var deltaX = point1.X - point2.X;
            var deltaY = point1.Y - point2.Y;
            X = deltaX;
            Y = deltaY;
        }
    #region Addition
        public static Point operator +(Vector vector, Point rhs)
        {
            return new Point(vector.X + rhs.X, vector.Y + rhs.Y);
        }
        public static Point Add(Vector vector, Point point)
        {
            return new Point(vector.X + point.X, vector.Y + point.Y);
        }
        public static Vector operator +(Vector lhs, Vector rhs)
        {
            return new Vector(lhs.X + rhs.X, lhs.Y + rhs.Y);
        }
        public static Vector Add(Vector lhs, Vector rhs)
        {
            return new Vector(lhs.X + rhs.X, lhs.Y + rhs.Y);
        }
    #endregion

    #region Subtraction
        public static Vector operator -(Vector lhs, Vector rhs)
        {
            return new Vector(lhs.X - rhs.X, lhs.Y - rhs.Y);
        }
        public static Vector Subtract(Vector lhs, Vector rhs)
        {
            return new Vector(lhs.X - rhs.X, lhs.Y - rhs.Y);
        }
    #endregion

    #region Division
        public static Vector operator /(Vector lhs, double scalar)
        {
             return new Vector(lhs.X / scalar, lhs.Y / scalar);
        }
        public static Vector Divide(Vector lhs, double scalar)
        {
            return new Vector(lhs.X / scalar, lhs.Y / scalar);
        }
       
    #endregion

    #region Multiplication
        public static Vector operator *(double scalar, Vector vector)
        {
            return new Vector(scalar * vector.X, scalar * vector.Y);
        }
        public static Vector Multiply(double scalar, Vector rhs)
        {
            return new Vector(scalar * rhs.X, scalar * rhs.Y);
        }
        public static Vector Multiply(Vector vector, double scalar)
        {
            return new Vector(vector.X * scalar, vector.Y * scalar);
        }
        public static Vector operator *(Vector vector, double scalar)
        {
            return new Vector(vector.X * scalar, vector.Y * scalar);
        }
       

#if false
        public static Vector operator *(Vector lhs, Matrix rhs)
        {
        }
        public static Vector Multiply(Vector lhs, Matrix rhs)
        {
        }
#endif
        public static double operator *(Vector lhs, Vector rhs)
        {
            return lhs.X * rhs.X + lhs.Y * rhs.Y;
        }
        public static double Multiply(Vector lhs, Vector rhs)
        {
            return lhs.X * rhs.X + lhs.Y * rhs.Y;
        }
    #endregion

        public static double AngleBetween(Vector lhs, Vector rhs)
        {
            return Math.Acos((lhs*rhs)/(lhs.Length*rhs.Length));
        }
#if false
        public static Vector CrossProduct(Vector lhs, Vector rhs)
        {
        }
#endif
        public static double Determinant(Vector lhs, Vector rhs)
        {
            return lhs.X * rhs.Y - lhs.Y * rhs.Y;
        }
        public void Normalize()
        {
            double length = Length;

            if (length > 0.0)
            {
                X = X / length;
                Y = Y / length;
            }
        }

        public static Vector operator -(Vector vector)
        {
            return new Vector(-vector.X, -vector.Y);
        }
        public void Negate()
        {
            X = -X;
            Y = -Y;
        }

    #region Converstion
		public static explicit operator Point(Vector vector)
        {
            return new Point(vector.X, vector.Y);
        }
        public static explicit operator Size(Vector vector)
        {
            return new Size(vector.X, vector.Y);
        } 
    #endregion

        public double X { get; set; }
        public double Y { get; set; }
        public double LengthSquared
        {
            get { return X * X + Y * Y; }
        }
        public double Length
        {
            get { return Math.Sqrt(X * X + Y * Y); }
        }
    }
    
#endif

    public static class VectorEx
     {
         #region Division
         //public static Vector operator -(this Vector lhs, double scalar)
         //{
         //    return new Vector(lhs.X / scalar, lhs.Y / scalar);
         //}
         //public static Vector Divide(this Vector lhs, double scalar)
         //{
         //    return new Vector(lhs.X / scalar, lhs.Y / scalar);
         //}
         //public static Vector Multiply(this Vector vector, double scalar)
         //{
         //   return new Vector(vector.X * scalar, vector.Y * scalar);
         //}
         #endregion
     }
}
