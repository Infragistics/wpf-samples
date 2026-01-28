using System; 
using System.Linq;
using System.Windows;
using System.Windows.Controls; 
using System.Windows.Media;
using System.Collections.Generic;

using Infragistics.Controls.Charts;
using IGDataChart.Models;

namespace IGDataChart.Samples.Layers
{
    public partial class DataAnnotationRectLayerWithStocks : Infragistics.Samples.Framework.SampleContainer
    { 
        public DataAnnotationRectLayerWithStocks()
        {
            InitializeComponent();
            UseDefaultTheme = true;

            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var dataSource = new StockTeslaData();

            this.Chart.DataContext = dataSource;

            this.Chart.Series.Add(CreateStockBearishPatterns(xAxis));

            var tooltip = new DataToolTipLayer();
            tooltip.IncludedColumns = new string[] { "High", "Low", "Open", "Close" };
            tooltip.LayoutMode = DataLegendLayoutMode.Vertical;

            this.Chart.Series.Add(tooltip);
        }

        public static Series CreateStockBearishPatterns(Axis targetAxis)
        {
            var annoLayer = new DataAnnotationRectLayer();
            annoLayer.StartLabelXDisplayMode = DataAnnotationDisplayMode.Hidden;
            annoLayer.StartLabelYDisplayMode = DataAnnotationDisplayMode.Hidden;
            annoLayer.EndLabelXDisplayMode = DataAnnotationDisplayMode.Hidden;
            annoLayer.EndLabelYDisplayMode = DataAnnotationDisplayMode.Hidden;
            annoLayer.CenterLabelXDisplayMode = DataAnnotationDisplayMode.Hidden;
            annoLayer.Brush = new SolidColorBrush(Colors.Purple);
            annoLayer.Outline = new SolidColorBrush(Colors.Purple);
            annoLayer.OverlayTextMemberPath = "Label";
            annoLayer.OverlayTextLocation = OverlayTextLocation.OutsideBottomCenter;
            annoLayer.OverlayTextVerticalMargin = 15;
            annoLayer.AreaFillOpacity = 0.1;
            annoLayer.StartValueXMemberPath = "StartX";
            annoLayer.StartValueYMemberPath = "StartY";
            annoLayer.EndValueXMemberPath = "EndX";
            annoLayer.EndValueYMemberPath = "EndY";
            annoLayer.TargetAxis = targetAxis;
            annoLayer.ItemsSource = new List<DataAnnotation>
            {
                new DataAnnotation() {
                    StartX = 85, StartY = 190,
                    EndX = 140, EndY = 415,
                    Label = "Head & Shoulders Pattern \n  (Bearish Downtrend)" },

                new DataAnnotation() {
                    StartX = 53, StartY = 75,
                    EndX = 230, EndY = 80,
                    Label = "Price Gap (Bearish Target)" },
            };
            return annoLayer;
        }
    }
}