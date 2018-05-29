#if SILVERLIGHT
using IGExtensions.Framework.Tools;
using System.Windows.Media.Animation;
using System.ComponentModel;
using System.Windows.Media.Media3D;

namespace System.Windows.Media.Media3D
{
    /// <summary>
    /// Implemenation of the useful parts of System.Windows.Media.Media3D.Quaternion
    /// </summary>
    public struct Quaternion
    {
        /// <summary>
        /// Creates a new Quaternion. 
        /// </summary>
        /// <param name="axisOfRotation">Vector3D that represents the axis of rotation.</param>
        /// <param name="angleInDegrees">Angle to rotate around the specified axis, in degrees.</param>
        public Quaternion(Vector3D axisOfRotation, double angleInDegrees):
            this()
        {
            Axis = axisOfRotation;
            Angle = angleInDegrees;

            axisOfRotation.Normalize();

            double theta = Math.PI*angleInDegrees/180.0;

            double w = Math.Cos(0.5 * theta);
            double x = Axis.X * Math.Sin(0.5 * theta);
            double y = Axis.Y * Math.Sin(0.5 * theta);
            double z = Axis.Z * Math.Sin(0.5 * theta);

            double m = MathTool.Hypot(x, y, z, w);

            X = x / m;
            Y = y / m;
            Z = z / m;
            W = w / m;
        }

        /// <summary>
        /// Creates a new Quaternion. 
        /// </summary>
        /// <param name="x">Value of the new Quaternion's X coordinate.</param>
        /// <param name="y">Value of the new Quaternion's Y coordinate.</param>
        /// <param name="z">Value of the new Quaternion's Z coordinate.</param>
        /// <param name="w">Value of the new Quaternion's W coordinate.</param>
        public Quaternion(double x, double y, double z, double w):
            this()
        {
            double m = MathTool.Hypot(x, y, z, w);

            X = x / m;
            Y = y / m;
            Z = z / m;
            W = w / m;
            
            double s = Math.Sqrt(1.0 - W * W);

            Angle = 2.0 * Math.Acos(W);

            if (s != 0.0)
            {
                Axis = new Vector3D(X / s, Y / s, Z / s);
            }
            else
            {
                Axis = new Vector3D(0, 0, 1);
            }
        }

        /// <summary>
        /// Gets the W component of the Quaternion.
        /// </summary>
        public double W { get; private set; }

        /// <summary>
        /// Gets the X component of the Quaternion.
        /// </summary>
        public double X { get; private set; }

        /// <summary>
        /// Gets the Y component of the Quaternion.
        /// </summary>
        public double Y { get; private set; }

        /// <summary>
        /// Gets the Z component of the Quaternion.
        /// </summary>
        public double Z { get; private set; }

        /// <summary>
        /// Gets the Quaternion's angle, in degrees. 
        /// </summary>
        public double Angle { get; private set; }

        /// <summary>
        /// Gets the Quaternion's axis.
        /// </summary>
        public Vector3D Axis { get; private set; }

        /// <summary>
        /// Gets the identity Quaternion 
        /// </summary>
        public static Quaternion Identity
        {
            get { return identity; }
        }
        private static Quaternion identity = new Quaternion(0, 0, 0, 1);

        public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
        {
            return new Quaternion(
                /* X */lhs.X * rhs.W + lhs.Y * rhs.Z - lhs.Z * rhs.Y + lhs.W * rhs.X,
                /* Y */-lhs.X * rhs.Z + lhs.Y * rhs.W + lhs.Z * rhs.X + lhs.W * rhs.Y,
                /* Z */lhs.X * rhs.Y - lhs.Y * rhs.X + lhs.Z * rhs.W + lhs.W * rhs.Z,
                /* W */ -lhs.X * rhs.X - lhs.Y * rhs.Y - lhs.Z * rhs.Z + lhs.W * rhs.W);
        }

        /// <summary>
        /// Interpolates between two orientations using spherical linear interpolation. 
        /// </summary>
        /// <param name="from">Quaternion that represents the starting orientation.</param>
        /// <param name="to">Quaternion that represents the ending orientation</param>
        /// <param name="t">Interpolation coefficient.</param>
        /// <returns>Quaternion that represents the orientation resulting from the interpolation.</returns>
        public static Quaternion Slerp(Quaternion from, Quaternion to, double t)
        {
            return Slerp(from, to, t, false);
        }

        /// <summary>
        /// Interpolates between two orientations using spherical linear interpolation. 
        /// </summary>
        /// <param name="from">Quaternion that represents the starting orientation.</param>
        /// <param name="to">Quaternion that represents the ending orientation</param>
        /// <param name="t">Interpolation coefficient.</param>
        /// <param name="useShortestPath">Boolean that indicates whether to compute quaternions that constitute the shortest possible arc on a four-dimensional unit sphere.</param>
        /// <returns>Quaternion that represents the orientation resulting from the interpolation.</returns>
        public static Quaternion Slerp(Quaternion from, Quaternion to, double t, bool useShortestPath)
        {
            double cosTheta = MathTool.Clamp(from.X * to.X + from.Y * to.Y + from.Z * to.Z + from.W * to.W, -1.0, 1.0);

            if (useShortestPath && cosTheta < 0)
            {
                from = new Quaternion(-from.X, -from.Y, -from.Z, -from.W);
                cosTheta = -cosTheta;
            }

            double theta = Math.Acos(cosTheta);
            double sinTheta = Math.Sin(theta);

            double t0 = sinTheta > 0.001 ? Math.Sin((1.0f - t) * theta) / sinTheta : 1.0f - t;
            double t1 = sinTheta > 0.001 ? Math.Sin(t * theta) / sinTheta : t;

            return new Quaternion(from.X * t0 + to.X * t1, from.Y * t0 + to.Y * t1, from.Z * t0 + to.Z * t1, from.W * t0 + to.W * t1);
        }
    }

}
#endif
