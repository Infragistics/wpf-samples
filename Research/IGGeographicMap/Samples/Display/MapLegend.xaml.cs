using System.Collections.Specialized;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using System.ComponentModel;
using System.Windows.Media.Animation;
using System.Windows;
using System;
using Infragistics.Controls.Charts;

namespace IGGeographicMap.Samples
{
    public partial class MapLegend : Infragistics.Samples.Framework.SampleContainer
    {
        public MapLegend()
        {
            InitializeComponent();

            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // zoom in geo-map a specific geographic region of the world
            this.GeoMap.NavigateTo(GeoRegions.WorldNonAntarcticRegion); // using GeoMapAdapter class
        }

        private void OnLegendItemMouseEnter(object sender, LegendMouseEventArgs e)
        {
            if (e.Series != null)
            {
                foreach (var series in this.GeoMap.Series)
                {
                    HideSeries(series); 
                }
                ShowSeries((Series)e.Series);
            }
        }

        private void OnLegendItemMouseLeave(object sender, LegendMouseEventArgs e)
        {
            if (e.Series != null)
            {
                foreach(var series in this.GeoMap.Series)
                {
                    ShowSeries(series); 
                }                
            }
        }
               
        private void HideSeries(Series series)
        {
            series.Animate("Opacity", 0.3);
        }

        private void ShowSeries(Series series)
        {
            series.Animate("Opacity", 1.0);
        }
    }

    public static class Animator
    {
        public static void Animate(this DependencyObject target, 
            string numericProperty, double endValue, double seconds = 0.3)
        {
            var animation = new DoubleAnimation
            {
                To = endValue,
                Duration = TimeSpan.FromSeconds(seconds)
            };
            var sb = new Storyboard
            {
                Duration = TimeSpan.FromSeconds(seconds)
            };
            sb.Children.Add(animation);
            Storyboard.SetTarget(animation, target);
            Storyboard.SetTargetProperty(animation, new PropertyPath(numericProperty));
            sb.Begin();
        }
    }
}
