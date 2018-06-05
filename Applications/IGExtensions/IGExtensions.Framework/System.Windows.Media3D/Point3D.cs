#if SILVERLIGHT

namespace System.Windows.Media.Media3D
{
    public struct Point3D
    {
        public Point3D(double x, double y, double z) :
            this()
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }
}
#endif