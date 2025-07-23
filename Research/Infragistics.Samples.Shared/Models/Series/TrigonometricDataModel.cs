using System.Collections.ObjectModel;

 
namespace Infragistics.Samples.Shared.Models
{
    public class TrigonometricDataModel : ObservableModel
    {
        public TrigonometricDataModel()
        {
            this.Data = new TrigonometricDataCollection();
            this.Generate();
        }
        public int Points { get; set; }
        public double AmplitudeCoefficient { get; set; }
        public double PeriodCoefficient { get; set; }
        public void Generate()
        {
            this.Data.Clear();
            for (int i = 0; i < Points; i++)
            {
                double x = i * PeriodCoefficient;
                double y = AmplitudeCoefficient * i * System.Math.Cos(x * System.Math.PI / 180);
                _data.Add(new TrigonometricDataPoint { X = x, Y = y, Index = i });
            }
        }
        private TrigonometricDataCollection _data;
        public TrigonometricDataCollection Data
        {
            get { return _data; }
            set
            {
                if (_data == value) return;
                _data = value;
                OnPropertyChanged("Data");
            }
        }
    }
    public class TrigonometricDataCollection : ObservableCollection<TrigonometricDataPoint>
    {
        public TrigonometricDataCollection()
        {
        }
    }
   
    public class TrigonometricDataPoint : NumericDataPoint
    {
  
    }
}