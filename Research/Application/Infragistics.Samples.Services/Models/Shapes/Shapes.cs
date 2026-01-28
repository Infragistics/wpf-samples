using System;
using System.Collections.Generic;
using System.Windows; 

namespace Infragistics.Models
{
    public class Shapes : List<Shape3D>
    {
        public Shapes()
        {
            this.Add(new Trapezoid());
            this.Add(new TrapezoidFlippedXYZ());
            this.Add(new TrapezoidFlippedXY());
            this.Add(new TrapezoidFlippedX());
            this.Add(new TrapezoidFlippedY());
            this.Add(new TrapezoidFlippedYZ());
            this.Add(new TrapezoidFlippedZ());
            this.Add(new TrapezoidTwinsX());
            this.Add(new TrapezoidTwinsY());
            this.Add(new TrapezoidTwinsZ());
            
            this.Add(new Pyramid());
            this.Add(new PyramidFlippedXYZ());
            this.Add(new PyramidFlippedXY());
            this.Add(new PyramidFlippedX());
            this.Add(new PyramidFlippedY());
            this.Add(new PyramidFlippedYZ());
            this.Add(new PyramidFlippedZ());
            this.Add(new PyramidTwinsX());
            this.Add(new PyramidTwinsY());
            this.Add(new PyramidTwinsZ());
            this.Add(new PyramidWithDoubles()); 
          
            this.Add(new PyramidWithNaN());
            this.Add(new PyramidWithNull());
            this.Add(new PyramidWithInfinity());
            this.Add(new PyramidWithMixedValues());
          
            this.Add(new Tetrahedron());
            this.Add(new TetrahedronFlippedXYZ());
            this.Add(new TetrahedronFlippedXY());
            this.Add(new TetrahedronFlippedX());
            this.Add(new TetrahedronFlippedY());
            this.Add(new TetrahedronFlippedYZ());
            this.Add(new TetrahedronFlippedZ());
            this.Add(new TetrahedronTwinsX());
            this.Add(new TetrahedronTwinsY());
           
            this.Add(new Cone());
            this.Add(new ConeFlippedXYZ());
            this.Add(new ConeFlippedXY());
            this.Add(new ConeFlippedX());
            this.Add(new ConeFlippedY());
            this.Add(new ConeFlippedYZ());
            this.Add(new ConeFlippedZ());
            this.Add(new ConeTwinsX());
            this.Add(new ConeTwinsY());
            this.Add(new ConeTwinsZ());
          
            this.Add(new ConeSlice());
            this.Add(new ConeSliceFlippedXYZ());
            this.Add(new ConeSliceFlippedXY());
            this.Add(new ConeSliceFlippedX());
            this.Add(new ConeSliceFlippedY());
            this.Add(new ConeSliceFlippedYZ());
            this.Add(new ConeSliceFlippedZ());
            this.Add(new ConeSliceTwinsX());
            this.Add(new ConeSliceTwinsY());
            this.Add(new ConeSliceTwinsZ());
          
            this.Add(new PlainFlatZ());
            this.Add(new PlainFlatX());
            this.Add(new PlainFlatY());
            this.Add(new PlainAtAngle());
            this.Add(new PlainAtAngleFlippedXYZ());
            this.Add(new PlainAtAngleFlippedXY());
            this.Add(new PlainAtAngleFlippedX());
            this.Add(new PlainAtAngleFlippedY());
            this.Add(new PlainAtAngleFlippedYZ());
            this.Add(new PlainAtAngleFlippedZ());

            this.Add(new PlainUnevenRangeX());
           
            

            this.Add(new Cube());
            this.Add(new Cylinder());
         
        }

    }
    public class PlainAtAngle : Shape3D
    {
        public PlainAtAngle()
        {
            this.Add(new ShapePoint3D { X = 40, Y = 0, Z = 100000 });
            this.Add(new ShapePoint3D { X = 40, Y = 4, Z = 300000 });
            this.Add(new ShapePoint3D { X = 0, Y = 4,  Z = 500000 });
            this.Add(new ShapePoint3D { X = 0, Y = 0,  Z = 300000 });
        }
    }
    public class PlainFlatX : Shape3D
    {
        public PlainFlatX()
        {
            this.Add(new ShapePoint3D { X = 20, Y = 0, Z = 200000 });
            this.Add(new ShapePoint3D { X = 20, Y = 4, Z = 200000 });
            this.Add(new ShapePoint3D { X = 20, Y = 2, Z = 400000 }); 
        }
    }
    public class PlainAtAngleFlippedX : PlainAtAngle
    {
        public PlainAtAngleFlippedX() { FlipX(); }
    }
    public class PlainAtAngleFlippedY : PlainAtAngle
    {
        public PlainAtAngleFlippedY() { FlipY(); }
    }
    public class PlainAtAngleFlippedZ : PlainAtAngle
    {
        public PlainAtAngleFlippedZ() { FlipZ(); }
    }
    public class PlainAtAngleFlippedYZ : PlainAtAngle
    {
        public PlainAtAngleFlippedYZ() { FlipY(); FlipZ(); }
    }
    public class PlainAtAngleFlippedXY : PlainAtAngle
    {
        public PlainAtAngleFlippedXY() { FlipX(); FlipY(); }
    }
    public class PlainAtAngleFlippedXYZ : PlainAtAngle
    {
        public PlainAtAngleFlippedXYZ() { FlipX(); FlipY(); FlipZ(); }
    }
    public class PlainUnevenRangeX : Shape3D
    {
        public PlainUnevenRangeX()
        {
            this.Add(new ShapePoint3D { X = 0, Y = 4, Z = 0 });
            this.Add(new ShapePoint3D { X = 0, Y = 0, Z = 0 });
            this.Add(new ShapePoint3D { X = 2.5, Y = 0, Z = 5 });
            this.Add(new ShapePoint3D { X = 2.5, Y = 4, Z = 5 });
            this.Add(new ShapePoint3D { X = 5, Y = 0, Z = 10 });
            this.Add(new ShapePoint3D { X = 5, Y = 4, Z = 10 });
        }
    }
    public class PlainFlatZ : Shape3D
    {
        public PlainFlatZ()
        {
            this.Add(new ShapePoint3D { X = 40, Y = 0, Z = 200000 });
            this.Add(new ShapePoint3D { X = 40, Y = 4, Z = 200000 });
            this.Add(new ShapePoint3D { X = 0, Y = 4, Z = 200000 });
            this.Add(new ShapePoint3D { X = 0, Y = 0, Z = 200000 });
        }
    }
    public class PlainFlatY : Shape3D
    {
        public PlainFlatY()
        {
            this.Add(new ShapePoint3D { Y = 20, X = 0, Z = 200000 });
            this.Add(new ShapePoint3D { Y = 20, X = 4, Z = 200000 });
            this.Add(new ShapePoint3D { Y = 20, X = 2, Z = 400000 });
        }
    }
    public class Pyramid : Shape3D
    {
        public Pyramid()
        {
            this.Add(new ShapePoint3D { X = 20, Y = 2, Z = 400000 });
            this.Add(new ShapePoint3D { X = 40, Y = 0, Z = 0 });
            this.Add(new ShapePoint3D { X = 40, Y = 4, Z = 0 });
            this.Add(new ShapePoint3D { X = 0, Y = 0, Z = 0 });
            this.Add(new ShapePoint3D { X = 0, Y = 4, Z = 0 });
        }
    }
    public class PyramidWithDoubles : Shape3D
    {
        public PyramidWithDoubles()
        {
            this.Add(new ShapePoint3D { X = 20.5, Y = 2.25, Z = 400000.5 });
            this.Add(new ShapePoint3D { X = 40.5, Y = 0.25, Z = 0.5 });
            this.Add(new ShapePoint3D { X = 40.5, Y = 4.25, Z = 0.5 });
            this.Add(new ShapePoint3D { X = 0.5,  Y = 0.25, Z = 0.5 });
            this.Add(new ShapePoint3D { X = 0.5,  Y = 4.25, Z = 0.5 });
        }
    }
    /// <summary>
    /// Represents 3d shape with double.NaN points between valid points of a pyramid
    /// </summary>
    public class PyramidWithNaN : Shape3D
    {
        public PyramidWithNaN()
        {
            this.Add(new ShapePoint3D { X = 20, Y = 2, Z = 400000 });
            this.Add(new ShapePoint3D { X = double.NaN, Y = 0, Z = 0 });
            this.Add(new ShapePoint3D { X = 40, Y = 0, Z = 0 });
            this.Add(new ShapePoint3D { X = double.NaN, Y = double.NaN, Z = 0 });
            this.Add(new ShapePoint3D { X = 40, Y = 4, Z = 0 });
            this.Add(new ShapePoint3D { X = double.NaN, Y = double.NaN, Z = double.NaN });
            this.Add(new ShapePoint3D { X = 0, Y = 0, Z = 0 });
            this.Add(new ShapePoint3D { X = 0, Y = double.NaN, Z = double.NaN });
            this.Add(new ShapePoint3D { X = 0, Y = 4, Z = 0 });
        }
    }
    /// <summary>
    /// Represents 3d shape with null points between valid points of a pyramid
    /// </summary>
    public class PyramidWithNull : Shape3D
    {
        public PyramidWithNull()
        {
            this.Add(new ShapePoint3D { X = 20, Y = 2, Z = 400000 });
            this.Add(null);
            this.Add(new ShapePoint3D { X = 40, Y = 0, Z = 0 });
            this.Add(null);
            this.Add(new ShapePoint3D { X = 40, Y = 4, Z = 0 });
            this.Add(null);
            this.Add(new ShapePoint3D { X = 0, Y = 0, Z = 0 });
            this.Add(null);
            this.Add(new ShapePoint3D { X = 0, Y = 4, Z = 0 });
        }
    }
    /// <summary>
    /// Represents 3d shape with double infinity points between valid points of a pyramid
    /// </summary>
    public class PyramidWithInfinity : Shape3D
    {
        public PyramidWithInfinity()
        {
            this.Add(new ShapePoint3D { X = 20, Y = 2, Z = 400000 });
            this.Add(new ShapePoint3D { X = double.PositiveInfinity, Y = 0, Z = 0 });
            this.Add(new ShapePoint3D { X = 40, Y = 0, Z = 0 });
            this.Add(new ShapePoint3D { X = double.NegativeInfinity, Y = double.PositiveInfinity, Z = 0 });
            this.Add(new ShapePoint3D { X = 40, Y = 4, Z = 0 });
            this.Add(new ShapePoint3D { X = double.NegativeInfinity, Y = double.NegativeInfinity, Z = double.PositiveInfinity });
            this.Add(new ShapePoint3D { X = 0, Y = 0, Z = 0 });
            this.Add(new ShapePoint3D { X = 0, Y = double.PositiveInfinity, Z = double.NegativeInfinity });
            this.Add(new ShapePoint3D { X = 0, Y = 4, Z = 0 });
        }
    }
    /// <summary>
    /// Represents 3d shape with double infinity, Nan, and null points between valid points of a pyramid
    /// </summary>
    public class PyramidWithMixedValues : Shape3D
    {
        public PyramidWithMixedValues()
        {
            this.Add(new ShapePoint3D { X = 20, Y = 2, Z = 400000 });
            this.Add(null);
            this.Add(new ShapePoint3D { X = double.NaN, Y = 0, Z = 0 });
            this.Add(new ShapePoint3D { X = 40, Y = 0, Z = 0 });
            this.Add(new ShapePoint3D { X = double.NegativeInfinity, Y = double.PositiveInfinity, Z = 0 });
            this.Add(null);
            this.Add(new ShapePoint3D { X = 40, Y = 4, Z = 0 });
            this.Add(new ShapePoint3D { X = double.NaN, Y = double.NegativeInfinity, Z = double.PositiveInfinity });
            this.Add(null);
            this.Add(new ShapePoint3D { X = 0, Y = 0, Z = 0 });
            this.Add(null);
            this.Add(new ShapePoint3D { X = 0, Y = double.PositiveInfinity, Z = double.NaN });
            this.Add(new ShapePoint3D { X = 0, Y = 4, Z = 0 });
        }
    }
    public class PyramidTwinsZ : Shape3D
    {
        public PyramidTwinsZ()
        {
            var twin1 = new Pyramid();
            var twin2 = new Pyramid().FlipZ();
              
            this.AddRange(twin1);
            this.AddRange(twin2);
        }
    }
    public class PyramidTwinsY : Shape3D
    {
        public PyramidTwinsY()
        {
            var twin1 = new Pyramid();
            var twin2 = new Pyramid().FlipY(); 

            this.AddRange(twin1);
            this.AddRange(twin2);
        }
    }
    public class PyramidTwinsX : Shape3D
    {
        public PyramidTwinsX()
        {
            var twin1 = new Pyramid();
            var twin2 = new Pyramid().FlipX(); 

            this.AddRange(twin1);
            this.AddRange(twin2);
        }
    }
    public class PyramidFlippedXYZ : Pyramid
    {
        public PyramidFlippedXYZ() {  FlipX(); FlipY(); FlipZ(); }
    }
    public class PyramidFlippedZ : Pyramid
    {
        public PyramidFlippedZ() { FlipZ(); }
    }
    public class PyramidFlippedY : Pyramid
    {
        public PyramidFlippedY() { FlipY(); }
    }
    public class PyramidFlippedX : Pyramid
    {
        public PyramidFlippedX() { FlipX(); }
    }
    public class PyramidFlippedYZ : Pyramid
    {
        public PyramidFlippedYZ() { FlipY(); FlipZ(); }
    }
    public class PyramidFlippedXZ : Pyramid
    {
        public PyramidFlippedXZ() {  FlipX(); FlipZ(); }
    }
    public class PyramidFlippedXY : Pyramid
    {
        public PyramidFlippedXY() { FlipX(); FlipY(); }
    }

