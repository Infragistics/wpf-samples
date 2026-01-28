using IGSurfaceChart.Samples.Models;
using Infragistics.Controls.Charts;
using Infragistics.Samples.Shared.Models;
using System.Collections.Generic;
using System.Windows.Media.Media3D; 

namespace IGSurfaceChart.Samples.ViewModels
{
    public class LightSettingsViewModel : ObservableModel
    {
        public LightSettingsViewModel()
        {
            DataCollection = Data.GenerateFormula6Data();
        }

        public List<DataPoint3D> DataCollection { get; set; }

        #region LightDirection

        private Vector3D _lightDirectionVector;
        public Vector3D LightDirectionVector
        {
            get
            {
                return new Vector3D(_lightDirectionX, _lightDirectionY, _lightDirectionZ);
            }
            set
            {
                if (this._lightDirectionVector != value)
                {
                    this._lightDirectionVector = value;
                    this.OnPropertyChanged("LightDirectionVector");
                }
            }
        }

        private double _lightDirectionX = ((Vector3D)XamScatterSurface3D.LightDirectionProperty.DefaultMetadata.DefaultValue).X;
        public double LightDirectionX
        {
            get
            {
                return this._lightDirectionX;
            }
            set
            {
                if (this._lightDirectionX != value)
                {
                    this._lightDirectionX = value;
                    this.OnPropertyChanged("LightDirectionX");
                    this.OnPropertyChanged("LightDirectionVector");
                }
            }
        }

        private double _lightDirectionY = ((Vector3D)XamScatterSurface3D.LightDirectionProperty.DefaultMetadata.DefaultValue).Y;
        public double LightDirectionY
        {
            get
            {
                return this._lightDirectionY;
            }
            set
            {
                if (this._lightDirectionY != value)
                {
                    this._lightDirectionY = value;
                    this.OnPropertyChanged("LightDirectionY");
                    this.OnPropertyChanged("LightDirectionVector");
                }
            }
        }

        private double _lightDirectionZ = ((Vector3D)XamScatterSurface3D.LightDirectionProperty.DefaultMetadata.DefaultValue).Z;
        public double LightDirectionZ
        {
            get
            {
                return this._lightDirectionZ;
            }
            set
            {
                if (this._lightDirectionZ != value)
                {
                    this._lightDirectionZ = value;
                    this.OnPropertyChanged("LightDirectionZ");
                    this.OnPropertyChanged("LightDirectionVector");
                }
            }
        }

        #endregion // LightDirection
    }
}
