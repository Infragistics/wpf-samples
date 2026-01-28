using System;
using System.Collections.Generic;
using System.Linq;
 
namespace Infragistics.Samples.Shared.Models
{
    public class SampleDataModel
    {
        
    }
    public class SampleStockMarketDataCollection : StockMarketDataCollection
    {
        public SampleStockMarketDataCollection()
        {
            this.Generate();
        }

    }
    public class SamplePolarDataCollection : PolarDataCollection
    {
        public SamplePolarDataCollection()
        {
            this.Add(new PolarDataPoint { Angle = 0, Radius = 80 });
            this.Add(new PolarDataPoint { Angle = 30, Radius = 60 });
            this.Add(new PolarDataPoint { Angle = 45, Radius = 40 });
            this.Add(new PolarDataPoint { Angle = 60, Radius = 60 });

            this.Add(new PolarDataPoint { Angle = 90, Radius = 80 });
            this.Add(new PolarDataPoint { Angle = 120, Radius = 60 });
            this.Add(new PolarDataPoint { Angle = 135, Radius = 40 });
            this.Add(new PolarDataPoint { Angle = 150, Radius = 60 });

            this.Add(new PolarDataPoint { Angle = 180, Radius = 20 });
            this.Add(new PolarDataPoint { Angle = 210, Radius = 60 });
            this.Add(new PolarDataPoint { Angle = 225, Radius = 40 });
            this.Add(new PolarDataPoint { Angle = 240, Radius = 60 });

            this.Add(new PolarDataPoint { Angle = 270, Radius = 60 });
            this.Add(new PolarDataPoint { Angle = 300, Radius = 60 });
            this.Add(new PolarDataPoint { Angle = 315, Radius = 40 });
            this.Add(new PolarDataPoint { Angle = 330, Radius = 60 });
            this.Add(new PolarDataPoint { Angle = 360, Radius = 80 });
        }
    }
    //public class SampleRadialDataCollection : RadialDataCollection
    //{
    //    public SampleRadialDataCollection()
    //    {
    //        this.Add(new RadialDataPoint { Label = "0", Radius = 80 });
    //        this.Add(new RadialDataPoint { Label = "30", Radius = 60 });
    //        this.Add(new RadialDataPoint { Label = "45", Radius = 40 });
    //        this.Add(new RadialDataPoint { Label = "60", Radius = 60 });

    //        this.Add(new RadialDataPoint { Label = "90", Radius = 80 });
    //        this.Add(new RadialDataPoint { Label = "120", Radius = 60 });
    //        this.Add(new RadialDataPoint { Label = "135", Radius = 40 });
    //        this.Add(new RadialDataPoint { Label = "150", Radius = 60 });

    //        this.Add(new RadialDataPoint { Label = "180", Radius = 20 });
    //        this.Add(new RadialDataPoint { Label = "210", Radius = 60 });
    //        this.Add(new RadialDataPoint { Label = "225", Radius = 40 });
    //        this.Add(new RadialDataPoint { Label = "240", Radius = 60 });

    //        this.Add(new RadialDataPoint { Label = "270", Radius = 60 });
    //        this.Add(new RadialDataPoint { Label = "300", Radius = 60 });
    //        this.Add(new RadialDataPoint { Label = "315", Radius = 40 });
    //        this.Add(new RadialDataPoint { Label = "330", Radius = 60 });
    //        this.Add(new RadialDataPoint { Label = "360", Radius = 80 });
    //    }
    //}

