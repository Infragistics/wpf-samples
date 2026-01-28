using System.ComponentModel;
using System.Windows;
using Infragistics.Controls.Maps;

namespace IGGeographicMap.Models
{
    public class MapShapeElement : FrameworkElement 
    {
        public MapShapeElement()
        {

            this.ShapeRecord = new ShapefileRecord();
        }
        #region ShapeRecord
        public const string ShapeRecordPropertyName = "ShapeRecord";
        /// <summary>
        /// Identifies the Shape Record dependency property.
        /// </summary>
        public static DependencyProperty ShapeRecordProperty = DependencyProperty.Register(
        ShapeRecordPropertyName, typeof(ShapefileRecord), typeof(MapShapeElement), null);

        /// <summary>
        /// Gets or sets Shape Record of the MapShapeElement
        /// </summary>
        public ShapefileRecord ShapeRecord
        {
            get { return (ShapefileRecord)GetValue(ShapeRecordProperty); }
            set { SetValue(ShapeRecordProperty, value); }
        }
        #endregion
        public void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            //switch (ea.PropertyName)
            //{
            //}
        }
    }
    public class WorldMapShapeElement : MapShapeElement
    {
        public WorldMapShapeElement()
        {
        }
        public string ShapeRegionName { get; set; }
        public string ShapeName { get; set; }
        public string ShapeCode { get; set; }

        public string GetShapeId()
        {
            return ShapeRegionName + "." + ShapeName + "." + ShapeCode;
        }
    }
}