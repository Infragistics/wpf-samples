using Infragistics;
using Infragistics.Samples.Shared.Models;
using System;
using System.Collections.ObjectModel;

namespace IGSurfaceChart.Samples.Models
{
    public class DataItem : ObservableModel
    {
        private static Random random = new Random();

        public DataItem(double seed)
        {
            x = 100.0 * random.NextDouble();
            y = 100.0 * random.NextDouble();
            z = MathUtilExtended.Noise(0.02 * x, 0.02 * y, seed);
        }

        public void Update(double seed)
        {
            this.z = MathUtilExtended.Noise(0.02 * x, 0.02 * y, seed);
        }

        private double x;
        public double X
        {
            get { return x; }
            set
            {
                x = value;
                OnPropertyChanged("X");
            }
        }

        private double y;
        public double Y
        {
            get { return y; }
            set
            {
                y = value;
                OnPropertyChanged("Y");
            }
        }

        private double z;
        public double Z
        {
            get { return z; }
            set
            {
                z = value;
                OnPropertyChanged("Z");
            }
        }
    }

    public class RandomData : ObservableCollection<DataItem>
    {
        public RandomData(int count, double seed)
        {
            for (int i = 0; i < count; ++i)
            {
                Add(new DataItem(seed));
            }
        }

        public void Update(double seed)
        {
            seed += 0.01;
            foreach (DataItem item in this) item.Update(seed);
        }
    }
}
