using System;
using System.Collections.ObjectModel;


namespace Infragistics.Samples.Shared.Models
{
    public class NumericDataSample : NumericDataCollection
    {
        public NumericDataSample()
        {
            this.Add(new NumericDataPoint { X = 1, Y = 1 });
            this.Add(new NumericDataPoint { X = 2, Y = 2 });
            this.Add(new NumericDataPoint { X = 3, Y = 6 });
            this.Add(new NumericDataPoint { X = 4, Y = 8 });
            this.Add(new NumericDataPoint { X = 5, Y = 2 });
            this.Add(new NumericDataPoint { X = 6, Y = 6 });
            this.Add(new NumericDataPoint { X = 7, Y = 4 });
            this.Add(new NumericDataPoint { X = 8, Y = 2 });
            this.Add(new NumericDataPoint { X = 9, Y = 1 });

            int index = 0;
            foreach (NumericDataPoint dataPoint in Items)
            {
                dataPoint.Index = index++;
            }
        }
    }
    public class NumericDataCollection : ObservableCollection<NumericDataPoint>
    {

    }
    public class NumericDataPoint : DataPoint
    {
        #region Properties
        private double _y;
        public double Y
        {
            get { return _y; }
            set
            {
                if (_y == value) return;
                _y = value;
                OnPropertyChanged("Y");
            }
        }

        private double _x;
        public double X
        {
            get { return _x; }
            set
            {
                if (_x == value) return;
                _x = value;
                OnPropertyChanged("X");
            }
        }

        #endregion

        public new string ToString()
        {
            return String.Format("Index {0}, X {1}, Y {2}", Index, X, Y);
        }
    }
}