    public class Tetrahedron : Shape3D
    {
        public Tetrahedron() 
        {
            this.Add(new ShapePoint3D { X = 20, Y = 2, Z = 400000 });

            this.Add(new ShapePoint3D { X = 0, Y = 0, Z = 100000 });
            this.Add(new ShapePoint3D { X = 40, Y = 0, Z = 100000 });
            this.Add(new ShapePoint3D { X = 20, Y = 4, Z = 100000 });
        }
    }
    public class TetrahedronTwinsZ : Shape3D
    {
        public TetrahedronTwinsZ()
        {
            var twin1 = new Tetrahedron();
            var twin2 = new Tetrahedron().FlipZ();

            this.AddRange(twin1);
            this.AddRange(twin2);
        }
    }
    public class TetrahedronTwinsY : Shape3D
    {
        public TetrahedronTwinsY()
        {
            var twin1 = new Tetrahedron();
            var twin2 = new Tetrahedron().FlipY();

            this.AddRange(twin1);
            this.AddRange(twin2);
        }
    }
    public class TetrahedronTwinsX : Shape3D
    {
        public TetrahedronTwinsX()
        {
            var twin1 = new Tetrahedron();
            var twin2 = new Tetrahedron().FlipX();

            this.AddRange(twin1);
            this.AddRange(twin2);
        }
    }

