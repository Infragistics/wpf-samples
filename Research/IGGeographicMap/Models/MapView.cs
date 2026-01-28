using System.ComponentModel;
using System.Windows;

namespace IGGeographicMap.Models
{
    public class MapView : INotifyPropertyChanged
    {
        private Rect _windowRect;
        public Rect WindowRect
        {
            get
            {
                return _windowRect;
            }

            set
            {
                _windowRect = value;
                OnPropertyChanged("WindowRect");
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }
        public override string ToString()
        {
            return this.Description;
        }
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }

        }
        #endregion
    }

     public class MapViews
     {
         public static MapView WorldNonPolarMapView = new MapView { WindowRect = new Rect(0.15, 0.1, 0.7, 0.7) };
         public static MapView WorldNonAntarcticMapView = new MapView { WindowRect = new Rect(0.15, 0.025, 0.7, 0.7) };
     }
}