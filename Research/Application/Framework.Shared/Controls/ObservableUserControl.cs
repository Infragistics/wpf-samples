using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Infragistics.Framework
{
    public class ObservableUserControl : UserControl, INotifyPropertyChanged
    {

#if SILVERLIGHT
        protected bool IsLoaded;
#else
        protected new bool IsLoaded;
#endif
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public virtual void OnPropertyChanged(PropertyChangedEventArgs ea)
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
