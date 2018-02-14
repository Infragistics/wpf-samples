using System;
using System.Collections.ObjectModel;
using System.Windows;
using IGExtensions.Common.Models;
using IGExtensions.Framework;

namespace IGShowcase.GeographicMapBrowser.Models
{
     
    public abstract class MapLayer : ObservableModel // MapElementMapLayer
    {
        protected MapLayer()
        {
            this.Visibility = Visibility.Visible;
            this.Opacity = 1.0;
            this.IsOrderEditable = true;
            this.IsMapLayerEditable = true;
            //this.Order++;
        }

        #region Properties

        private string _title;
        public string Title
        {
            get { return _title; }
            set { if (_title == value) return; _title = value; OnPropertyChanged("Title"); }
        }

        private Visibility _visibility;
        public Visibility Visibility
        {
            get { return _visibility; }
            set { if (_visibility == value) return; _visibility = value; 
                OnPropertyChanged("Visibility"); OnPropertyChanged("IsVisible"); }
        }

        //private bool _isVisible;
        /// <summary>
        /// Gets or sets IsVisible property 
        /// </summary>
        public bool IsVisible
        {
            get { return this.Visibility == Visibility.Visible; }
            set 
            { 
                if (this.IsVisible == value) return;
                this.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
                OnPropertyChanged("IsVisible");
            }
        }

        private double _opacity;
        public double Opacity
        {
            get { return _opacity; }
            set { if (Math.Abs(_opacity - value) < 0.01) return; _opacity = value; OnPropertyChanged("Opacity"); }
        }

        private int _order;
        public int Order
        {
            get { return _order; }
            set { if (_order == value) return; _order = value; OnPropertyChanged("Order"); }
        }

        private bool _isOrderEditable;
        public bool IsOrderEditable
        {
            get { return _isOrderEditable; }
            protected set { if (_isOrderEditable == value) return; _isOrderEditable = value; OnPropertyChanged("IsOrderEditable"); }
        }

        private bool _isMapLayerEditable;
        public bool IsMapLayerEditable
        {
            get { return _isMapLayerEditable; }
            protected set { if (_isMapLayerEditable == value) return; _isMapLayerEditable = value; OnPropertyChanged("IsMapLayerEditable"); }
        } 
        #endregion
    }
    public class MapLayers : ObservableCollection<MapLayer>
    {

    }
}