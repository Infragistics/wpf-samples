using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Runtime.Serialization;
using System.Windows.Threading;
using Infragistics.Samples.Shared.Tools;

namespace Infragistics.Samples.Shared.Models
{
    [DataContract]
    public abstract class ObservableModel : INotifyPropertyChanged, IDataErrorInfo
    {
        protected ObservableModel()
        {
            IsPropertyNotifyActive = true;
        }

        #region INotifyPropertyChanged  
        public bool IsPropertyNotifyActive { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected bool HasPropertyChangedHandler()
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            return handler != null;
        }
        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null && IsPropertyNotifyActive)
                handler(sender, e);
        }
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            OnPropertyChanged(this, e);
        }
        protected void OnPropertyChanged(object sender, string propertyName)
        {
            OnPropertyChanged(sender, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        protected delegate void PropertyChangedDelegate(object sender, string propertyName);

        //WPF
        //public void OnAsyncPropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
        //           new ThreadStart(() =>
        //           {
        //              PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //         }));
        //    }
        //}
        #endregion
        #region IDataErrorInfo Members
        public ValidationHandler ValidationHandler = new ValidationHandler();
        public string Error
        {
            get { return null; }
        }
        public string this[string columnName]
        {
            get
            {
                if (this.ValidationHandler.BrokenRuleExists(columnName))
                {
                    return this.ValidationHandler[columnName];
                }
                return null;
            }
        }
        #endregion
    }
}
