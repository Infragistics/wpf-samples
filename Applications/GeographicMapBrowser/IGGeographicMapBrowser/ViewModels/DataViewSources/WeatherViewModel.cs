using System.Collections.Generic;
using System.ComponentModel;
using IGExtensions.Common.Maps.DataSelectors;
//using IGShowcase.GeographicMapBrowser.WeatherServiceReference;

namespace IGShowcase.GeographicMapBrowser.ViewModels
{
    //TODO-MT WeatherNoaaService     
    public class WeatherNoaaData
    {
        public double Temperature { get; set; }
    }

    //TODO-MT WeatherNoaaService     
    public class WeatherDataViewSource : GeoDataViewSource
    {
        public WeatherDataViewSource()
        {
            this.DataSource = new List<WeatherNoaaData>();
            //this.DataCategory = GeoDataCategory.WorldCities;
            this.DataType = GeoDataType.Locations;
            this.FilterSettings = new WeatherDataFilterSettings();
            this.SortSettings = new WeatherDataSortSettings();
            this.PropertyChanged += OnViewSourcePropertyChanged;
        }
        private void OnViewSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DataSource")
            {
                var min = double.MaxValue;
                var max = double.MinValue;
                if (DataSource.Count > 0)
                {
                //    min = -50.0;
                //    max = 50.0;
                //}
                //else
                //{
                    foreach (var item in DataSource)
                    {
                        min = System.Math.Min(min, item.Temperature);
                        max = System.Math.Max(max, item.Temperature);
                    }
                    this.FilterView.ValueMin = min;
                    this.FilterView.ValueMax = max;
                }
            }
        }
        private List<WeatherNoaaData> _dataSource;
        /// <summary>
        /// Gets or sets DataSource property 
        /// </summary>
        public List<WeatherNoaaData> DataSource
        {
            get { return _dataSource; }
            set { if (_dataSource == value) return; _dataSource = value; OnPropertyChanged("DataSource"); }
        }

        public WeatherDataFilterSettings FilterView
        {
            get { return this.FilterSettings as WeatherDataFilterSettings; }
            set { this.FilterSettings = value; }
        }
    }
    public class WeatherDataSortSettings : GeoDataSortSettings
    {
        public WeatherDataSortSettings()
        {
            this.SortPropertyName = "Temperature";
            this.SortDirection = ListSortDirection.Descending;
        }
    }
    public class WeatherDataFilterSettings : GeoDataFilterSettings //ObservableModel, IGeoDataFilterSettings //GeoDataFilterSettings
    {
        public WeatherDataFilterSettings()
        {
            this.ValueMin = -30;
            this.ValueMax = 50;
            //this.DepthMin = 0;
            //this.DepthMax = double.MaxValue; // 10000;
            //this.DateMin = DateTime.MinValue;
            //this.DateMax = DateTime.MaxValue;
        }
        public override bool Filter(object item) // 
        {
            var data = item as WeatherNoaaData;
            if (data == null) return false;
            
            return data.Temperature >= this.ValueMin &&
                   data.Temperature <= this.ValueMax;
            //return eq.Magnitude >= this.MagnitudeMin && eq.Depth >= this.DepthMin && eq.Updated >= this.DateMin &&
            //       eq.Magnitude <= this.MagnitudeMax && eq.Depth <= this.DepthMax && eq.Updated <= this.DateMax;
        }
        #region Properties


        private double _valueMin;
        /// <summary>
        /// Gets or sets ValueMin property 
        /// </summary>
        public double ValueMin
        {
            get { return _valueMin; }
            set { if (_valueMin == value) return; _valueMin = value; OnPropertyChanged("ValueMin"); }
        }
        private double _valueMax;
        /// <summary>
        /// Gets or sets ValueMax property 
        /// </summary>
        public double ValueMax
        {
            get { return _valueMax; }
            set { if (_valueMax == value) return; _valueMax = value; OnPropertyChanged("ValueMax"); }
        }

        #endregion
    }
}