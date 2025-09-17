using System; 
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Infragistics.Framework
{
    public class ScatterData : List<Point2D>
    {
        public ScatterData()
        {     
            var value = 100.0;
            for (var i = 0; i <= 50; i++)
            { 
                var sine = System.Math.Sin(i * 30.0 / 180.0 * System.Math.PI);

                var item = new Point2D();
                item.Label = "L" + i;
                item.Y = sine;

                var increment = Random.NextDouble();
                if (increment <= 0.5)
                {
                    value -= (increment * 5);
                }
                else
                {
                    value += (increment * 5);
                }

                this.Add(item);
            } 
        }
        internal static Random Random = new Random();

        public static List<Point2D> GetBodyMassIndex(
            double heightMin, double heightMax, double massRange, double bmi)
        {
            var data = new List<Point2D>();
            for (var i = 0; i < 100; i++)
            {
                var height = (Random.NextDouble() * (heightMax - heightMin)) + heightMin;
                //r.nextInt(High-Low) + Low;
                var mass = bmi * height * height;
                var massMin = mass - massRange;
                var massMax = mass + massRange;
                mass = (Random.NextDouble() * (massMax - massMin)) + massMin;

                var newItem = new Point2D();
                newItem.X = mass;
                newItem.Y = height;
                data.Add(newItem);
            }
            data = data.SortByProperty("Y").ToList();

            return data;
        }

        /// <summary>
        /// Gets wind scatter data where x is wind direction and y wind is speed
        /// </summary>  
        public static List<Point2D> GetWindData(double offset = 1.0)
        {
            if (offset == 0) offset = 1.0;

            var data = new List<Point2D>();
            data.Add(new Point2D(0, 10 * offset));
            data.Add(new Point2D(45, 15 * offset));

            data.Add(new Point2D(90, 10 * offset));
            data.Add(new Point2D(135, 5 * offset));

            data.Add(new Point2D(180, 10 * offset));
            data.Add(new Point2D(225, 25 * offset));

            data.Add(new Point2D(270, 10 * offset));
            data.Add(new Point2D(315, 30 * offset));
            data.Add(new Point2D(360, 10 * offset));

            //data.sortByY();
            return data;
        }

    }

    public abstract class ScatterDataBase : List<Point2D>
    {

        protected ScatterDataBase(int xCount = 10)
            : this(-20, 20, xCount)
        {
        }

        protected ScatterDataBase(double xMin, double xMax, int xCount = 10)
        { 
            var xStep = (xMax - xMin) / (xCount - 1);  
            for (var x = xMin; x <= xMax; x += xStep)
            {
                this.Add(new Point2D { X = x, Y = this.Compute(x) });
            }
            Debug.WriteLine("Data points = " + this.Count);
        }
        protected abstract double Compute(double x);
    }

    public class BubbleDataPoint
    {
        public string Label { get; set; }
        public double Radius { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }
    public class BubbleDataSource : List<BubbleDataPoint>
    {
        public static Random Rand = new Random();
        public BubbleDataSource()
        {
            int value = 50;
            for (int i = 0; i < 100; i++)
            {
                double change = Rand.NextDouble();
                if (change > .5)
                {
                    value += (int)(change * 20);
                }
                else
                {
                    value -= (int)(change * 20);
                }
                value %= 100;
                this.Add(new BubbleDataPoint
                {
                    Label = "Item " + i.ToString(),
                    Radius = Rand.Next(10, 50),
                    X = Rand.Next(i, i + 5),
                    Y = Rand.Next(value - 50, value + 50)
                });
            }
        }
    }
}