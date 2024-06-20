using IGSurfaceChart.Samples.Models;
using System;
using System.Collections.Generic;
using System.Windows;

namespace IGSurfaceChart.Samples.ViewModels
{
    public class Data
    {
        internal static List<DataPoint3D> GenerateSimpleData()
        {
            var DataPoint3Ds = new List<DataPoint3D>
                                 {
                                     new DataPoint3D() {X = 0, Y = 0, Z = 3},
                                     new DataPoint3D() {X = 1, Y = 0, Z = 3},
                                     new DataPoint3D() {X = 2, Y = 0, Z = 3},
                                     new DataPoint3D() {X = 3, Y = 0, Z = 3},
                                     new DataPoint3D() {X = 4, Y = 0, Z = 3},
                                     new DataPoint3D() {X = 0, Y = 1, Z = 3},
                                     new DataPoint3D() {X = 1, Y = 1, Z = 2},
                                     new DataPoint3D() {X = 2, Y = 1, Z = 2},
                                     new DataPoint3D() {X = 3, Y = 1, Z = 2},
                                     new DataPoint3D() {X = 4, Y = 1, Z = 3},
                                     new DataPoint3D() {X = 0, Y = 2, Z = 3},
                                     new DataPoint3D() {X = 1, Y = 2, Z = 2},
                                     new DataPoint3D() {X = 2, Y = 2, Z = 1},
                                     new DataPoint3D() {X = 3, Y = 2, Z = 2},
                                     new DataPoint3D() {X = 4, Y = 2, Z = 3},
                                     new DataPoint3D() {X = 0, Y = 3, Z = 3},
                                     new DataPoint3D() {X = 1, Y = 3, Z = 2},
                                     new DataPoint3D() {X = 2, Y = 3, Z = 2},
                                     new DataPoint3D() {X = 3, Y = 3, Z = 2},
                                     new DataPoint3D() {X = 4, Y = 3, Z = 3},
                                     new DataPoint3D() {X = 0, Y = 4, Z = 3},
                                     new DataPoint3D() {X = 1, Y = 4, Z = 3},
                                     new DataPoint3D() {X = 2, Y = 4, Z = 3},
                                     new DataPoint3D() {X = 3, Y = 4, Z = 3},
                                     new DataPoint3D() {X = 4, Y = 4, Z = 3}
                                 };

            return DataPoint3Ds;
        }

        //  z = 8*x^2 - 4*y^4  
        // -2 <= x <=2 | -2 <= y <=2
        internal static List<DataPoint3D> GenerateFormula1Data()
        {
            var data = new List<DataPoint3D>();

            for (double x = -2; x <= 2; x += 0.5)
            {
                for (double y = -2; y <= 2; y += 0.5)
                {
                    double z = 8 * Math.Pow(x, 2.00) - 4 * Math.Pow(y, 4.00);
                    var point = new DataPoint3D(x, y, z);
                    data.Add(point);
                }
            }

            return data;
        }

        //  z = 1-cos(x^2 + y^2)/x^2 + y^2
        // -3 <= x <= 3 | -3 <= y <= 3
        internal static List<DataPoint3D> GenerateFormula2Data()
        {
            var data = new List<DataPoint3D>();

            for (double x = -3; x <= 3; x += 0.5)
            {
                for (double y = -3; y <= 3; y += 0.5)
                {
                    double z = Math.Sin(y * y + x * x) / Math.Sqrt(x * x + y * y + .0001);

                    var point = new DataPoint3D(x, y, z);
                    data.Add(point);
                }
            }

            return data;
        }

        // sin(x-y)*y*cos(x),(x,-3,3),(y,-3,3)
        internal static List<DataPoint3D> GenerateFormula3Data()
        {
            var data = new List<DataPoint3D>();

            for (double x = -3; x <= 3; x += 0.5)
            {
                for (double y = -3; y <= 3; y += 0.5)
                {
                    double z = Math.Sin(x - y)*y* Math.Cos(x);

                    var point = new DataPoint3D(x, y, z);
                    data.Add(point);
                }
            }

            return data;
        }

        internal static List<DataPoint3D> GenerateFormula4Data()
        {
            var data = new List<DataPoint3D>();

            for (int x = -20; x <= 20; x += 2)
            {
                for (int y = -20; y <= 20; y += 2)
                {
                    double z = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));

                    var point = new DataPoint3D(x, y, z);
                    data.Add(point);
                }
            }