    public class TetrahedronFlippedXYZ : Tetrahedron
    {
        public TetrahedronFlippedXYZ() { FlipX(); FlipY(); FlipZ(); }
    }
    public class TetrahedronFlippedZ : Tetrahedron
    {
        public TetrahedronFlippedZ() { FlipZ(); }
    }
    public class TetrahedronFlippedY : Tetrahedron
    {
        public TetrahedronFlippedY() { FlipY(); }
    }
    public class TetrahedronFlippedX : Tetrahedron
    {
        public TetrahedronFlippedX() { FlipX(); }
    }
    public class TetrahedronFlippedYZ : Tetrahedron
    {
        public TetrahedronFlippedYZ() { FlipY(); FlipZ(); }
    }
    public class TetrahedronFlippedXZ : Tetrahedron
    {
        public TetrahedronFlippedXZ() { FlipX(); FlipZ(); }
    }
    public class TetrahedronFlippedXY : Tetrahedron
    {
        public TetrahedronFlippedXY() { FlipX(); FlipY(); }
    }

    public class Trapezoid : Shape3D
    {
        public Trapezoid()
        {
            this.Add(new ShapePoint3D { X = 30, Y = 2, Z = 400000 });
            this.Add(new ShapePoint3D { X = 30, Y = 3, Z = 400000 });
            this.Add(new ShapePoint3D { X = 20, Y = 3, Z = 400000 });
            this.Add(new ShapePoint3D { X = 20, Y = 2, Z = 400000 });

            this.Add(new ShapePoint3D { X = 40, Y = 1, Z = 100000 });
            this.Add(new ShapePoint3D { X = 40, Y = 4, Z = 100000 });
            this.Add(new ShapePoint3D { X = 10, Y = 4, Z = 100000 });
            this.Add(new ShapePoint3D { X = 10, Y = 1, Z = 100000 });
        }
    }
    public class TrapezoidTwinsZ : Shape3D
    {
        public TrapezoidTwinsZ()
        {
            var twin1 = new Trapezoid();
            var twin2 = new Trapezoid().FlipZ();

            this.AddRange(twin1);
            this.AddRange(twin2);
        }
    }
    public class TrapezoidTwinsY : Shape3D
    {
        public TrapezoidTwinsY()
        {
            var twin1 = new Trapezoid();
            var twin2 = new Trapezoid().FlipY();

            this.AddRange(twin1);
            this.AddRange(twin2);
        }
    }
    public class TrapezoidTwinsX : Shape3D
    {
        public TrapezoidTwinsX()
        {
            var twin1 = new Trapezoid();
            var twin2 = new Trapezoid().FlipX();

            this.AddRange(twin1);
            this.AddRange(twin2);
        }
    }

