using System.Collections.Generic; 

namespace Infragistics.Framework 
{
    public class AllSurfaces : List<Shape3D>
    {
        public AllSurfaces()
        {
            this.AddRange(new Shapes());
            this.AddRange(new Surfaces());
        }
    }
    //Vortex Surface
    public class VortexSurface : List<Point3D>
    {
        public VortexSurface()
        {
            /*
            3 3 3 3 3
            3 2 2 2 3
            3 2 1 2 3
            3 2 2 2 3
            3 3 3 3 3
            */
            this.Add(new Point3D() { X = 0, Y = 0, Z = 3 });
            this.Add(new Point3D() { X = 1, Y = 0, Z = 3 });
            this.Add(new Point3D() { X = 2, Y = 0, Z = 3 });
            this.Add(new Point3D() { X = 3, Y = 0, Z = 3 });
            this.Add(new Point3D() { X = 4, Y = 0, Z = 3 });
            this.Add(new Point3D() { X = 0, Y = 1, Z = 3 });
            this.Add(new Point3D() { X = 1, Y = 1, Z = 2 });
            this.Add(new Point3D() { X = 2, Y = 1, Z = 2 });
            this.Add(new Point3D() { X = 3, Y = 1, Z = 2 });
            this.Add(new Point3D() { X = 4, Y = 1, Z = 3 });
            this.Add(new Point3D() { X = 0, Y = 2, Z = 3 });
            this.Add(new Point3D() { X = 1, Y = 2, Z = 2 });
            this.Add(new Point3D() { X = 2, Y = 2, Z = 1 });
            this.Add(new Point3D() { X = 3, Y = 2, Z = 2 });
            this.Add(new Point3D() { X = 4, Y = 2, Z = 3 });
            this.Add(new Point3D() { X = 0, Y = 3, Z = 3 });
            this.Add(new Point3D() { X = 1, Y = 3, Z = 2 });
            this.Add(new Point3D() { X = 2, Y = 3, Z = 2 });
            this.Add(new Point3D() { X = 3, Y = 3, Z = 2 });
            this.Add(new Point3D() { X = 4, Y = 3, Z = 3 });
            this.Add(new Point3D() { X = 0, Y = 4, Z = 3 });
            this.Add(new Point3D() { X = 1, Y = 4, Z = 3 });
            this.Add(new Point3D() { X = 2, Y = 4, Z = 3 });
            this.Add(new Point3D() { X = 3, Y = 4, Z = 3 });
            this.Add(new Point3D() { X = 4, Y = 4, Z = 3 });
            this.ToAscii();
        }

        private void ToAscii()
        {
            List<string> lines = new List<string>();
            for (int ii = 0; ii < this.Count; ii++)
            {
                var dp = this[ii];
                int yy = (int)dp.Y;
                while (lines.Count <= yy)
                {
                    lines.Add("");
                }
                int xx = (int)dp.X;
                if (lines[yy].Length <= xx)
                {
                    lines[yy] = lines[yy].PadRight(xx + 1);
                }
                char[] chars = lines[yy].ToCharArray();
                chars[xx] = dp.Z.ToString()[0];
                lines[yy] = new string(chars);
            }
            foreach (string line in lines)
            {
                System.Diagnostics.Debug.WriteLine(line);
            }

        }
    }
    public class MixedData : Shape3D
    {
        public MixedData()
        {
            this.Add(new Point3D { X = -2, Y = 0.00002, Z = 400000 });
            this.Add(new Point3D { X = -4, Y = 0, Z = 200000 });
            this.Add(new Point3D { X = -4, Y = 0.00004, Z = 200000 });
            this.Add(new Point3D { X = 0, Y = 0, Z = 200000 });
            this.Add(new Point3D { X = 0, Y = 0.00004, Z = 200000 });

            // above points should render as a shape similar to a Pyramid shape:
            //this.Add(new Point3D { X = 2, Y = 2, Z = 4 });
            //this.Add(new Point3D { X = 4, Y = 0, Z = 0 });
            //this.Add(new Point3D { X = 4, Y = 4, Z = 0 });
            //this.Add(new Point3D { X = 0, Y = 0, Z = 0 });
            //this.Add(new Point3D { X = 0, Y = 4, Z = 0 });
        }
    }

    public class LogData : Shape3D
    {
        public LogData()
        {
            this.Add(new Point3D { X = 0, Y = 0, Z = 0 });
            this.Add(new Point3D { X = 0, Y = 100, Z = 0 });
            this.Add(new Point3D { X = 5, Y = 50, Z = 1000 });
            this.Add(new Point3D { X = 10, Y = 0, Z = 0 });
            this.Add(new Point3D { X = 10, Y = 100, Z = 0 });
        }
    }


}