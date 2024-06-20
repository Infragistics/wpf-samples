using IGSurfaceChart.Samples.Models;
using Infragistics.Samples.Shared.Models;
using System.Windows.Media;
using ColorCollection = Infragistics.Controls.Charts.ColorCollection;

namespace IGSurfaceChart.Samples.ViewModels
{
    public class KeyFeatureViewModel : ObservableModel
    {
        private const int pointCount = 500;
        private const double seed = 0.0;

        public KeyFeatureViewModel()
        {
            DataCollection = new RandomData(pointCount, seed);
        }

        private RandomData _dataCollection;
        public RandomData DataCollection
        {
            get { return _dataCollection; }
            private set
            {
                _dataCollection = value;
                OnPropertyChanged("DataCollection");
            }
        }

        public ColorCollection ColorsCollection
        {
            get
            {
                var colors = new ColorCollection();
                colors.Add(_lowValueColor);
                colors.Add(_midValueColor);
                colors.Add(_highValueColor);

                return colors;
            }
        }

        private Color _lowValueColor;
        public Color LowValueColor
        {
            get { return _lowValueColor; }
            set
            {
                _lowValueColor = value;
                OnPropertyChanged("LowValueColor");
                OnPropertyChanged("ColorsCollection");
            }
        }

        private Color _midValueColor;
        public Color MidValueColor
        {
            get { return _midValueColor; }
            set
            {
                _midValueColor = value;
                OnPropertyChanged("MidValueColor");
                OnPropertyChanged("ColorsCollection");
            }
        }

        private Color _highValueColor;
        public Color HighValueColor
        {
            get { return _highValueColor; }
            set
            {
                _highValueColor = value;
                OnPropertyChanged("HighValueColor");
                OnPropertyChanged("ColorsCollection");
            }
        }
    }
}
