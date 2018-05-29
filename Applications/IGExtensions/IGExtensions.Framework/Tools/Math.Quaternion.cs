using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;

namespace IGExtensions.Framework.Tools
{
    /// <summary>
    /// Toolities for Quaternions
    /// </summary>
    public static partial class MathTool
    {
        /// <summary>
        /// Create a quaternion from the specified Eueler angles.
        /// </summary>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="rz"></param>
        /// <returns>A new Quaternion.</returns>
        public static Quaternion FromEulerAngles(double rx, double ry, double rz)
        {
            double bank = Math.PI * rx / 180.0;
            double heading = Math.PI * ry / 180.0;
            double attitude = Math.PI * rz / 180.0;

            double c1 = Math.Cos(0.5 * heading);
            double c2 = Math.Cos(0.5 * attitude);
            double c3 = Math.Cos(0.5 * bank);
            double s1 = Math.Sin(0.5 * heading);
            double s2 = Math.Sin(0.5 * attitude);
            double s3 = Math.Sin(0.5 * bank);

            double w = c1 * c2 * c3 - s1 * s2 * s3;
            double x = s1 * s2 * c3 + c1 * c2 * s3;
            double y = s1 * c2 * c3 + c1 * s2 * s3;
            double z = c1 * s2 * c3 - s1 * c2 * s3;

            double m = Hypot(w, x, y, z);

            double W = w / m;
            double X = x / m;
            double Y = y / m;
            double Z = z / m;

            return new Quaternion(X, Y, Z, W);
        }

        /// <summary>
        /// Gets the Euler angles for the current Quaternion. .
        /// </summary>
        public static double[] EulerAngles(this Quaternion q)
        {
            double heading = Math.Atan2(2.0 * q.Y * q.W - 2.0 * q.X * q.Z, 1.0 - 2.0 * q.Y * q.Y - 2.0 * q.Z * q.Z);
            double attitude = Math.Asin(2.0 * q.X * q.Y + 2.0 * q.Z * q.W);
            double bank = Math.Atan2(2.0 * q.X * q.W - 2.0 * q.Y * q.Z, 1.0 - 2.0 * q.X * q.X - 2.0 * q.Z * q.Z);

            return new double[] { 180.0 * bank / Math.PI, 180.0 * heading / Math.PI, 180.0 * attitude / Math.PI };
        }
#if false
        /// <summary>
        /// Gets the rotation matrix for the current Quaternion. .
        /// </summary>
        public static Matrix3D Matrix(this Quaternion q)
        {
            double m = Math.Sqrt(q.W * q.W + q.X * q.X + q.Y * q.Y + q.Z * q.Z);
            double x = q.X / m;
            double y = q.Y / m;
            double z = q.Z / m;
            double w = q.W / m;

            double xx = x * x;
            double xy = x * y;
            double xz = x * z;
            double xw = x * w;

            double yy = y * y;
            double yz = y * z;
            double yw = y * w;

            double zz = z * z;
            double zw = z * w;

            return new Matrix3D(1.0 - 2.0 * (yy + zz), 2.0 * (xy + zw), 2.0 * (xz - yw), 0.0,
                                2.0 * (xy - zw),1.0 - 2.0 * (xx + zz), 2.0 * (yz + xw), 0.0,
                                2.0 * (xz + yw), 2.0 * (yz - xw), 1.0 - 2.0 * (xx + yy), 0.0,
                                0.0, 0.0, 0.0, 1.0);
        }

        /// <summary>
        /// Rotates a vector by the current Quaternion
        /// </summary>
        /// <param name="q"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3D Transform(this Quaternion q, Vector3D v)
        {
            return q.Matrix().Transform(v); // not the fastest, not the most elegant and not even all that accurate
        }
#endif
        public static Quaternion Nlerp(Quaternion from, Quaternion to, double t0)
        {
            double t1 = 1.0 - t0;

            return new Quaternion(from.X * t0 + to.Y * t1, from.Y * t0 + to.Y * t1, from.Z * t0 + to.Y * t1, from.W * t0+ to.W * t1);
        }
    }
}
