using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using IGExtensions.Common.Controls;
using Infragistics.Controls.Maps;

namespace IGExtensions.Common.Maps.Controls
{
    public class GeoMapPane : MovableControl, INotifyPropertyChanged
    {
        public GeoMapPane()
        {
            DefaultStyleKey = typeof(GeoMapPane);

            this.IsMovable = true;
        }

   
        #region Properties

        public const string MapControlPropertyName = "MapControl";
        /// <summary>
        /// Identifies the GeographicMap dependency property.
        /// </summary>
        public static readonly DependencyProperty MapControlProperty = DependencyProperty.Register(
                MapControlPropertyName, typeof(XamGeographicMap), typeof(GeoMapPane),
                new PropertyMetadata(null, (sender, e) =>
                {
                    (sender as GeoMapPane).OnPropertyChanged(new PropertyChangedEventArgs(MapControlPropertyName));
                }));

        /// <summary>
        /// Sets or gets the MapControl property.
        /// </summary>
        public XamGeographicMap MapControl
        {
            get { return (XamGeographicMap)GetValue(MapControlProperty); }
            set { SetValue(MapControlProperty, value); }
        }

        public const string MapPaneOrientationPropertyName = "MapPaneOrientation";
        /// <summary>
        /// Identifies the MapPaneOrientation dependency property.
        /// </summary>
        public static readonly DependencyProperty MapPaneOrientationProperty =
            DependencyProperty.Register(MapPaneOrientationPropertyName, typeof(Orientation), typeof(GeoMapPane),
             new PropertyMetadata(Orientation.Vertical, (sender, e) =>
             {
                 (sender as GeoMapPane).OnPropertyChanged(new PropertyChangedEventArgs(MapPaneOrientationPropertyName));
             }));

        /// <summary>
        /// Gets or sets orientation of the Map Pane
        /// </summary>
        public Orientation MapPaneOrientation
        {
            get { return (Orientation)GetValue(MapPaneOrientationProperty); }
            set { SetValue(MapPaneOrientationProperty, value); }
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        public virtual void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, ea);
            }

           // if (this.IsUpdating) return;


            //switch (ea.PropertyName)
            //{
            
                 
            //}
        }
        #endregion
  
    }
}