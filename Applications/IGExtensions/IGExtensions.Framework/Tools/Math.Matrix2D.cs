using System.Diagnostics.CodeAnalysis;
using System.Windows.Media;
using System.Windows;
using System;

namespace IGExtensions.Framework.Tools
{
    
    public static partial class MathTool
    {
#if SILVERLIGHT
        public static Matrix Translated(this Matrix matrix, double offsetX, double offsetY)
        {
            return Multiply(matrix, MatrixFromTranslation(offsetX, offsetY));
        }
        public static Matrix Scaled(this Matrix matrix, double scaleX, double scaleY)
        {
            return Multiply(matrix, MatrixFromScale(scaleX, scaleY));
        }
        public static Matrix Rotated(this Matrix matrix, double angle)
        {
            return Multiply(matrix, MatrixFromRotation(angle));
        }
#endif

        public static Matrix MatrixFromTranslation(double translationX, double translationY)
        {
            return new Matrix(1.0, 0.0, /* 0 */
                                0.0, 1.0, /* 0 */
                                translationX, translationY /*, 1*/);
        }
        public static Matrix MatrixFromScale(double scaleX, double scaleY)
        {
            return new Matrix(scaleX, 0,      /* 0 */
                                0.0, scaleY, /* 0 */
                                0.0, 0.0       /*, 1*/);
        }
        public static Matrix MatrixFromRotation(double rotation)
        {
            double cos = Math.Cos(Math.PI * rotation / 180.0);
            double sin = Math.Sin(Math.PI * rotation / 180.0);

            return new Matrix(cos, -sin,   /* 0 */
                                sin, cos,    /* 0 */
                                0.0, 0.0     /*, 1*/);
        }

        /// <summary>
        /// Gets the inverse of the matrix.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>Inverse matrix.</returns>
        public static Matrix Inverse(this Matrix matrix)
        {
            Matrix im = new Matrix();

            double det = matrix.M11 * matrix.M22 - matrix.M21 * matrix.M12;

            if (det != 0)
            {
                im.M11 = matrix.M22 / det;
                im.M21 = -matrix.M21 / det;
                im.OffsetX = (matrix.M21 * matrix.OffsetY - matrix.OffsetX * matrix.M22) / det;

                im.M12 = -matrix.M12 / det;
                im.M22 = matrix.M11 / det;
                im.OffsetY = (matrix.OffsetX * matrix.M12 - matrix.M11 * matrix.OffsetY) / det;
            }

            return im;
        }

        public static Matrix Multiply(Matrix lhs, Matrix rhs)
        {
            double M11 = lhs.M11 * rhs.M11 + lhs.M12 * rhs.M21 + 0 * rhs.OffsetX;
            double M12 = lhs.M11 * rhs.M12 + lhs.M12 * rhs.M22 + 0 * rhs.OffsetY;
            //double M13 = lhs.M11 * 0 + lhs.M12 * 0 + 0 * 1; // 0
            double M21 = lhs.M21 * rhs.M11 + lhs.M22 * rhs.M21 + 0 * rhs.OffsetX;
            double M22 = lhs.M21 * rhs.M12 + lhs.M22 * rhs.M22 + 0 * rhs.OffsetY;
            //double M23 = lhs.M21 * 0 + lhs.M22 * 0 + 0 * 1; // 0
            double M31 = lhs.OffsetX * rhs.M11 + lhs.OffsetY * rhs.M21 + 1 * rhs.OffsetX;
            double M32 = lhs.OffsetX * rhs.M12 + lhs.OffsetY * rhs.M22 + 1 * rhs.OffsetY;
            //double M33 = lhs.OffsetX * 0 + lhs.OffsetY * 0 + 1 * 1; // 1

            return new Matrix(M11, M12, M21, M22, M31, M32);
        }

        public static Point Multiply(Matrix lhs, Point rhs)
        {
            double x = lhs.M11 * rhs.X + lhs.M12 * rhs.Y + lhs.OffsetX /* *rhs.Z */;
            double y = lhs.M21 * rhs.X + lhs.M22 * rhs.Y + lhs.OffsetY /* *rhs.Z */;

            return new Point(x, y);
        }
    }
}
