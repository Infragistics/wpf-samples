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
    public static partial class GeometryTool
    {
        public static double GreatCircleDistance(Point start, Point end)
        {
            double cosφs = Math.Cos(MathTool.Radians(start.Y));
            double sinφs = Math.Sin(MathTool.Radians(start.Y));

            double sinφf = Math.Sin(MathTool.Radians(end.Y));
            double cosφf = Math.Cos(MathTool.Radians(end.Y));

            double cosΔλ = Math.Cos(MathTool.Radians(end.X - start.X));
            double sinΔλ = Math.Sin(MathTool.Radians(end.X - start.X));

            return Math.Atan2(MathTool.Sqr(cosφf * sinΔλ) + MathTool.Sqr(cosφs * sinφf - sinφs * cosφf * cosΔλ), sinφs * sinφf + cosφs * cosφf * cosΔλ); 
        }

        public static Vector3D SphericalInterpolation(Vector3D start, Vector3D end, double p)
        {
            double cosΩ = MathTool.Clamp(Vector3D.DotProduct(start, end) / (start.Length * end.Length), -1.0, 1.0);
            double Ω = Math.Acos(cosΩ);

            return SphericalInterpolation(start, end, Ω, Math.Sin(Ω), p);
        }

        private static Vector3D SphericalInterpolation(Vector3D p0, Vector3D p1, double Ω, double sinΩ, double t)
        {
            return p0 * (Math.Sin((1.0 - t) * Ω) / sinΩ) + p1 * (Math.Sin(t * Ω) / sinΩ);
        }

        // pretty sure there should be a function which does a spherical interpolation of lon/lat along a great
        // circle at a constant step size

        // pretty sure there should be a way to generate projected points along the above interpolation
        // although that would probably need to be in the projection class
    }
}
