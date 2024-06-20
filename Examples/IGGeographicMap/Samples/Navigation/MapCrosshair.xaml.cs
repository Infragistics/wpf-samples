using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using IGGeographicMap.Extensions;
using IGGeographicMap.Models;
using Infragistics.Controls.Charts;
using Infragistics.Samples.Shared.Models;

namespace IGGeographicMap.Samples
{
    public partial class MapCrosshair : Infragistics.Samples.Framework.SampleContainer
    {
        public MapCrosshair()
        {
            InitializeComponent();
            InitializeMapAxes();
            this.SampleLoaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, System.EventArgs e)
        {
            // zoom in geo-map a specific geographic region of the world
            this.GeoMap.NavigateTo(GeoRegions.WorldNonAntarcticRegion); // using GeoMapAdapter class
        }

        private void InitializeMapAxes()
        {
            this.GeoMap.XAxis.Stroke = new SolidColorBrush(Colors.Gray);
            this.GeoMap.XAxis.StrokeThickness = 0.5;
            this.GeoMap.XAxis.LabelSettings = new AxisLabelSettings { Visibility = Visibility.Visible };

            this.GeoMap.YAxis.Stroke = new SolidColorBrush(Colors.Gray);
            this.GeoMap.YAxis.StrokeThickness = 0.5;
            this.GeoMap.YAxis.LabelSettings = new AxisLabelSettings { Visibility = Visibility.Visible };
        }

        private GeoLocation GetGeoLocation(Point mousePosition)
        {
            var xAxis = this.GeoMap.XAxis;
            var yAxis = this.GeoMap.YAxis;

            var viewport = new Rect(0, 0, this.GeoMap.ActualWidth, this.GeoMap.ActualHeight);
            var window = this.GeoMap.ActualWindowRect;

            bool isInverted = xAxis.IsInverted;
            var param = new ScalerParams(window, viewport, isInverted);
            param.EffectiveViewportRect = this.GeoMap.EffectiveViewport;
            var longitude = xAxis.GetUnscaledValue(mousePosition.X, param);

            isInverted = yAxis.IsInverted;
            param = new ScalerParams(window, viewport, isInverted);
            param.EffectiveViewportRect = this.GeoMap.EffectiveViewport;
            var latitude = yAxis.GetUnscaledValue(mousePosition.Y, param);

            return new GeoLocation(longitude, latitude);
        }
        private void OnMapMouseMove(object sender, MouseEventArgs e)
        {
            // calculate Geographic location based on position of mouse cursor
            var mousePosition = e.GetPosition(this.GeoMap);
            var geoLocation = GetGeoLocation(mousePosition);
            var crosshairPoint = this.GeoMap.CrosshairPoint;

             
            if (geoLocation.Longitude >= -180 && geoLocation.Longitude <= 180)
                this.LongitudeTextBlock.Text = String.Format(": {0,7:#0.0}, ", geoLocation.Longitude);
            else
            {
                this.LongitudeTextBlock.Text = ":    ---.-";
            }

            if (geoLocation.Latitude >= -85 && geoLocation.Latitude <= 85)
                this.LatitudeTextBlock.Text = String.Format(": {0,7:#0.0}, ", geoLocation.Latitude);
            else
            {
                this.LatitudeTextBlock.Text = ":    ---.-";
            }

            this.XTextBlock.Text = String.Format("{0,5:#0.0} ({1:0.00})", mousePosition.X, crosshairPoint.X);
            this.YTextBlock.Text = String.Format("{0,5:#0.0} ({1:0.00})", mousePosition.Y, crosshairPoint.Y);
        }
         
        private void ShowVerticalMapCrosshairCheckBox_Click(object sender, RoutedEventArgs e)
        {
            bool isCrosshairVisibile = ((CheckBox)sender).IsChecked.Value;

            // update the CrosshairVisibility with new value
            GeographicMapCrosshairVisibility behavior = GeoMapExtensions.GetCrosshairVisibility(this.GeoMap);
            behavior.ShowVerticalCrosshair = isCrosshairVisibile;
            GeoMapExtensions.SetCrosshairVisibility(this.GeoMap, behavior);
        }

        private void ShowHorizontalMapCrosshairCheckBox_Click(object sender, RoutedEventArgs e)
        {
            bool isCrosshairVisibile = ((CheckBox)sender).IsChecked.Value;

            // update the CrosshairVisibility with new value
            GeographicMapCrosshairVisibility behavior = GeoMapExtensions.GetCrosshairVisibility(this.GeoMap);
            behavior.ShowHorizontalCrosshair = isCrosshairVisibile;
        }

       
    }
}
