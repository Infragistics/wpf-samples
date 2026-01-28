namespace Infragistics.Samples.Shared.Models
{
    public class DataPoint : ObservableModel
    {
        #region Properties
        private string _label;
        public string Label
        {
            get { return _label; }
            set
            {
                if (_label == value) return;
                _label = value;
                OnPropertyChanged("Label");
            }
        }

        private int _index;
        public int Index
        {
            get { return _index; }
            set
            {
                if (_index == value) return;
                _index = value;
                OnPropertyChanged("Index");
            }
        }

        public string Caption
        {
            get { return this.ToString(); }
        }

        #endregion
    }
}