using System;
using System.ComponentModel;
using System.Windows;

namespace IGPropertyGrid.Samples.DataModel
{
    public class MyDependencyObject : DependencyObject, INotifyPropertyChanged
    {
        private string _MyRegularProperty1;

        public string MyRegularProperty1
        {
            get { return _MyRegularProperty1; }
            set
            {
                if (_MyRegularProperty1 != value)
                {
                    _MyRegularProperty1 = value;
                    OnPropertyChanged("MyRegularProperty1");
                }
            }
        }

        public static readonly DependencyProperty MyDependencyProperty1Property =
            DependencyProperty.Register(
                "MyDependencyProperty1",
                typeof(String),
                typeof(MyDependencyObject));

        public string MyDependencyProperty1
        {
            get { return (string)this.GetValue(MyDependencyObject.MyDependencyProperty1Property); }
            set { this.SetValue(MyDependencyObject.MyDependencyProperty1Property, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