    public class TrapezoidFlippedXYZ : Trapezoid
    {
        public TrapezoidFlippedXYZ() { FlipX(); FlipY(); FlipZ(); }
    }
    public class TrapezoidFlippedZ : Trapezoid
    {
        public TrapezoidFlippedZ() { FlipZ(); }
    }
    public class TrapezoidFlippedY : Trapezoid
    {
        public TrapezoidFlippedY() { FlipY(); }
    }
    public class TrapezoidFlippedX : Trapezoid
    {
        public TrapezoidFlippedX() { FlipX(); }
    }
    public class TrapezoidFlippedYZ : Trapezoid
    {
        public TrapezoidFlippedYZ() { FlipY(); FlipZ(); }
    }
    public class TrapezoidFlippedXZ : Trapezoid
    {
        public TrapezoidFlippedXZ() { FlipX(); FlipZ(); }
    }
    public class TrapezoidFlippedXY : Trapezoid
    {
        public TrapezoidFlippedXY() { FlipX(); FlipY(); }
    }

    public class Cone : Shape3D
    {
        public Cone(int points = 10)
        {
            var radius = 4;
            var center = new Point(radius / 2.0, radius / 2.0);
            Generate(center, radius, 1, points);
            this.Add(new ShapePoint3D { X = radius / 2.0, Y = radius / 2.0, Z = 4 });

        }
        public void Generate(Point center, double radius, double z, int points = 10)
        {
            var interval = 360.0 / points;
            for (var i = 0.0; i <= 360.0; i += interval)
            {
                var p = GetPoint(i, radius / 2.0, center);
                this.Add(new ShapePoint3D { X = p.X, Y = p.Y, Z = z });
            }
        }