    public class SampleBubbleDataCollection : BubbleDataCollection
    {
        public SampleBubbleDataCollection()
        {
            this.GenerateCubicData();
        }
        public void GenerateCubicData()
        {
            this.Clear();
            double r = 20;
            CubicFunction function = new CubicFunction
                                         {
                                             Constant = 300, 
                                             ValueStart = -10, 
                                             CoefficientX3 = 0.5, 
                                             CoefficientX2 = 4, 
                                             CoefficientX1 = -1.5
                                         };
            for (int i = 0; i < 10; i++)
            {
                NumericDataPoint dataPoint = function.GenerateDataPoint(i);
                this.Add(new BubbleDataPoint { X = dataPoint.X, Y = dataPoint.Y, Radius = r });
                r += 5;
            }
            for (int i = 10; i < 20; i++)
            {
                NumericDataPoint dataPoint = function.GenerateDataPoint(i);
                this.Add(new BubbleDataPoint { X = dataPoint.X, Y = dataPoint.Y, Radius = r });
                r -= 5;
            }
        }
        public void GenerateSpiralData()
        {
            this.Clear();
            double r = 5, x = 0, y = 0;
            for (int i = 0; i <= 48; i++)
            {
                string lbl = i.ToString();
                this.Add(new BubbleDataPoint { X = x, Y = y, Radius = r, Label = lbl });
                x += 15;
                y += 10;
                r = i * 5;
                x %= 360;
            }
        }
        public void GenerateSinData()
        {
            this.Clear();
            SinFunction function = new SinFunction { Amplitude = 100, Shift = 10 };
            double r = 0;
            for (int i = 0; i < 20; i++)
            {
                r += 2;
                NumericDataPoint dataPoint = function.GenerateDataPoint(i);
                this.Add(new BubbleDataPoint { X = dataPoint.X, Y = dataPoint.Y, Radius = r });
            }
            for (int i = 20; i < 40; i++)
            {
                r -= 2;
                NumericDataPoint dataPoint = function.GenerateDataPoint(i);
                this.Add(new BubbleDataPoint { X = dataPoint.X, Y = dataPoint.Y, Radius = r });
            }
        }
        public void GenerateQuadraticData()
        {
            this.Clear();
            double r = 0;
            QuadraticFunction function = new QuadraticFunction();
            for (int i = 0; i < 20; i++)
            {
                r += 5;
                NumericDataPoint dataPoint = function.GenerateDataPoint(i);
                this.Add(new BubbleDataPoint { X = dataPoint.X, Y = dataPoint.Y, Radius = r });
            }
            for (int i = 20; i < 40; i++)
            {
                r -= 5;
                NumericDataPoint dataPoint = function.GenerateDataPoint(i);
                this.Add(new BubbleDataPoint { X = dataPoint.X, Y = dataPoint.Y, Radius = r });
            }
        }
        public void GenerateRandomData()
        {
            this.Clear();
            const int min = 5;
            const int max = 255;

            List<BubbleDataPoint> list = new List<BubbleDataPoint>();

            Random rnd = new Random(); 
            for (int i = 0; i <= 200; i++)
            {
                int radius = rnd.Next(5, 100);
                int x = rnd.Next(min, max);
                int y = rnd.Next(min, max);
                list.Add(new BubbleDataPoint { X = x, Y = y, Radius = radius });
            }


            list.Sort(delegate(BubbleDataPoint n1, BubbleDataPoint n2)
            {
                if (n1.Radius == n2.Radius) return 0;
                if (n1.Radius > n2.Radius) return -1;
                return 1;
            });

            foreach (BubbleDataPoint item in list)
            {
                this.Add(item);
            }

        }
   
    }
    public class CustomBubbleDataCollection : BubbleDataCollection
    {
        public CustomBubbleDataCollection()
        {

            this.GenerateData();
        }
        public void GenerateData()
        {
            this.Clear();

            List<BubbleDataPoint> list = new List<BubbleDataPoint>();

            double x = 20; double y = 20; double radius = 20;
            for (int i = 0; i < 10; i++)
            {
                list.Add(new BubbleDataPoint { X = x, Y = y, Radius = radius });
                radius += 10;
                x += 5;
                y += 5;
            }
            for (int i = 0; i <= 10; i++)
            {
                list.Add(new BubbleDataPoint { X = x, Y = y, Radius = radius });
                radius -= 10;
                x += 5;
                y += 5;
            }

            x = 20; y = 20; radius = 20;
            for (int i = 0; i < 10; i++)
            {
                radius += 10;
                x += 5;
                list.Add(new BubbleDataPoint { X = x, Y = y, Radius = radius });
            }
            for (int i = 0; i < 10; i++)
            {
                radius -= 10;
                x += 5;
                list.Add(new BubbleDataPoint { X = x, Y = y, Radius = radius });

            }

            x = 120; y = 20; radius = 20;
            for (int i = 0; i < 5; i++)
            {
                radius += 20;
                y += 10;
                list.Add(new BubbleDataPoint { X = x, Y = y, Radius = radius });

            }
            for (int i = 0; i < 4; i++)
            {
                radius -= 20;
                y += 10;
                list.Add(new BubbleDataPoint { X = x, Y = y, Radius = radius });

            }

            foreach (BubbleDataPoint item in list)
            {
                this.Add(item);
            }

        }
        public void GenerateData2()
        {
            this.Clear();

            List<BubbleDataPoint> list = new List<BubbleDataPoint>();

            double x = 5; double y = 5; double radius = 5;
            for (int i = 0; i < 10; i++)
            {
                list.Add(new BubbleDataPoint { X = x, Y = y, Radius = radius });
                radius += 5;
                x += 1.25;
                y += 2.5;
            }
            for (int i = 0; i <= 10; i++)
            {
                list.Add(new BubbleDataPoint { X = x, Y = y, Radius = radius });
                radius -= 5;
                x += 2.5;
                y += 1.25;
            }

            x = 5; y = 5; radius = 5;
            for (int i = 0; i < 10; i++)
            {
                list.Add(new BubbleDataPoint { X = x, Y = y, Radius = radius });
                radius += 5;
                x += 2.5;
                y += 1.25;
            }
            for (int i = 0; i < 10; i++)
            {
                list.Add(new BubbleDataPoint { X = x, Y = y, Radius = radius });
                radius -= 5;
                x += 1.25;
                y += 2.5;
            }

            foreach (BubbleDataPoint item in list)
            {
                this.Add(item);
            }

        }

    }
    public class BubbleCicleDataCollection : BubbleDataCollection
    {
        public BubbleCicleDataCollection()
        {

            this.GenerateData();
        }

