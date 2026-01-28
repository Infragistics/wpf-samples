using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Infragistics.Framework
{ 
    public abstract class ObservableControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                OnPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            OnPropertyChanged(this, e);
        }
        protected void OnPropertyChanged(object sender, string propertyName)
        {
            OnPropertyChanged(sender, new PropertyChangedEventArgs(propertyName));
        }
        //protected void OnPropertyChanged(string propertyName)
        //{
        //    OnPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //}

    } 
}
