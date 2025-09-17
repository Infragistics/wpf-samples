using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices; 

namespace Infragistics.Framework
{
    public abstract class ObservableModel : INotifyPropertyChanged
    {
        protected ObservableModel()
        {
            IsPropertyNotifyActive = true;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        internal bool IsPropertyNotifyActive { get; set; }

        public void SetPropertyNotifications(bool isActive)
        {
            IsPropertyNotifyActive = isActive;
        }

        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var handler = this.PropertyChanged;
            if (handler != null && IsPropertyNotifyActive)
            { 
                handler(sender, e);
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
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