        private Point GetPoint(double angle, double distance, Point centre)
        {
            var x = (float)(distance * Math.Cos(angle)) + centre.X;
            var y = (float)(distance * Math.Sin(angle)) + centre.Y;
            return new Point(x, y);
        }
    }
    public class ConeTwinsZ : Shape3D
    {
        public ConeTwinsZ()
        {
            var twin1 = new Cone();
            var twin2 = new Cone().FlipZ();

            this.AddRange(twin1);
            this.AddRange(twin2);
        }
    }
    public class ConeTwinsY : Shape3D
    {
        public ConeTwinsY()
        {
            var twin1 = new Cone();
            var twin2 = new Cone().FlipY();

            this.AddRange(twin1);
            this.AddRange(twin2);
        }
    }
    public class ConeTwinsX : Shape3D
    {
        public ConeTwinsX()
        {
            var twin1 = new Cone();
            var twin2 = new Cone().FlipX();

            this.AddRange(twin1);
            this.AddRange(twin2);
        }
    }

    public class ConeFlippedXYZ : Cone
    {
        public ConeFlippedXYZ() { FlipX(); FlipY(); FlipZ(); }
    }
    public class ConeFlippedZ : Cone
    {
        public ConeFlippedZ() { FlipZ(); }
    }
    public class ConeFlippedY : Cone
    {
        public ConeFlippedY() { FlipY(); }
    }
    public class ConeFlippedX : Cone
    {
        public ConeFlippedX() { FlipX(); }
    }
    public class ConeFlippedYZ : Cone
    {
        public ConeFlippedYZ() { FlipY(); FlipZ(); }
    }
    public class ConeFlippedXZ : Cone
    {
        public ConeFlippedXZ() { FlipX(); FlipZ(); }
    }
    public class ConeFlippedXY : Cone
    {
        public ConeFlippedXY() { FlipX(); FlipY(); }
    }

