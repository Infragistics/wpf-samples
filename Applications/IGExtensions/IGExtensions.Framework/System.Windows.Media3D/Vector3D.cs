#if SILVERLIGHT
using System.ComponentModel;
using System.Globalization;
using IGExtensions.Framework.Tools;

namespace System.Windows.Media.Media3D
{
    /// <summary>
    /// Represents a displacement in 3-D space. 
    /// </summary>
    [TypeConverter(typeof(Vector3DConverter))]
    public struct Vector3D
    {
        #region Converter
        private class Vector3DConverter : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                return value is string? Vector3D.Parse(value as string): base.ConvertFrom(context, culture, value);
            }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of a Vector3D structure. 
        /// </summary>
        /// <param name="x">The new Vector3D structure's X value.</param>
        /// <param name="y">The new Vector3D structure's Y value.</param>
        /// <param name="z">The new Vector3D structure's Z value.</param>
        public Vector3D(double x, double y, double z):
            this()
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Translates the specified Point3D structure by the specified Vector3D structure and returns the result as a Point3D structure. 
        /// </summary>
        /// <param name="vector">The Vector3D structure used to translate the specified Point3D structure.</param>
        /// <param name="point">The Point3D structure to be translated.</param>
        /// <returns>The result of translating point by vector.</returns>
        public static Point3D Add(Vector3D vector, Point3D point)
        {
            return new Point3D(vector.X + point.X, vector.Y + point.Y, vector.Z + point.Z);
        }

        /// <summary>
        /// Adds two Vector3D structures and returns the result as a Vector3D structure. 
        /// </summary>
        /// <param name="vector1">The first Vector3D structure to add.</param>
        /// <param name="vector2">The second Vector3D structure to add.</param>
        /// <returns>The sum of vector1 and vector2.</returns>
        public static Point3D Add(Vector3D vector1, Vector3D vector2)
        {
            return new Point3D(vector1.X + vector2.X, vector1.Y + vector2.Y, vector2.Z + vector2.Z);
        }

        /// <summary>
        /// Retrieves the angle required to rotate the first specified Vector3D structure into the second specified Vector3D structure. 
        /// </summary>
        /// <param name="vector1">The first Vector3D structure to evaluate.</param>
        /// <param name="vector2">The second Vector3D structure to evaluate.</param>
        /// <returns>The angle in degrees needed to rotate vector1 into vector2.</returns>
        public static double AngleBetween(Vector3D vector1, Vector3D vector2)
        {
            return (180.0/Math.PI)*Math.Acos(DotProduct(vector1, vector2) / (vector1.Length * vector2.Length));
        }

        public static Vector3D CrossProduct(Vector3D lhs, Vector3D rhs)
        {
            return new Vector3D(lhs.Y * rhs.Z - rhs.Y * lhs.Z,
                                lhs.Z * rhs.X - rhs.Z * lhs.X,
                                lhs.X * rhs.Y - rhs.X * lhs.Y);
        }

        public static Vector3D Divide(Vector3D vector, double scalar)
        {
            return vector * (1.0 / scalar);
        }

