
 
namespace Infragistics.Samples.Shared.Models
{
    public class ScatterDataModel : ObservableModel
    {
        public ScatterDataModel()
        {
            _data = new ScatterDataCollection();
            _function = new RandomFunction { DataPoints = 100, ValueStart = 100 };
            _function.PropertyChanged += OnFunctionPropertyChanged;
            this.Generate();
        }

        private void OnFunctionPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.Generate();
        }
        public void Generate()
        {
            _data = new ScatterDataSample();
        }

        #region Properties
        private Function _function;
        public Function GenerationFunction
        {
            get { return _function; }
            set
            {
                if (_function == value) return;
                _function = value;
                OnPropertyChanged("GenerationFunction");
            }
        }

        private ScatterDataCollection _data;
        public ScatterDataCollection Data
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