            return data;
        }

        // z = math.exp(x/5)*math.sin(y)
        // -5 <= x <= 5 | -5 <= y <= 5
        internal static List<DataPoint3D> GenerateFormula5Data()
        {
            var data = new List<DataPoint3D>();

            for (double x = -5; x <= 5; x += 0.5)
            {
                for (double y = -5; y <= 5; y += 0.5)
                {
                    double z = Math.Exp(x / 5.0) * Math.Sin(y);

                    var point = new DataPoint3D(x, y, z);
                    data.Add(point);
                }
            }

            return data;
        }

        // z = -x^2 -y^2 + 6
        // -4 <= x < =4
        // -4 <= y < =4
        internal static List<DataPoint3D> GenerateFormula6Data()
        {
            var data = new List<DataPoint3D>();

            for (double x = -4; x <= 4; x += 0.5)
            {
                for (double y = -4; y <= 4; y += 0.5)
                {
                    double z = - Math.Pow(x, 2) - Math.Pow(y, 2) + 6;

                    var point = new DataPoint3D(x, y, z);
                    data.Add(point);
                }
            }

            return data;
        }

        internal static List<DataPoint3D> GenerateCone()
        {
            var data = new List<DataPoint3D>();

            var radius = 4;
            var center = new Point(radius / 2.0, radius / 2.0);
            data = Generate(center, radius, 0);
            data.Add(new DataPoint3D { X = radius / 2.0, Y = radius / 2.0, Z = 4 });

            return data;
        }
        internal static List<DataPoint3D> GenerateConeFlipped()
        {
            List<DataPoint3D> data = GenerateCone();

            data = FlipZ(data);

            return data;
        }

        internal static List<DataPoint3D> Generate(Point center, double radius, double z)
        {
            List<DataPoint3D> data = new List<DataPoint3D>();
            for (int i = 0; i < 360; i += 10)
            {
                var p = GetPoint(i, radius / 2.0, center);
                data.Add(new DataPoint3D { X = p.X, Y = p.Y, Z = z });
            }

            return data;
        }

        internal static Point GetPoint(double angle, double distance, Point centre)
        {
            var x = (float)(distance * Math.Cos(angle)) + centre.X;
            var y = (float)(distance * Math.Sin(angle)) + centre.Y;
            return new Point(x, y);
        }

        internal static List<DataPoint3D> FlipZ(List<DataPoint3D> data)
        {
            for (var i = 0; i < data.Count; i++)
            {
                var item = data[i];
                item.Z *= -1;
                data[i] = item;
            }

            return data;
        }

        internal static List<DataPoint3D> GenerateConeSlice()
        {
            List<DataPoint3D> data = new List<DataPoint3D>();

            var radius = 4;
            var center = new Point(radius / 2.0, radius / 2.0);
            data = Generate(center, radius, 0);
            radius = 2;
            data.AddRange(Generate(center, radius, 4));
            data.Add(new DataPoint3D { X = 2, Y = 2, Z = 4 });

            return data;
        }

        internal static List<DataPoint3D> GenerateConeSliceFlipped()
        {
            List<DataPoint3D> data = GenerateConeSlice();

            data = FlipZ(data);

            return data;
        }

        internal static List<DataPoint3D> Generate_XSqPlusYSq()
        {
            List<DataPoint3D> data = new List<DataPoint3D>();

            for (double x = -20; x <= 20; x += 2)
            {
                for (double y = -20; y <= 20; y += 2)
                {
                    data.Add(new DataPoint3D() { X = x, Y = y, Z = Math.Pow(x, 2) + Math.Pow(y, 2) });
                }
            }

            return data;
        }

        internal static List<DataPoint3D> Generate_XCuPlusYCu()
        {
            List<DataPoint3D> data = new List<DataPoint3D>();

            for (double x = -20; x <= 20; x += 2)
            {
                for (double y = -20; y <= 20; y += 2)
                {
                    data.Add(new DataPoint3D() { X = x, Y = y, Z = Math.Pow(x, 3) + Math.Pow(y, 3) });
                }
            }

            return data;
        }

        internal static List<DataPoint3D> Generate_YPlusSinXSqPlus3Y()
        {
            List<DataPoint3D> data = new List<DataPoint3D>();

            for (double x = -20; x <= 20; x += 2)
            {
                for (double y = -20; y <= 20; y += 2)
                {
                    data.Add(new DataPoint3D() { X = x, Y = y, Z = y + Math.Sin(Math.Pow(x, 2) + 3 * y) });
                }
            }

            return data;
        }
    }
}
