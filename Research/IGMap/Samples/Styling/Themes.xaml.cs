using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using IGMap.Models;                                 // MapAdapter
using Infragistics.Controls;                        // XamDock
using Infragistics.Controls.Maps;                   // MapThumbnailPane
using Infragistics.Samples.Shared.DataProviders;    // ImageFilePathProvider
using Infragistics.Samples.Shared.Models;           // ThemesViewModel, Theme, GeoRegions
using Infragistics.Themes;

namespace IGMap.Samples.Styling
{
    public partial class Themes : Infragistics.Samples.Framework.SampleContainer
    {
        public Themes()
        {
            InitializeComponent();

            ThumbnailImage = new BitmapImage();

            ThumbnailPane = new MapThumbnailPane();
            ThumbnailPane.SetValue(XamDock.EdgeProperty, DockEdge.InsideRight);
            ThumbnailPane.SetValue(XamDock.VerticalDockAlignmentProperty, VerticalAlignment.Bottom);
            ThumbnailPane.Width = 200;
            ThumbnailPane.Height = 100;
            ThumbnailPane.Margin = new Thickness(10);
             
            // initialize map boundary to specific map region
            MapAdapter mapAdapter = new MapAdapter(this.igMap);
            mapAdapter.SetMapBoundary(GeoRegions.WorldFullRegion);
            mapAdapter.ZoomMapToRegion(GeoRegions.WorldNonPolarRegion);
        }

        protected MapThumbnailPane ThumbnailPane;
        protected BitmapImage ThumbnailImage;
         
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {  
            var theme = this.ThemeSelector.SelectedItem as ThemeInfo;           
            if (theme == null) return;

            // apply selected theme 
            ThemeManager.SetTheme(this.LayoutRoot, theme.Resources);
            
            // apply thumbnail image that machtes selected theme
            string path;

            if (theme.ID == "OfficeBlue")
            {
                path = ImageFilePathProvider.GetImageLocalPath("/maps/tinyWorldBlue.png");
            }
            else if (theme.ID == "Metro")
            {
                path = ImageFilePathProvider.GetImageLocalPath("/maps/tinyWorldIG.png");
            }
            else if (theme.ID == "MetroDark")
            {
                path = ImageFilePathProvider.GetImageLocalPath("/maps/tinyWorldIG.png");
            }
            else if (theme.ID == "IG")
            {
                path = ImageFilePathProvider.GetImageLocalPath("/maps/tinyWorldIG.png");
            }
            else
            {
                path = ImageFilePathProvider.GetImageLocalPath("/maps/tinyWorld.png");
            }
            var thumbnailImage = ImageFilePathProvider.CreateBitmapImage(path);
            
            var imgBrush = new ImageBrush();
            imgBrush.ImageSource = thumbnailImage;

            var worldStyle = new Style(typeof(Path));
            worldStyle.Setters.Add(new Setter(Path.FillProperty, imgBrush));
            ThumbnailPane.WorldStyle = worldStyle;
            igMap.LogicalChildren.Add(ThumbnailPane);
        } 
    }
}