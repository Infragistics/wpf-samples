using System;
using System.Windows.Media.Media3D;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;
using IGExtensions.Framework.Tools;
using System.Globalization;

namespace IGExtensions.Framework.Tools
{
    
    public static partial class MathTool
    {
        public static Matrix3D Matrix3DFromShadow(Vector3D pointOnPlane, Vector3D normalToPlane, Vector3D light)
        {
            double d = light.X*normalToPlane.X+light.Y*normalToPlane.Y+light.Z*normalToPlane.Z;
            double c = pointOnPlane.X*normalToPlane.X+pointOnPlane.Y*normalToPlane.Y+pointOnPlane.Z*normalToPlane.Z - d;

            return new Matrix3D(
                light.X*normalToPlane.X+c, light.Y*normalToPlane.X, light.Z*normalToPlane.X, normalToPlane.X,
                light.X*normalToPlane.Y, light.Y*normalToPlane.Y+c, light.Z*normalToPlane.Y, normalToPlane.Y,
                light.X*normalToPlane.Z, light.Y*normalToPlane.Z, light.Z*normalToPlane.Z, normalToPlane.Z,
                -c*light.X-d*light.X, -c*light.Y-d*light.Y,-c*light.Z -d*light.Z, -d);
        }
        public static Matrix3D Matrix3DFromPerspectiveFovLH(double fovy, double aspect, double near, double far)
        {
            double fov = Math.PI * fovy / 180.0;
            double yscale=1.0/Math.Tan(fov/2.0);
            double xscale=yscale/aspect;

            return new Matrix3D(
                xscale, 0.0, 0.0, 0.0,
                0.0, yscale, 0.0, 0.0,
                0.0, 0.0, far/(far-near), 1.0,
                0.0, 0.0, -near*far/(far-near), 0.0
            );
        }
        public static Matrix3D Matrix3DFromPerspectiveLH(double width, double height, double near, double far)
        {
            return new Matrix3D(
                (2.0 * near) / width, 0.0, 0.0, 0.0,
                0.0, (2.0 * near) / height, 0.0, 0.0,
                0, 0, far / (far - near), 1.0,
                0.0, 0.0, (near*far) / (near-far), 0.0
            );
        }
        public static Matrix3D Matrix3DFromOrthoLH(double width, double height, double near, double far)
        {
            return new Matrix3D(
                2.0 / width, 0.0, 0.0, 0.0,
                0.0, 2.0 / height, 0.0, 0.0,
                0.0, 0.0, 1.0 / (far - near), 0.0,
                0, 0, near / (near - far), 1.0
            );
        }
        public static Matrix3D Matrix3DFromRotation(double angle, double x, double y, double z)
        {
            double c = Math.Cos(Math.PI * angle / 180.0);
            double s = Math.Sin(Math.PI * angle / 180.0);

            double x2 = x * x;
            double xy = x * y;
            double xz = x * z;

            double y2 = y * y;
            double yz = y * z;

            double z2 = z * z;

            double xs = x * s;
            double ys = y * s;
            double zs = z * s;

            return new Matrix3D(
                x2 * (1.0 - c) + c, xy * (1.0 - c) + zs, xz * (1.0 - c) - ys, 0.0,
                xy * (1.0 - c) - zs, y2 * (1.0 - c) + c, yz * (1.0 - c) + xs, 0.0,
                xz * (1.0 - c) + ys, yz * (1.0 - c) - xs, z2 * (1.0 - c) + c, 0.0,
                0.0, 0.0, 0.0, 1.0
            );
        }
        public static Matrix3D Matrix3DFromRotation(Quaternion quaternion)
        {
            double m = Math.Sqrt(quaternion.W * quaternion.W + quaternion.X * quaternion.X + quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z);
            double x = quaternion.X / m;
            double y = quaternion.Y / m;
            double z = quaternion.Z / m;
            double w = quaternion.W / m;

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
                                2.0 * (xy - zw), 1.0 - 2.0 * (xx + zz), 2.0 * (yz + xw), 0.0,
                                2.0 * (xz + yw), 2.0 * (yz - xw), 1.0 - 2.0 * (xx + yy), 0.0,
                                0.0, 0.0, 0.0, 1.0);
        }
        public static Matrix3D Matrix3DFromTranslation(double x, double y, double z)
        {
            return new Matrix3D
            (
                1.0, 0.0, 0.0, 0.0,
                0.0, 1.0, 0.0, 0.0,
                0.0, 0.0, 1.0, 0.0,
                x, y, z, 1.0
            );
        }
        public static Matrix3D Matrix3DFromScale(double sx, double sy, double sz)
        {
            return new Matrix3D
            (
                sx, 0.0, 0.0, 0.0,
                0.0, sy, 0.0, 0.0,
                0.0, 0.0, sz, 0.0,
                0.0, 0.0, 0.0, 1.0
            );
        }
        public static Matrix3D Matrix3DFromViewportScale(double left, double width, double top, double height)
        {
            return new Matrix3D(width / 2.0, 0, 0, 0,
                                0, -height / 2.0, 0, 0,
                                0, 0, 1, 0,
                                left + width / 2.0, top + height / 2.0, 0, 1);
        }

