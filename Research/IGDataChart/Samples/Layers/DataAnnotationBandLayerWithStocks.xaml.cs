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
    public partial class DataAnnotationBandLayerWithStocks : Infragistics.Samples.Framework.SampleContainer
    { 
        public DataAnnotationBandLayerWithStocks()
        {
            InitializeComponent();
            UseDefaultTheme = true;

            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var dataSource = new StockTeslaData();

            this.Chart.DataContext = dataSource;

            this.Chart.Series.Add(CreateStockRapidGrowth(xAxisBottom));

            var tooltip = new DataToolTipLayer();
            tooltip.IncludedColumns = new string[] { "High", "Low", "Open", "Close" };
            tooltip.LayoutMode = DataLegendLayoutMode.Vertical;

            this.Chart.Series.Add(tooltip);
        }

        public static Series CreateStockRapidGrowth(Axis targetAxis)
        {
            var annoLayer = new DataAnnotationBandLayer();
            annoLayer.CenterLabelXDisplayMode = DataAnnotationDisplayMode.Hidden;
            annoLayer.StartLabelXDisplayMode = DataAnnotationDisplayMode.DataLabel;
            annoLayer.EndLabelXDisplayMode = DataAnnotationDisplayMode.DataLabel;
            annoLayer.Brush = new SolidColorBrush(Colors.Purple);
            annoLayer.Outline = new SolidColorBrush(Colors.Purple);
            annoLayer.OverlayTextColor = new SolidColorBrush(Colors.Purple);
            annoLayer.OverlayTextMemberPath = "Label";
            annoLayer.OverlayTextVerticalMargin = 45;
            annoLayer.OverlayTextHorizontalMargin = -25;
            annoLayer.OverlayTextLocation = OverlayTextLocation.InsideTopCenter;
            annoLayer.OverlayTextMemberPath = "Label";
            annoLayer.StartLabelXMemberPath = "StartLabel";
            annoLayer.EndLabelXMemberPath = "EndLabel";

            annoLayer.StartValueXMemberPath = "StartX";
            annoLayer.StartValueYMemberPath = "StartY";
            annoLayer.EndValueXMemberPath = "EndX";
            annoLayer.EndValueYMemberPath = "EndY";
            annoLayer.AnnotationBreadthMemberPath = "Value";
            annoLayer.TargetAxis = targetAxis;
            annoLayer.ItemsSource = new List<DataAnnotation>
            {
                new DataAnnotation() {
                    StartLabel = "Growth Start",
                    EndLabel = "Growth Stop",
                    StartX = 48, StartY = 110,
                    EndX = 105, EndY = 335,
                    Value = 170,
                    Label = "Rapid Growth" },
            };
            return annoLayer;
        }
    }
}