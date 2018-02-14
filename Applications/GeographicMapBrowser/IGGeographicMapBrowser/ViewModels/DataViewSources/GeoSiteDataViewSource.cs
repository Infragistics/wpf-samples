using System.Collections.Generic;
using System.ComponentModel;
using IGExtensions.Common.Maps.DataSelectors;
using IGExtensions.Common.Models;

namespace IGShowcase.GeographicMapBrowser.ViewModels
{
    public class GeoSiteDataViewSource : GeoDataViewSource
    {
        public GeoSiteDataViewSource()
        {
            this.DataSource = new List<GeoSite>();
            //this.DataCategory = GeoDataCategory.Unknown;
            this.DataType = GeoDataType.Locations;
            this.PropertyChanged += OnViewSourcePropertyChanged;
        }

        private List<GeoSite> _dataSource;
        /// <summary>
        /// Gets or sets actual DataSource   
        /// </summary>
        public List<GeoSite> DataSource
        {
            get { return _dataSource; }
            set { if (_dataSource == value) return; _dataSource = value; OnPropertyChanged("DataSource"); }
        }
        private void OnViewSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DataSource")
            {
                if (this.DataSource == null || this.DataSource.Count == 0) return;

                var geoLocations = new List<IGeoLocatable>();
                foreach (var item in DataSource)
                {
                    geoLocations.Add(item);
                }
                this.UpdateDataWorldRect(geoLocations);
            }
        }

    }

  
}