using IGSurfaceChart.Samples.Helpers;
using IGSurfaceChart.Samples.Models;
using Infragistics.Samples.Shared.Models;
using System.Collections.Generic;
using System.Windows.Media.Media3D; 

namespace IGSurfaceChart.Samples.ViewModels
{
    public class RotationViewModel : ObservableModel
    {
        public RotationViewModel()
        {
            DataCollection = Data.Generate_XCuPlusYCu();
        }

        #region Properties

        public List<DataPoint3D> DataCollection { get; set; }
        
        private QuaternionRotation3D _quaternionRotation;
        public QuaternionRotation3D QuaternionRotation
        {
            get
            {
                return new QuaternionRotation3D(Math3D.GetQuaternionFromAngles(_xAxisAngleRotation, _yAxisAngleRotation, _zAxisAngleRotation));
            }
            set
            {
                if (this._quaternionRotation != value)
                {
                    this._quaternionRotation = value;
                    this.OnPropertyChanged("QuaternionRotation");
                }
            }
        }

        private double _xAxisAngleRotation = 50;
        public double XAxisAngleRotation
        {
            get
            {
                return this._xAxisAngleRotation;
            }
            set
            {
                if (this._xAxisAngleRotation != value)
                {
                    this._xAxisAngleRotation = value;
                    this.OnPropertyChanged("XAxisAngleRotation");
                    this.OnPropertyChanged("QuaternionRotation");
                }
            }
        }

        private double _yAxisAngleRotation = -10;
        public double YAxisAngleRotation
        {
            get
            {
                return this._yAxisAngleRotation;
            }
            set
            {
                if (this._yAxisAngleRotation != value)
                {
                    this._yAxisAngleRotation = value;
                    this.OnPropertyChanged("YAxisAngleRotation");
                    this.OnPropertyChanged("QuaternionRotation");
                }
            }
        }

        private double _zAxisAngleRotation = 180;
        public double ZAxisAngleRotation
        {
            get
            {
                return this._zAxisAngleRotation;
            }
            set
            {
                if (this._zAxisAngleRotation != value)
                {
                    this._zAxisAngleRotation = value;
                    this.OnPropertyChanged("ZAxisAngleRotation");
                    this.OnPropertyChanged("QuaternionRotation");
                }
            }
        }

        #endregion // Properties
    }
}
