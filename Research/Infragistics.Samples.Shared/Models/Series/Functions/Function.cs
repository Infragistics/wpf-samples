namespace Infragistics.Samples.Shared.Models 
{
    public abstract class Function : ObservableModel
    {
        protected Function()
        {
            _dataPoints = 100;
        }

        public int DataPoints
        {
            get { return _dataPoints; }
            set
            {
                if (_dataPoints == value) return;
                if (_dataPoints < 0) return;
                _dataPoints = value;
                OnPropertyChanged("DataPoints");
            }
        }
        private int _dataPoints;
        public abstract NumericDataPoint GenerateDataPoint(int index);


    }
  
}