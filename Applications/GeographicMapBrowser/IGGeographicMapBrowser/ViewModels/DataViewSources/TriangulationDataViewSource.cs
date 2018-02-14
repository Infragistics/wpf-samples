using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using IGExtensions.Common.Frameworks;
using IGExtensions.Common.Maps.DataSelectors;
using IGExtensions.Common.Models;
using Infragistics.Controls.Charts;

namespace IGShowcase.GeographicMapBrowser.ViewModels
{
    public class TriangulationDataViewSource : GeoDataViewSource
    {
        public TriangulationDataViewSource()
        {
            //this.DataSource = new List<GeoSite>();
            //this.DataCategory = GeoDataCategory.Unknown;
            this.DataType = GeoDataType.Triangulation;
            this.PropertyChanged += OnViewSourcePropertyChanged;
        }

        private TriangulationSource _dataSource;
        /// <summary>
        /// Gets or sets actual DataSource   
        /// </summary>
        public TriangulationSource DataSource
        {
            get { return _dataSource; }
            set { if (_dataSource == value) return; _dataSource = value; OnPropertyChanged("DataSource"); }
        }
        private void OnViewSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DataSource")
            {
                if (this.DataSource == null || this.DataSource.Points == null || this.DataSource.Points.Count == 0) return;
                //TODO update DataWorldRect based on geo-location of data items
                var geoPoints = new List<Point>();
                foreach (var item in DataSource.Points)
                {
                    geoPoints.Add(item.Point);
                }
                this.UpdateDataWorldRect(geoPoints);
            }
        }
    }
   
}