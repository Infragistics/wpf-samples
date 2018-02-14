using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using IGExtensions.Common.Assets;
using IGExtensions.Common.Assets.Resources;
using IGExtensions.Common.Controls;

namespace MediaTimeline.Views
{
    public partial class About : UserControl
    {
        public About()
        {
            InitializeComponent();
#if SILVERLIGHT
            var htmlViewer = new NavigationSampleLink { Component =  InfragisticsComponents.HtmlViewer};
            htmlViewer.Foreground = App.Current.Resources["ForegroundDark"] as Brush;
            ComponentListPanel.Children.Add(htmlViewer);
#endif
        }

        public Size GetDesiredSize()
        {
            this.Measure(new Size(750, double.PositiveInfinity));
            return this.DesiredSize;
        }
    }
}