        public static Matrix3D GetTranspose(this Matrix3D matrix)
        {
            return new Matrix3D(matrix.M11, matrix.M21, matrix.M31, matrix.OffsetX,
                                matrix.M12, matrix.M22, matrix.M32, matrix.OffsetY,
                                matrix.M13, matrix.M23, matrix.M33, matrix.OffsetZ,
                                matrix.M14, matrix.M24, matrix.M34, matrix.M44);
        }
        public static Matrix3D GetInverse(this Matrix3D matrix)
        {
            Matrix3D ret = matrix;

            ret.Invert();

            return ret;
        }
        public static Matrix3D Multiply(this Matrix3D lhs, Matrix3D rhs)
        {
            double m11 = lhs.M11 * rhs.M11 + lhs.M12 * rhs.M21 + lhs.M13 * rhs.M31 + lhs.M14 * rhs.OffsetX;
            double m21 = lhs.M21 * rhs.M11 + lhs.M22 * rhs.M21 + lhs.M23 * rhs.M31 + lhs.M24 * rhs.OffsetX;
            double m31 = lhs.M31 * rhs.M11 + lhs.M32 * rhs.M21 + lhs.M33 * rhs.M31 + lhs.M34 * rhs.OffsetX;
            double m41 = lhs.OffsetX * rhs.M11 + lhs.OffsetY * rhs.M21 + lhs.OffsetZ * rhs.M31 + lhs.M44 * rhs.OffsetX;

            double m12 = lhs.M11 * rhs.M12 + lhs.M12 * rhs.M22 + lhs.M13 * rhs.M32 + lhs.M14 * rhs.OffsetY;
            double m22 = lhs.M21 * rhs.M12 + lhs.M22 * rhs.M22 + lhs.M23 * rhs.M32 + lhs.M24 * rhs.OffsetY;
            double m32 = lhs.M31 * rhs.M12 + lhs.M32 * rhs.M22 + lhs.M33 * rhs.M32 + lhs.M34 * rhs.OffsetY;
            double m42 = lhs.OffsetX * rhs.M12 + lhs.OffsetY * rhs.M22 + lhs.OffsetZ * rhs.M32 + lhs.M44 * rhs.OffsetY;

            double m13 = lhs.M11 * rhs.M13 + lhs.M12 * rhs.M23 + lhs.M13 * rhs.M33 + lhs.M14 * rhs.OffsetZ;
            double m23 = lhs.M21 * rhs.M13 + lhs.M22 * rhs.M23 + lhs.M23 * rhs.M33 + lhs.M24 * rhs.OffsetZ;
            double m33 = lhs.M31 * rhs.M13 + lhs.M32 * rhs.M23 + lhs.M33 * rhs.M33 + lhs.M34 * rhs.OffsetZ;
            double m43 = lhs.OffsetX * rhs.M13 + lhs.OffsetY * rhs.M23 + lhs.OffsetZ * rhs.M33 + lhs.M44 * rhs.OffsetZ;

            double m14 = lhs.M11 * rhs.M14 + lhs.M12 * rhs.M24 + lhs.M13 * rhs.M34 + lhs.M14 * rhs.M44;
            double m24 = lhs.M21 * rhs.M14 + lhs.M22 * rhs.M24 + lhs.M23 * rhs.M34 + lhs.M24 * rhs.M44;
            double m34 = lhs.M31 * rhs.M14 + lhs.M32 * rhs.M24 + lhs.M33 * rhs.M34 + lhs.M34 * rhs.M44;
            double m44 = lhs.OffsetX * rhs.M14 + lhs.OffsetY * rhs.M24 + lhs.OffsetZ * rhs.M34 + lhs.M44 * rhs.M44;

            return new Matrix3D(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
        }

        public static Vector3D Transform(this Matrix3D lhs, Vector3D rhs)
        {
            double ex = lhs.M11 * rhs.X + lhs.M21 * rhs.Y + lhs.M31 * rhs.Z + lhs.OffsetX * 1.0;
            double ey = lhs.M12 * rhs.X + lhs.M22 * rhs.Y + lhs.M32 * rhs.Z + lhs.OffsetY * 1.0;
            double ez = lhs.M13 * rhs.X + lhs.M23 * rhs.Y + lhs.M33 * rhs.Z + lhs.OffsetZ * 1.0;
            double ew = lhs.M14 * rhs.X + lhs.M24 * rhs.Y + lhs.M34 * rhs.Z + lhs.M44 * 1.0;

            return new Vector3D(ex / ew, ey / ew, ez / ew);
        }
    }
}