        public void GenerateData()
        {
            this.Clear();
            
            List<BubbleDataPoint> list = new List<BubbleDataPoint>();
            //double x = 2.5, y = 2.5, radius = 0;

            for (int x = 0; x < 25; x++ )
            {
                for (int y = 0; y < 25; y++)
                {

                }
            }
               

            //for (int i = 0; i <= 10; i++)
            //{
            //    list.Add(new BubbleDataPoint { X = x, Y = y, Radius = radius });
            //    radius -= 5;
            //    x += 1.25;
            //    y += 2.5;
            //}


            ////for (int i = 0; i < 20; i++)
            ////{
            ////    radius += 5;
            ////    x += i % 5;
            ////    y += i % 5;
            ////    list.Add(new BubbleDataPoint { X = x, Y = y, Radius = radius });
            ////}

            //for (int i = 0; i < 8; i++)
            //{
            //    radius += 5;
            //    x += 2.5;
            //    y = rnd.Next(5, 20);
            //    list.Add(new BubbleDataPoint { X = x, Y = y, Radius = radius });
            //}

            //random
            //for (int i = 0; i < 10; i++)
            //{
            //    min += 2;
            //    max += 5;

            //    radius += 5;
            //    x = rnd.Next(5, 20);
            //    y = rnd.Next(5, 20);
            //    list.Add(new BubbleDataPoint { X = x, Y = y, Radius = radius });
            //}

            //for (int i = 10; i < 20; i++)
            //{
            //    radius -= 5;
            //    x = rnd.Next(5, 20);
            //    y = rnd.Next(5, 20);
            //    list.Add(new BubbleDataPoint { X = x, Y = y, Radius = radius });
            //}


            //for (int i = 12; i < 20; i++)
            //{
            //    radius += 5;
            //    y += 2.5;
            //    list.Add(new BubbleDataPoint { X = x, Y = y, Radius = radius });
            //}
            //for (int i = 0; i < 10; i++)
            //{
            //    radius += 5;
            //    x += 2.5;
            //    y += 2.5;
            //    list.Add(new BubbleDataPoint { X = x, Y = y, Radius = radius });
            //}
            //for (int i = 10; i < 20; i++)
            //{
            //    radius -= 5;
            //    x += 2.5;
            //    y += 2.5;
            //    list.Add(new BubbleDataPoint { X = x, Y = y, Radius = radius });
            //}


            //var sortedArray = list.OrderBy(x => x.Radius).ToArray();

            //list.Sort(delegate(BubbleDataPoint n1, BubbleDataPoint n2)
            //{
            //    if (n1.Radius == n2.Radius) return 0;
            //    if (n1.Radius > n2.Radius) return -1;
            //    return 1;
            //});

            foreach (BubbleDataPoint item in list)
            {
                this.Add(item);
            }

        }
        public void Sort()
        {
            //var query = from x in this
            //            where x.Tag == "Something"
            //            select x;

            var query = from item in this
                        orderby item.Radius
                        select item;

            List<BubbleDataPoint> sorted = new List<BubbleDataPoint>();
            //sorted = this.Items.
            this.Clear();
            foreach (var item in query)
                this.Add(item);
        }
    }
    
