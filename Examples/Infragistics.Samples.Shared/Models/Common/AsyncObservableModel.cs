using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Infragistics.Samples.Shared.Models.Common
{
    public class AsyncObservableModel : ObservableModel
    {
        public void OnAsyncPropertyChanged(string propertyName)
        {
            if (HasPropertyChangedHandler())
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    new ThreadStart(() => OnPropertyChanged(this, propertyName)));

                //Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                //new ThreadStart(() =>
                //{
                //    OnPropertyChanged(this, propertyName);
                //}));
            }
        }
        //public void OnAsyncPropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
        //                                           new ThreadStart(() =>
        //                                           {
        //                                               PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //                                           }));
        //    }
        //}

    }
}