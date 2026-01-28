using IGSurfaceChart.Samples.Models;
using Infragistics.Controls.Charts;
using Infragistics.Samples.Shared.Models;
using System.Collections.Generic; 

namespace IGSurfaceChart.Samples.ViewModels
{
    public class ScalingViewModel : ObservableModel
    {
        private const int pointCount = 300;
        private const double seed = 0.3;

        public ScalingViewModel()
        {
            DataCollection = new RandomData(pointCount, seed);
        }

        #region Properties
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

        #region Scale
        private double _scaleSettings = (double)XamChart3D.ScaleProperty.DefaultMetadata.DefaultValue;
        public double ScaleSettings
        {
            get
            {
                return this._scaleSettings;
            }
            set
            {
                if (this._scaleSettings != value)
                {
                    this._scaleSettings = value;
                    this.OnPropertyChanged("ScaleSettings");
                }
            }
        }
        #endregion // Scale

        #endregion       
    }
}
