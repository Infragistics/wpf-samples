using System.ComponentModel;
using System.Windows.Controls;

namespace IGExtensions.Common.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class ObservableControl : Control, INotifyPropertyChanged
    {

#if SILVERLIGHT
        protected bool IsLoaded;
#else
        protected new bool IsLoaded;
#endif
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            OnPropertyChanged(ea.PropertyName);
            //switch (ea.PropertyName)
            //{
            //    case "PropertyName":
            //        {
                      
            //            break;
            //        }
            //}
        }

        #endregion
    }
}