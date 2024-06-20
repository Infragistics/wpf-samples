using System;
using System.Collections.Generic; 
using System.Windows; 

namespace IGShapeChart.Samples
{ 
    public class ScatterValueItem 
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double V { get; set; }
    }
    public class ScatterBubbleItem 
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double R { get; set; } 
    } 
    public class ScatterShapeItem   
    {
        public List<List<Point>> Points { get; set; }
    } 
    public class ScatterProvider
    {  
        public ScatterProvider()
        {   
            BubblePoints = new List<ScatterBubbleItem >();  
            ValuePoints = new List<ScatterValueItem >();
            ShapePoints = new List<ScatterShapeItem >();
            DensityPoints = new List<ScatterValueItem >(); 

            var xColumn = new double[] { 30, 30, 70, 70 };
            var yColumn = new double[] { 30, 60, 60, 30 };
            var vColumn = new double[] { 10, 10, 90, 90 };
            var rColumn = new double[] { 20, 20, 50, 50 }; 
            
            for (int i = 0; i < xColumn.Length; i++)
            {
                var x = xColumn[i];
                var y = yColumn[i];
                var r = rColumn[i];
                var v = vColumn[i];
                
                BubblePoints.Add(new ScatterBubbleItem { X = x, Y = y, R = r });
                ValuePoints.Add(new ScatterValueItem   { X = x, Y = y, V = v });
                ShapePoints.Add(new ScatterShapeItem { Points = GetShapePoints(x, y ) }); 
 
                DensityPoints.AddRange(GetDensityPoints(x, y)); 
            } 
        }
         
        public List<ScatterValueItem> ValuePoints { get; set; }  
        public List<ScatterShapeItem> ShapePoints { get; set; }  
        public List<ScatterValueItem> DensityPoints { get; set; } 
        public List<ScatterBubbleItem> BubblePoints { get; set; } 
         
        private static List<List<Point>> GetShapePoints(double x, double y, double width = 10, double height = 15)
        {
            var points = new List<Point>();
            
            points.Add(new Point { X = x - width, Y = y + height });
            points.Add(new Point { X = x - width, Y = y });
            points.Add(new Point { X = x + width, Y = y });
            points.Add(new Point { X = x + width, Y = y + height });

            return new List<List<Point>> { points };
        } 

        static Random Rand = new Random();
        private static List<ScatterValueItem> GetDensityPoints(double x, double y, int range = 10)
        {
            var points = new List<ScatterValueItem>();

            for (int p = 0; p < 1000; p++)
            {
                var dx = x + (Rand.Next(-range, range) * Rand.NextDouble());
                var dy = y + (Rand.Next(-range, range) * Rand.NextDouble());
                points.Add(new ScatterValueItem { X = dx, Y = dy });
            }
            range = range / 2;
            for (int p = 0; p < 1500; p++)
            {
                var dx = x + (Rand.Next(-range, range) * Rand.NextDouble());
                var dy = y + (Rand.Next(-range, range) * Rand.NextDouble());
                points.Add(new ScatterValueItem { X = dx, Y = dy });
            }

            return points;
        }
    }

}
