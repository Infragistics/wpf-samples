using System.Windows.Media;
using System.Windows.Shapes;
using Infragistics.Controls;

namespace IGGeographicMap.Samples
{
    public partial class MapOverviewPane : Infragistics.Samples.Framework.SampleContainer
    {
        public MapOverviewPane()
        {
            InitializeComponent();
        }
        private void OpdHorizontalAlignmentComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var alignment = (System.Windows.HorizontalAlignment)e.AddedItems[0];
            if (this.GeoMap != null)
                this.GeoMap.OverviewPlusDetailPaneHorizontalAlignment = alignment;
        }
        private void OpdVerticalAlignmentComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var alignment = (System.Windows.VerticalAlignment)e.AddedItems[0];
            if (this.GeoMap != null) 
                this.GeoMap.OverviewPlusDetailPaneVerticalAlignment = alignment;
        }
        private void ThumbnailImagesListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var imageBrush = e.AddedItems[0] as ImageBrush;
            if (imageBrush == null) return;
           
            var worldPath = this.Resources["OPDWorldStyle"] as System.Windows.Style;
            if (worldPath == null) return;

            var newWorldStyle = new System.Windows.Style { TargetType = typeof(Path), BasedOn = worldPath };
            newWorldStyle.Setters.Add(new System.Windows.Setter(Shape.FillProperty, imageBrush));
            
            var opdStyle = this.Resources["OPDStyle"] as System.Windows.Style;
            if (opdStyle == null) return;

            var newOpdStyle = new System.Windows.Style { TargetType = typeof(XamOverviewPlusDetailPane), BasedOn = opdStyle };
            newOpdStyle.Setters.Add(new System.Windows.Setter(XamOverviewPlusDetailPane.WorldStyleProperty, newWorldStyle));

            this.GeoMap.OverviewPlusDetailPaneStyle = newOpdStyle;
        }
    }

 
}
