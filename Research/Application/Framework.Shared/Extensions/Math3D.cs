using System.Diagnostics;
using System.Windows.Media.Media3D;
using Infragistics;

namespace System.Windows.Controls
{
    public static class Math3D
    {
        public static class Pythagoras
        {
            public static double GetLength(double a, double b)
            {
                var length = (a * a) + (b * b);
                return Math.Sqrt(length);
            }

            public static double GetSide(double a, double length)
            {
                var side = (length * length) - (a * a);
                return Math.Sqrt(side);
            }
        }
        

        public static int GetDecimals(double value)
        {
            int count = BitConverter.GetBytes(decimal.GetBits((decimal)value)[3])[2];
            return count;
        }
        /// <summary> Converts Euler's rotation Vector to Quaternion </summary>
        public static Quaternion GetQuaternion(Vector3D v)
        {
            return GetQuaternion(v.Y, v.X, v.Z);
        } 
        private static double ToRadian(double angleDegrees)
        {
            return Math.PI * angleDegrees / 180.0;
        }
        private static double ToDegree(double angleRadians)
        {
            return angleRadians * (180.0 / Math.PI);
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

            yaw = (float)ToRadian(yaw);
            roll = (float)ToRadian(roll);
            pitch = (float)ToRadian(pitch);

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
        public static Vector3D GetVector(this Quaternion q)
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

            v.Y = (float)ToDegree(pitch);
            v.X = (float)ToDegree(roll);
            v.Z = (float)ToDegree(yaw);

            return v;
        }

        public static Vector3D Normalize(Vector3D angles)
        {
            angles.X = Normalize(angles.X);
            angles.Y = Normalize(angles.Y);
            angles.Z = Normalize(angles.Z);
            return angles;
        }

        public static double Normalize(double angle)
        {
            while (angle > 360)
                angle -= 360;
            while (angle < 0)
                angle += 360;
            return angle;
        }

        public static AxisAngleRotation3D GetAngleRotation(double x, double y, double z)
        {
            var xRad = ToRadian(x);
            var yRad = ToRadian(y);
            var zRad = ToRadian(z);
            var xSin = Math.Sin(xRad / 2);
            var xCos = Math.Cos(xRad / 2);
            var ySin = Math.Sin(yRad / 2);
            var yCos = Math.Cos(yRad / 2);
            var zSin = Math.Sin(zRad / 2);
            var zCos = Math.Cos(zRad / 2);

            var info = string.Format("{0:0}, {1:0}, {2:0}", x, y, z);

            Debug.WriteLine(info);

            var angle = ToDegree(2 * Math.Acos(yCos * zCos * xCos + ySin * zSin * xSin));
            var xAxis = yCos * zCos * xSin - ySin * zSin * xCos;
            var yAxis = yCos * zSin * xCos + ySin * zCos * xSin;
            var zAxis = ySin * zCos * xCos - yCos * zSin * xSin;

            var axis = new Vector3D(xAxis, yAxis, zAxis);
            return new AxisAngleRotation3D(axis, angle);
        }
    }

}