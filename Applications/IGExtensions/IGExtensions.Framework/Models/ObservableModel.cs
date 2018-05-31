using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Data;
using IGExtensions.Framework;

namespace IGExtensions.Framework //.Extensions.Framework //System.ComponentModel //InfragisticsEx.Models //System.ComponentModel
{
    /// <summary>
    /// Represents an observable model that implements INotifyPropertyChanged interface
    /// </summary>
    [DataContract]
    public abstract class ObservableModel : INotifyPropertyChanged 
    {
 
        #region INotifyPropertyChanged  

        /// <summary>
        /// Occurs when a property value was changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected bool HasPropertyChangedHandler()
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            return handler != null;
        }
        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
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
        /// <summary>
        /// Raises the PropertyChanged event for provided property name
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public void LogPropertyChange(string propertyName)
        {
            DebugManager.LogTrace("PropertyChanged: " + "." + propertyName);
        }
        public void LogPropertyChange(object sender, string propertyName)
        {
            DebugManager.LogTrace("PropertyChanged: " + "." + propertyName);  
         }
        public void LogPropertyChange(PropertyChangedEventArgs ea)
        {
            LogPropertyChange(ea.PropertyName);
        }
        public void LogPropertyChange(object sender, PropertyChangedEventArgs ea)
        {
            LogPropertyChange(ea.PropertyName);
        }
        protected delegate void PropertyChangedDelegate(object sender, string propertyName);

        #endregion
       
    }

    public class ObservableObject : DependencyObject, INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property (including "effective" and non-dependency property) value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event for provided property name
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged(string propertyName)
        {
            //DebugManager.LogTrace("PropertyChanging: " + this.GetType() + "." + propertyName);
            OnPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public void OnPropertyChanged(string propertyName, object oldValue, object newValue)
        {
            OnPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            //if (PropertyChanged != null)
            //{
            //    PropertyChanged(this, new PropertyChangedEventArgs(name));
            //}
        }
        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
                handler(sender, e);
        }
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
                handler(null, e);
        }
        public void LogPropertyChange(string propertyName)
        {
            DebugManager.LogTrace("PropertyChanging: " + this.GetType() + "." + propertyName);
        }
        public void LogPropertyChange(PropertyChangedEventArgs ea)
        {
            LogPropertyChange(ea.PropertyName);
        }
    }

    public class ObservableCollectionViewSource : CollectionViewSource, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            //DebugManager.LogTrace("PropertyChanging: " + this.GetType() + "." + propertyName);
            OnPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
                handler(sender, e);
        }
        public void LogPropertyChange(string propertyName)
        {
            DebugManager.LogTrace("PropertyChanging: " + this.GetType() + "." + propertyName);
        }
        public void LogPropertyChange(PropertyChangedEventArgs ea)
        {
            LogPropertyChange(ea.PropertyName);
        }
    }
}
