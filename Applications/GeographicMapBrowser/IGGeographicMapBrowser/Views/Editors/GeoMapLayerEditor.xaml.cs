using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using IGMapBrowser.Models;

namespace IGShowcase.GeographicMapBrowser.Views
{
    public partial class GeoMapLayerEditor : UserControl
    {
        public GeoMapLayerEditor()
        {
            InitializeComponent();
        }

        public const string MapLayerPropertyName = "MapLayer";
        public static readonly DependencyProperty MapLayerProperty = DependencyProperty.Register(
            MapLayerPropertyName, typeof(GeoMapDataLayer), typeof(GeoMapLayerEditor), new PropertyMetadata(null, (sender, e) =>
            {
                (sender as GeoMapLayerEditor).OnPropertyChanged(new PropertyChangedEventArgs(MapLayerPropertyName));
            }));
        /// <summary>
        /// Gets or sets the MapLayer property 
        /// </summary>
        public GeoMapDataLayer MapLayer
        {
            get { return (GeoMapDataLayer)GetValue(MapLayerProperty); }
            set { SetValue(MapLayerProperty, value); }
        }
        //private void OnPropertyChanged(PropertyChangedEventArgs ea)
        //{
        //    if (ea.PropertyName == MapLayerPropertyName)
        //        this.DataContext = this.MapLayer;
        //}

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, ea);
            }

            if (ea.PropertyName == MapLayerPropertyName)
                this.DataContext = this.MapLayer;
     
        }
    }
}
