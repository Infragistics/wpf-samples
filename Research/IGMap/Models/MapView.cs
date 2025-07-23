using System.Windows;
using Infragistics.Samples.Shared.Models;   // ObservableModel

namespace IGMap.Models
{
    public class MapView : ObservableModel
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

    }
}