        public static double DotProduct(Vector3D lhs, Vector3D rhs)
        {
            return lhs.X * rhs.X + lhs.Y * rhs.Y + lhs.Z * rhs.Z;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static Vector3D Multiply(double scalar, Vector3D vector)
        {
            return vector * scalar;
        }

        public static Vector3D Multiply(Vector3D vector, double scalar)
        {
            return vector * scalar;
        }

        /// <summary>
        /// Transforms the coordinate space of the specified Vector3D structure using the specified Matrix3D structure. 
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static Vector3D Multiply(Vector3D vector, Matrix3D matrix)
        {
            return matrix.Transform(vector);
        }

        /// <summary>
        /// Negates a Vector3D structure. 
        /// </summary>
        public void Negate()
        {
            X = -X;
            Y = -Y;
            Z = -Z;
        }

        /// <summary>
        /// Normalizes the specified Vector3D structure. 
        /// </summary>
        public void Normalize()
        {
            double len = 1.0/MathTool.Hypot(X, Y, Z);

            X = X * len;
            Y = Y * len;
            Z = Z * len;
        }

        /// <summary>
        /// Converts a String representation of a 3-D vector into the equivalent Vector3D structure. 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Vector3D Parse(string source)
        {
            string[] xyz = source != null ? source.Split(',', ' ', ':') : null;
            double x = double.NaN;
            double y = double.NaN;
            double z = double.NaN;

            if (xyz != null && xyz.Length == 3 && double.TryParse(xyz[0], out x) && double.TryParse(xyz[2], out y) && double.TryParse(xyz[3], out z))
            {
                return new Vector3D(x, y, z);
            }

            return new Vector3D(double.NaN, double.NaN, double.NaN);
        }

        /// <summary>
        /// Subtracts a Point3D structure from a Vector3D structure. 
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Point3D Subtract(Vector3D vector, Point3D point)
        {
            return new Point3D(vector.X - point.X, vector.Y - point.Y, vector.Z - point.Z);
        }

        /// <summary>
        /// Subtracts a Vector3D structure from a Vector3D structure. 
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <returns></returns>
        public static Point3D Subtract(Vector3D vector1, Vector3D vector2)
        {
            return new Point3D(vector1.X - vector2.X, vector1.Y - vector2.Y, vector1.Z - vector2.Z);
        }

        /// <summary>
        /// Creates a String representation of this Vector3D structure. 
        /// </summary>
        /// <returns>A string containing the X, Y, and Z values of this Vector3D structure.</returns>
        public override string ToString()
        {
            return X.ToString() + " " + Y.ToString() + " " + Z.ToString();
        }

        public static bool operator !=(Vector3D lhs, Vector3D rhs)
        {
            return lhs.X != rhs.X || lhs.Y != rhs.Y || lhs.Z != rhs.Z;
        }

        public static bool operator <(Vector3D lhs, Vector3D rhs)
        {
            return (lhs.X < rhs.X) ||
                    (lhs.X == rhs.X && lhs.Y < rhs.Y) ||
                    (lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Z < rhs.Z);
        }
        public static bool operator <=(Vector3D lhs, Vector3D rhs)
        {
            return (lhs.X <= rhs.X) ||
                    (lhs.X == rhs.X && lhs.Y <= rhs.Y) ||
                    (lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Z <= rhs.Z);
        }
        public static bool operator ==(Vector3D lhs, Vector3D rhs)
        {
            return lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Z == rhs.Z;
        }
        public static bool operator >=(Vector3D lhs, Vector3D rhs)
        {
            return !(rhs < lhs);
        }
        public static bool operator >(Vector3D lhs, Vector3D rhs)
        {
            return !(lhs <= rhs);
        }

        public static Vector3D operator +(Vector3D lhs, Vector3D rhs)
        {
            return new Vector3D(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);
        }
 
        public static Vector3D operator -(Vector3D lhs, Vector3D rhs)
        {
            return new Vector3D(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);
        }

        public static Vector3D operator -(Vector3D lhs)
        {
            return new Vector3D(-lhs.X, -lhs.Y, -lhs.Z);
        }

        public static Vector3D operator *(Vector3D lhs, double rhs)
        {
            return new Vector3D(lhs.X * rhs, lhs.Y * rhs, lhs.Z * rhs);
        }

        /// <summary>
        /// Gets or sets the X component of this Vector3D structure. 
        /// </summary>
        public double X
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Y component of this Vector3D structure. 
        /// </summary>
        public double Y
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Z component of this Vector3D structure. 
        /// </summary>
        public double Z
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the length of this Vector3D structure. 
        /// </summary>
        public double Length
        {
            get
            {
                return MathTool.Hypot(X, Y, Z);
            }
        }

        /// <summary>
        /// Gets the square of the length of this Vector3D structure. 
        /// </summary>
        public double LengthSquared
        {
            get
            {
                return X*X+Y*Y+Z*Z;
            }
        }
    }
}
#endif