    //public class SampleCategoryDataCollection : CategoryDataCollection
    //{
    //    public SampleCategoryDataCollection()
    //    {
    //        this.Add(new CategoryDataPoint { Label = "United State", Value = 850, Low = 800, High = 950 });
    //        this.Add(new CategoryDataPoint { Label = "Russia", Value = 500, Low = 450, High = 650 });
    //        this.Add(new CategoryDataPoint { Label = "China", Value = 750, Low = 700, High = 900 });
    //        this.Add(new CategoryDataPoint { Label = "India", Value = 1000, Low = 750, High = 975 });
    //        this.Add(new CategoryDataPoint { Label = "Brazil", Value = 800, Low = 650, High = 800 });
    //        this.Add(new CategoryDataPoint { Label = "Indonesia", Value = 400, Low = 350, High = 550 });
    //        this.Add(new CategoryDataPoint { Label = "Japan", Value = 800, Low = 350, High = 500 });
    //        this.Add(new CategoryDataPoint { Label = "Germany", Value = 750, Low = 700, High = 900 });
    //        this.Add(new CategoryDataPoint { Label = "France", Value = 800, Low = 650, High = 800 });
    //        this.Add(new CategoryDataPoint { Label = "Cananda", Value = 850, Low = 800, High = 950 });
    //        this.Add(new CategoryDataPoint { Label = "Mexico", Value = 500, Low = 450, High = 600 });
    //    }
    //}
    public class SampleTimelineDataCollection : TimelineDataCollection
    {
        public SampleTimelineDataCollection()
        {
            this.Add(new TimelineDataPoint { Date = new DateTime(2010, 1, 1, 12, 0, 0), Value = 850 });
            this.Add(new TimelineDataPoint { Date = new DateTime(2010, 1, 2, 12, 10, 0), Value = 500 });
            this.Add(new TimelineDataPoint { Date = new DateTime(2010, 1, 3, 12, 20, 0), Value = 750 });
            this.Add(new TimelineDataPoint { Date = new DateTime(2010, 1, 4, 12, 30, 0), Value = 1000 });
            this.Add(new TimelineDataPoint { Date = new DateTime(2010, 1, 5, 12, 40, 0), Value = 800 });
            this.Add(new TimelineDataPoint { Date = new DateTime(2010, 1, 6, 12, 50, 0), Value = 400 });
            this.Add(new TimelineDataPoint { Date = new DateTime(2010, 1, 7, 13, 0, 0), Value = 800 });
        }
    }
    public class SampleNumericDataCollection : NumericDataCollection
    {
        public SampleNumericDataCollection()
        {
            this.Add(new NumericDataPoint { X = 1, Y = 850 });
            this.Add(new NumericDataPoint { X = 2, Y = 500 });
            this.Add(new NumericDataPoint { X = 3, Y = 750 });
            this.Add(new NumericDataPoint { X = 4, Y = 1000 });
            this.Add(new NumericDataPoint { X = 5, Y = 800 });
            this.Add(new NumericDataPoint { X = 6, Y = 400 });
            this.Add(new NumericDataPoint { X = 7, Y = 800 });
        }
    }
    
    
    public class RandomBubbleDataCollection : BubbleDataCollection
    {
        public RandomBubbleDataCollection()
        {

            this.GenerateData();
        }