    public class ConeSlice : Cone
    {
        public ConeSlice()
        {
            Clear();
            var radius = 4;
            var center = new Point(radius / 2.0, radius / 2.0);
            Generate(center, radius, 1);
            radius = 2;
            Generate(center, radius, 4);
            this.Add(new ShapePoint3D { X = 2, Y = 2, Z = 4 });
        }
    }
    public class ConeSliceTwinsZ : Shape3D
    {
        public ConeSliceTwinsZ()
        {
            var twin1 = new ConeSlice();
            var twin2 = new ConeSlice().FlipZ();

            this.AddRange(twin1);
            this.AddRange(twin2);
        }
    }
    public class ConeSliceTwinsY : Shape3D
    {
        public ConeSliceTwinsY()
        {
            var twin1 = new ConeSlice();
            var twin2 = new ConeSlice().FlipY();

            this.AddRange(twin1);
            this.AddRange(twin2);
        }
    }
    public class ConeSliceTwinsX : Shape3D
    {
        public ConeSliceTwinsX()
        {
            var twin1 = new ConeSlice();
            var twin2 = new ConeSlice().FlipX();

            this.AddRange(twin1);
            this.AddRange(twin2);
        }
    }
    public class ConeSliceFlippedXYZ : ConeSlice
    {
        public ConeSliceFlippedXYZ() { FlipX(); FlipY(); FlipZ(); }
    }
    public class ConeSliceFlippedZ : ConeSlice
    {
        public ConeSliceFlippedZ() { FlipZ(); }
    }
    public class ConeSliceFlippedY : ConeSlice
    {
        public ConeSliceFlippedY() { FlipY(); }
    }
    public class ConeSliceFlippedX : ConeSlice
    {
        public ConeSliceFlippedX() { FlipX(); }
    }
    public class ConeSliceFlippedYZ : ConeSlice
    {
        public ConeSliceFlippedYZ() { FlipY(); FlipZ(); }
    }
    public class ConeSliceFlippedXZ : ConeSlice
    {
        public ConeSliceFlippedXZ() { FlipX(); FlipZ(); }
    }
    public class ConeSliceFlippedXY : ConeSlice
    {
        public ConeSliceFlippedXY() { FlipX(); FlipY(); }
    } 
    public class Cylinder : Cone
    {
        public Cylinder()
        {
            Clear();
            var radius = 4;
            var center = new Point(radius / 2.0, radius / 2.0);
            Generate(center, radius, 2); 
            Generate(center, radius, 4); 
        }
    }
    public class Cube : Shape3D
    {
        public Cube()
        { 

            this.Add(new ShapePoint3D { X = 4, Y = 0, Z = 0 });
            this.Add(new ShapePoint3D { X = 4, Y = 4, Z = 0 });
            this.Add(new ShapePoint3D { X = 0, Y = 4, Z = 0 });
            this.Add(new ShapePoint3D { X = 0, Y = 0, Z = 0 });

            this.Add(new ShapePoint3D { X = 4, Y = 0, Z = 4 });
            this.Add(new ShapePoint3D { X = 4, Y = 4, Z = 4 });
            this.Add(new ShapePoint3D { X = 0, Y = 4, Z = 4 });
            this.Add(new ShapePoint3D { X = 0, Y = 0, Z = 4 }); 
    
        }

    }

     

}