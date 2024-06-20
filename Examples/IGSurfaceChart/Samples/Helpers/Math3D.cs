using Infragistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;

namespace IGSurfaceChart.Samples.Helpers
{
    public static class Math3D
    {
        public static Quaternion GetQuaternionFromAngles(double x, double y, double z)
        {
            return GetQuaternion(y, z, x);
        }

        /// <summary> Converts Euler's rotation angles to Quaternion </summary>
        /// <param name="yaw">rotation angle on z-axis</param>
        /// <param name="pitch">rotation angle on y-axis</param>
        /// <param name="roll">rotation angle on x-axis</param> 
        public static Quaternion GetQuaternion(double pitch, double yaw, double roll)
        {
            // heading	    azimuth	    yaw	    z
            // attitude	    elevation	pitch   y	 
            // bank	        tilt	    roll    x

            yaw = (float)MathUtil.Radians(yaw);
            roll = (float)MathUtil.Radians(roll);
            pitch = (float)MathUtil.Radians(pitch);

            var c1 = Math.Cos(pitch);
            var s1 = Math.Sin(pitch);
            var c2 = Math.Cos(yaw);
            var s2 = Math.Sin(yaw);
            var c3 = Math.Cos(roll);
            var s3 = Math.Sin(roll);

            var w = Math.Sqrt(1.0 + c1 * c2 + c1 * c3 - s1 * s2 * s3 + c2 * c3) / 2.0;
            var w4 = (4.0 * w);
            var x = (c2 * s3 + c1 * s3 + s1 * s2 * c3) / w4;
            var y = (s1 * c2 + s1 * c3 + c1 * s2 * s3) / w4;
            var z = (-s1 * s3 + c1 * s2 * c3 + s2) / w4;
            return new Quaternion(x, y, z, w);
        }

        /// <summary> Converts Quaternion rotation to Euler's angle vector </summary>
        public static Vector3D GetVector(Quaternion q)
        {
            var rad2Deg = 360 / (Math.PI * 2);

            var v = new Vector3D();
            var test = q.X * q.Y + q.Z * q.W;
            if (test > 0.499)
            { // singularity at north pole
                v.Y = 2 * Math.Atan2(q.X, q.W);
                v.Z = Math.PI / 2;
                v.X = 0;
                return v * rad2Deg;
            }
            if (test < -0.499)
            { // singularity at south pole
                v.Y = -2 * Math.Atan2(q.X, q.W);
                v.Z = -Math.PI / 2;
                v.X = 0;
                return v * rad2Deg;
            }
            var sqx = q.X * q.X;
            var sqy = q.Y * q.Y;
            var sqz = q.Z * q.Z;
            var pitch = Math.Atan2(2 * q.Y * q.W - 2 * q.X * q.Z, 1 - 2 * sqy - 2 * sqz);
            var yaw = Math.Asin(2 * test);
            var roll = Math.Atan2(2 * q.X * q.W - 2 * q.Y * q.Z, 1 - 2 * sqx - 2 * sqz);

            v.Y = (float)MathUtil.Degrees(pitch);
            v.X = (float)MathUtil.Degrees(roll);
            v.Z = (float)MathUtil.Degrees(yaw);

            return v;
        }
    }
}