        public void GenerateData()
        {
            this.Clear();

            List<BubbleDataPoint> list = new List<BubbleDataPoint>();
            Random rnd = new Random();

            const int min = 5;
            const int max = 45;

            for (int i = 0; i <= 50; i++)
            {
                double radius = rnd.Next(min, max);
                double x = rnd.Next(min, max);
                double y = rnd.Next(min, max);
                list.Add(new BubbleDataPoint { X = x, Y = y, Radius = radius });
            }

            //var sortedArray = list.OrderBy(x => x.Radius).ToArray();

            list.Sort(delegate(BubbleDataPoint n1, BubbleDataPoint n2)
            {
                if (n1.Radius == n2.Radius) return 0;
                if (n1.Radius > n2.Radius) return -1;
                return 1;
            });

            foreach (BubbleDataPoint item in list)
            {
                this.Add(item);
            }

        }
        public void Sort()
        {
            var query = from item in this
                        orderby item.Radius
                        select item;

            this.Clear();
            foreach (var item in query)
            {
                this.Add(item);
            }
        }
    }
    
    #region Complex Data

    public class SampleTrigonometricDataCollection : TrigonometricDataCollection
    {
        public SampleTrigonometricDataCollection()
        {
            this.GenerateData();
        }
        public void GenerateData()
        {
            this.Clear();
            const double amplitude = 0.02;
            for (int i = 0; i < 20; i++)
            {
                double x = i;
                double y = (amplitude * x) * System.Math.Cos(x * 15 * System.Math.PI / 180);
                this.Add(new TrigonometricDataPoint { X = x, Y = y });
            }
        }
        public void GenerateCosData()
        {
            this.Clear();
            CosFunction function = new CosFunction
                                       {
                                           AngleStart = 0, 
                                           Amplitude = 1, 
                                           AngleInterval = 15, 
                                           DataPoints = 48, 
                                           Shift = 0
                                       };
            for (int i = 0; i < function.DataPoints; i++)
            {
                NumericDataPoint dataPoint = function.GenerateDataPoint(i);
                this.Add(new TrigonometricDataPoint { X = dataPoint.X, Y = dataPoint.Y, Index = i });
            }
        }
        public void GenerateSinData()
        {
            this.Clear();
            SinFunction function = new SinFunction
            {
                AngleStart = 0,
                Amplitude = 1,
                AngleInterval = 15,
                DataPoints = 48,
                Shift = 0
            };
            for (int i = 0; i < function.DataPoints; i++)
            {
                NumericDataPoint dataPoint = function.GenerateDataPoint(i);
                this.Add(new TrigonometricDataPoint { X = dataPoint.X, Y = dataPoint.Y, Index = i });
            }
        }
    }
    public class SampleEconomicDataCollection : EconomicDataCollection
    {
        public SampleEconomicDataCollection()
        {

            _rnd = new Random();
            const int count = 200;
            double newValue = 1000;
            double newSupply = 1100, newDemand = 900;
            const double change = 10;
            DateTime newDate = new DateTime(2010, 1, 1);

            for (int i = 0; i < count; i++)
            {
                double oldValue = newValue;
                newValue = GetNewPrice(50, (int)oldValue);
                if (newValue > oldValue)
                {
                    newSupply += change; newDemand -= change;
                }
                else
                {
                    newSupply -= change; newDemand += change;
                }

                this.Add(new EconomicDataPoint
                {
                    Date = newDate,
                    Demand = newDemand,
                    Supply = newSupply,
                    Price = newValue
                });
                newDate = newDate.AddDays(7);
            }
        }

        private readonly Random _rnd;
        private double GetNewPrice(int sample, int prevValue)
        {
            double sum = 0;
            int min = prevValue - 100;
            int max = prevValue + 100;
            for (int i = 0; i < sample; i++)
            {
                double newValue = (double)_rnd.Next(min, max);
                sum += newValue;
            }
            return sum / sample;
        }
    }

    #endregion
}