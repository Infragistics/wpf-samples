

namespace Infragistics.Samples.Shared.Models
{
    public class NumericDataModel : ObservableModel
    {

        public NumericDataModel()
        {
            _data = new NumericDataCollection();
            _function = new RandomFunction { DataPoints = 1000, ValueStart = 100 };
            _function.PropertyChanged += OnFunctionPropertyChanged;
            this.Generate();
        }

        private void OnFunctionPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.Generate();
        }
        public void Generate()
        {
            _data = new NumericDataCollection();
            for (int i = 0; i < _function.DataPoints; i++)
            {
                _data.Add(_function.GenerateDataPoint(i));
            }
        }

        #region Properties
        private Function _function;
        public Function Function
        {
            get { return _function; }
            set
            {
                if (_function == value) return;
                _function = value;
                OnPropertyChanged("Function");
                this.Generate();
            }
        }

        private NumericDataCollection _data;
        public NumericDataCollection Data
        {
            get { return _data; }
            set
            {
                if (_data == value) return;
                _data = value;
                OnPropertyChanged("Data");
            }
        } 
        #endregion
    }
 
}
