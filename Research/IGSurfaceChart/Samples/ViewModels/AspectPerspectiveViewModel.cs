using IGSurfaceChart.Samples.Models;
using Infragistics.Controls.Charts;
using Infragistics.Samples.Shared.Models;
using System.Collections.Generic;
using System.Windows.Media.Media3D; 

namespace IGSurfaceChart.Samples.ViewModels
{
    public class AspectPerspectiveViewModel : ObservableModel
    {
        public AspectPerspectiveViewModel()
        {
            DataCollection = Data.Generate_XSqPlusYSq();
        }

        #region Properties
        public List<DataPoint3D> DataCollection { get; set; }

        #region Aspect

        private Vector3D _aspectSettings;
        public Vector3D AspectSettings
        {
            get
            {
                return new Vector3D(_aspectX, _aspectY, _aspectZ);
            }
            set
            {
                if (this._aspectSettings != value)
                {
                    this._aspectSettings = value;
                    this.OnPropertyChanged("AspectSettings");
                }
            }
        }

        private double _aspectX = 0.5;
        public double AspectX
        {
            get
            {
                return this._aspectX;
            }
            set
            {
                if (this._aspectX != value)
                {
                    this._aspectX = value;
                    this.OnPropertyChanged("AspectX");
                    this.OnPropertyChanged("AspectSettings");
                }
            }
        }

        private double _aspectY = 0.5;
        public double AspectY
        {
            get
            {
                return this._aspectY;
            }
            set
            {
                if (this._aspectY != value)
                {
                    this._aspectY = value;
                    this.OnPropertyChanged("AspectY");
                    this.OnPropertyChanged("AspectSettings");
                }
            }
        }

        private double _aspectZ = 0.5;
        public double AspectZ
        {
            get
            {
                return this._aspectZ;
            }
            set
            {
                if (this._aspectZ != value)
                {
                    this._aspectZ = value;
                    this.OnPropertyChanged("AspectZ");
                    this.OnPropertyChanged("AspectSettings");
                }
            }
        }

        #endregion // Aspect

        #region Perspective

        private double _perspectiveSettings = (double)XamChart3D.PerspectiveProperty.DefaultMetadata.DefaultValue;
        public double PerspectiveSettings
        {
            get
            {
                return this._perspectiveSettings;
            }
            set
            {
                if (this._perspectiveSettings != value)
                {
                    this._perspectiveSettings = value;
                    this.OnPropertyChanged("PerspectiveSettings");
                }
            }
        }
        #endregion // Perspective

        #endregion // Properties

    }
}
