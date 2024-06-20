using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using IGGeographicMap.Resources;              // MapStrings
using Infragistics.Controls;
using Infragistics.Samples.Shared.Models;     // ThemesViewModel, Theme
using Infragistics.Themes;

namespace IGGeographicMap.Samples.Styling
{
    public partial class Theming : Infragistics.Samples.Framework.SampleContainer
    {
        public Theming()
        {
            this.UseDefaultTheme = true;
            InitializeComponent();   
        }      

        private void OnShapefileImportCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            // zoom in geo-map a specific geographic region of the world
            this.GeoMap.NavigateTo(GeoRegions.WorldNonAntarcticRegion); // using GeoMapAdapter class
            this.GeoMap.Visibility = System.Windows.Visibility.Visible;
        }             
         
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {  
            var theme = this.ThemeSelector.SelectedItem as ThemeInfo;           
            if (theme == null) return;

            // apply selected theme 
            ThemeManager.SetTheme(this.LayoutRoot, theme.Resources);
 
            // apply a map preview that matches selected theme
            var opdStyle = new Style(typeof(XamOverviewPlusDetailPane));
            if (!theme.IsDefaultTheme) opdStyle.BasedOn = this.Resources["OverviewPlusDetailPaneStyle"] as Style;  

            var worldStyle = new Style(typeof(Path));
            worldStyle.Setters.Add(new Setter(Shape.StrokeProperty, new SolidColorBrush(Colors.Gray)));
            worldStyle.Setters.Add(new Setter(Shape.StrokeThicknessProperty, 0.5));

            ImageBrush mapPreview = null;
            if (theme.ID == "MetroLight")
            {
                mapPreview = this.Resources["metroLightThemeMap"] as ImageBrush;
            }
            else if (theme.ID == "MetroDark")
            {
                mapPreview = this.Resources["metroDarkThemeMap"] as ImageBrush;
            }
            else if (theme.ID == "IG")
            {
                mapPreview = this.Resources["igThemeMap"] as ImageBrush;
            }
            else if (theme.ID == "Office2010")
            {
                mapPreview = this.Resources["office2010ThemeMap"] as ImageBrush;
            }
            else if (theme.ID == "Office2013")
            {
                mapPreview = this.Resources["office2013ThemeMap"] as ImageBrush;
            }
            else if (theme.ID == "Default")
            {
                mapPreview = this.Resources["defaultThemeMap"] as ImageBrush;
            }
            else if (theme.ID == "RoyalDark")
            {
                mapPreview = this.Resources["royalDarkThemeMap"] as ImageBrush;
            }

            if (mapPreview != null)
                worldStyle.Setters.Add(new Setter(Shape.FillProperty, mapPreview));
      
            opdStyle.Setters.Add(new Setter(XamOverviewPlusDetailPane.ZoomTo100ButtonVisibilityProperty, Visibility.Collapsed));
            opdStyle.Setters.Add(new Setter(XamOverviewPlusDetailPane.InteractionStatesToolVisibilityProperty, Visibility.Visible));
            opdStyle.Setters.Add(new Setter(XamOverviewPlusDetailPane.ScaleToFitButtonToolTipProperty, MapStrings.XWGM_OPD_ScaleToFitButtonToolTip));
            opdStyle.Setters.Add(new Setter(XamOverviewPlusDetailPane.ZoomLevelLargeChangeProperty, 0.2));
            opdStyle.Setters.Add(new Setter(XamOverviewPlusDetailPane.WorldStyleProperty, worldStyle));
            this.GeoMap.OverviewPlusDetailPaneStyle = opdStyle;
        }
    }
}