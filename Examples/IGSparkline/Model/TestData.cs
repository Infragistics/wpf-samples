using System.Collections.ObjectModel;
using System.ComponentModel;

namespace IGSparkline.Model
{
    public class TestData : ObservableCollection<TestDataItem>
    {
        public TestData()
        {
            Add(new TestDataItem { Label = "4", Value = 4 });
            Add(new TestDataItem { Label = "5", Value = 5 });
            Add(new TestDataItem { Label = "2", Value = 2 });
            Add(new TestDataItem { Label = "7", Value = 7 });
            Add(new TestDataItem { Label = "-1", Value = -1 });
            Add(new TestDataItem { Label = "3", Value = 3 });
            Add(new TestDataItem { Label = "-2", Value = -2 });
            Add(new TestDataItem { Label = "5", Value = 5 });
            Add(new TestDataItem { Label = "2", Value = 2 });
            Add(new TestDataItem { Label = "6", Value = 6 });
        }
    }

    public class TestDataItem : INotifyPropertyChanged
    {
        private string _label;
        public string Label
        {
            get { return _label; }
            set { _label = value; RaisePropertyChanged("Label"); }
        }

        private double? _value;
        public double? Value
        {
            get { return _value; }
            set { _value = value; RaisePropertyChanged("Value"); }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        } 
        #endregion
    }
}
