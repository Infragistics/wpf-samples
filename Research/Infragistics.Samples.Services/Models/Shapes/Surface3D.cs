using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Threading;
using System.Collections.Specialized;

namespace Infragistics.Models
{
    public abstract class Surface3D : Shape3D
    {
        public Surface3D()
               : this(11, 11)
        {

        }
        public Surface3D(int xCount, int yCount)
            : this(-100, 100, -100, 100, xCount, yCount)
        {
            
        }

        public Surface3D(double xMin, double xMax, 
                         double yMin, double yMax,
                         int xCount = 11, int yCount = 11) 
        {
            XMin = xMin;
            XMax = xMax;
            YMin = xMin;
            YMax = yMax;
            XCount = xCount;
            YCount = yCount;

            Generate();
             
            UpdateTimer.Interval = TimeSpan.FromSeconds(UpdateInterval);
            UpdateTimer.Tick += OnUpdateTimerTick;
        }
        
        public double XMin { get; private set; }
        public double XMax { get; private set; }
        public double YMin { get; private set; }
        public double YMax { get; private set; }
        public double XCount { get; private set; }
        public double YCount { get; private set; }
          
        private bool _IsRandom;
        /// <summary> Gets or sets IsRandom </summary>
        public bool IsRandom
        {
            get { return _IsRandom; }
            set { if (_IsRandom == value) return; _IsRandom = value; Generate(); }
        }
        
        protected void Generate()
        {
            if (IsRandom)
            {
                var random = new Random();
                for (var i = 0; i <= XCount * YCount; i++)
                {
                    var x = random.Next((int)XMin, (int)XMax);
                    var y = random.Next((int)YMin, (int)YMax);

                    this.Add(new ShapePoint3D { X = x, Y = y, Z = this.Z(x, y) });
                }
            }
            else
            {
                var xStep = (XMax - XMin) / (XCount - 1);
                var yStep = (YMax - YMin) / (YCount - 1);

                for (var x = XMin; x <= XMax; x += xStep)
                {
                    for (var y = YMin; y <= YMax; y += yStep)
                    {
                        this.Add(new ShapePoint3D { X = x, Y = y, Z = this.Z(x, y) });
                    }
                }
            }
            Debug.WriteLine("Data points = " + this.Count);
        }
        protected abstract new double Z(double x, double y);


    }

}
