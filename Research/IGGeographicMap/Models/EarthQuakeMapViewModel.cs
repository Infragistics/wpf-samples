using System.Collections.ObjectModel;
using System.Windows.Data;
using Infragistics.Samples.Shared.Models;

namespace IGGeographicMap.Models
{
    /// <summary>
    /// Represents EarthQuake Map view model with a collection of EarthQuakes that can be filtered 
    /// </summary>
    public class EarthQuakeMapViewModel : ObservableModel
    {
        public EarthQuakeMapViewModel()
        {
            _earthQuakeData = new ObservableCollection<EarthQuakeViewModel>();

            _earthQuakeFilter = new EarthQuakeFilter(2.0, 10.0);
            _earthQuakeFilter.PropertyChanged += OnEarthQuakeFilterChanged;

            _earthQuakeFilteredData = new CollectionViewSource();
            _earthQuakeFilteredData.Source = this.EarthQuakeData;
            _earthQuakeFilteredData.Filter += this.EarthQuakeFilter.FilterEarthQuakes;
        }


        #region Properties
        private ObservableCollection<EarthQuakeViewModel> _earthQuakeData;
        public ObservableCollection<EarthQuakeViewModel> EarthQuakeData
        {
            get { return _earthQuakeData; }
            set
            {
                _earthQuakeData = value;
                OnPropertyChanged("EarthQuakeData");
            }
        }
        private CollectionViewSource _earthQuakeFilteredData;
        public CollectionViewSource EarthQuakeFilteredData
        {
            get { return _earthQuakeFilteredData; }
            private set
            {
                _earthQuakeFilteredData = value;
                OnPropertyChanged("EarthQuakeFilteredData");
            }
        }

        private EarthQuakeFilter _earthQuakeFilter;
        public EarthQuakeFilter EarthQuakeFilter
        {
            get { return _earthQuakeFilter; }
            set
            {
                _earthQuakeFilter = value;
                OnPropertyChanged("EarthQuakeFilter");
            }
        }
        #endregion
        public void FilterData()
        {
            _earthQuakeFilteredData = new CollectionViewSource();
            _earthQuakeFilteredData.Source = this.EarthQuakeData;
            _earthQuakeFilteredData.Filter += this.EarthQuakeFilter.FilterEarthQuakes;
            _earthQuakeFilteredData.View.Refresh();
        }

        #region Event Handlers
        private void OnEarthQuakeFilterChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            string propertyName = e.PropertyName;

            if (propertyName == "EarthQuakeMinMagnitude" || propertyName == "EarthQuakeMinDepth" ||
                propertyName == "EarthQuakeMaxMagnitude" || propertyName == "EarthQuakeMaxDepth")
            {
                this.EarthQuakeFilteredData.View.Refresh();
            }
        }
        protected new void OnPropertyChanged(string propertyName)
        {

            if (propertyName == "EarthQuakeData")
            {
                this.EarthQuakeFilteredData.Source = this.EarthQuakeData;
                //this.EarthQuakeFilteredData.View.Refresh();
            }
            else if (propertyName == "EarthQuakeFilter")
            {
                this.EarthQuakeFilteredData = new CollectionViewSource();
                this.EarthQuakeFilteredData.Source = this.EarthQuakeData;
                this.EarthQuakeFilteredData.Filter += this.EarthQuakeFilter.FilterEarthQuakes;

                this.EarthQuakeFilteredData.View.Refresh();
            }
            base.OnPropertyChanged(propertyName);
        }
        #endregion
    }

    /// <summary>
    /// Represents a filter of collection of EarthQuake models for CollectionViewSource
    /// </summary>
    public class EarthQuakeFilter : ObservableModel
    {
        public EarthQuakeFilter()
            : this(0.0, 12.0)
        { }
        public EarthQuakeFilter(double minMagnitude, double maxMagnitude)
        {
            _earthQuakeMinMagnitude = minMagnitude;
            _earthQuakeMaxMagnitude = maxMagnitude;
            _earthQuakeMinDepth = 0;
            _earthQuakeMaxDepth = 1000;
        }

        public void FilterEarthQuakes(object sender, FilterEventArgs e)
        {
            var earthQuake = ((EarthQuakeViewModel)e.Item).EarthQuakeData;

            //e.Accepted = IsEarthQuakeInMagnitudeRange(earthQuake);
            e.Accepted = IsEarthQuakeInMagnitudeRange(earthQuake) &&
                         IsEarthQuakeInDepthRange(earthQuake);
        }


        public bool IsEarthQuakeInMagnitudeRange(EarthQuake earthQuake)
        {
            if (earthQuake == null) return false;

            return earthQuake.Magnitude >= this.EarthQuakeMinMagnitude && earthQuake.Magnitude <= this.EarthQuakeMaxMagnitude;
        }
        public bool IsEarthQuakeInDepthRange(EarthQuake earthQuake)
        {
            if (earthQuake == null) return false;

            return earthQuake.Depth >= this.EarthQuakeMinDepth && earthQuake.Depth <= this.EarthQuakeMaxDepth;
        }

        private double _earthQuakeMinMagnitude;
        public double EarthQuakeMinMagnitude
        {
            get { return _earthQuakeMinMagnitude; }
            set
            {
                _earthQuakeMinMagnitude = value;
                OnPropertyChanged("EarthQuakeMinMagnitude");
            }
        }
        private double _earthQuakeMaxMagnitude;
        public double EarthQuakeMaxMagnitude
        {
            get { return _earthQuakeMaxMagnitude; }
            set
            {
                _earthQuakeMaxMagnitude = value;
                OnPropertyChanged("EarthQuakeMaxMagnitude");
            }
        }
        private double _earthQuakeMinDepth;
        public double EarthQuakeMinDepth
        {
            get { return _earthQuakeMinDepth; }
            set
            {
                _earthQuakeMinDepth = value;
                OnPropertyChanged("EarthQuakeMinDepth");
            }
        }
        private double _earthQuakeMaxDepth;
        public double EarthQuakeMaxDepth
        {
            get { return _earthQuakeMaxDepth; }
            set
            {
                _earthQuakeMaxDepth = value;
                OnPropertyChanged("EarthQuakeMaxDepth");
            }
        }
    }

   
}