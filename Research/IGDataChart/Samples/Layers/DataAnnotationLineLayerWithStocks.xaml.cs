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
    public partial class DataAnnotationLineLayerWithStocks : Infragistics.Samples.Framework.SampleContainer
    { 
        public DataAnnotationLineLayerWithStocks()
        {
            InitializeComponent();
            UseDefaultTheme = true;

            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var dataSource = new StockTeslaData();

            this.Chart.DataContext = dataSource;

            this.Chart.Series.Add(CreateStock52WeekRange(yAxisRight));
            this.Chart.Series.Add(CreateStockGrowthAndDecline(xAxis));

            var tooltip = new DataToolTipLayer();
            tooltip.IncludedColumns = new string[] { "High", "Low", "Open", "Close" };
            tooltip.LayoutMode = DataLegendLayoutMode.Vertical;

            this.Chart.Series.Add(tooltip);
        }

        public static Series CreateStock52WeekRange(Axis targetAxis)
        {
            var annoLayer = new DataAnnotationLineLayer();
            annoLayer.StartLabelXDisplayMode = DataAnnotationDisplayMode.Hidden;
            annoLayer.StartLabelYDisplayMode = DataAnnotationDisplayMode.DataValue;
            annoLayer.EndLabelXDisplayMode = DataAnnotationDisplayMode.Hidden;
            annoLayer.EndLabelYDisplayMode = DataAnnotationDisplayMode.DataValue;
            annoLayer.CenterLabelXDisplayMode = DataAnnotationDisplayMode.Hidden;
            annoLayer.Brush = new SolidColorBrush(Colors.Purple);
            annoLayer.Outline = new SolidColorBrush(Colors.Purple);
            annoLayer.OverlayTextColor = new SolidColorBrush(Colors.Purple);
            annoLayer.OverlayTextMemberPath = "Label";
            annoLayer.AreaFillOpacity = 0.1;
            annoLayer.StartValueXMemberPath = "StartX";
            annoLayer.StartValueYMemberPath = "StartY";
            annoLayer.EndValueXMemberPath = "EndX";
            annoLayer.EndValueYMemberPath = "EndY";
            annoLayer.TargetAxis = targetAxis;
            annoLayer.ItemsSource = new List<DataAnnotation>
            {
                new DataAnnotation() {
                    StartX = 190, StartY = 138,
                    EndX = 230, EndY = 138,
                    Label = "52-Week Low" },

                new DataAnnotation() {
                    StartX = 190, StartY = 481,
                    EndX = 230, EndY = 481,
                    Label = "52-Week High" },
            };
            return annoLayer;
        }

        public static Series CreateStockGrowthAndDecline(Axis targetAxis)
        {
            var annoLayer = new DataAnnotationLineLayer();
            annoLayer.StartLabelXDisplayMode = DataAnnotationDisplayMode.Hidden;
            annoLayer.EndLabelXDisplayMode = DataAnnotationDisplayMode.Hidden;
            annoLayer.CenterLabelXDisplayMode = DataAnnotationDisplayMode.Hidden;
            annoLayer.AnnotationBackgroundMode = AnnotationAppearanceMode.BrightnessShift;
            annoLayer.Brush = new SolidColorBrush(Colors.Purple);
            annoLayer.OverlayTextColor = new SolidColorBrush(Colors.Purple);
            annoLayer.OverlayTextMemberPath = "Label";
            annoLayer.OverlayTextHorizontalMargin = 55;
            annoLayer.StartValueXMemberPath = "StartX";
            annoLayer.StartValueYMemberPath = "StartY";
            annoLayer.EndValueXMemberPath = "EndX";
            annoLayer.EndValueYMemberPath = "EndY";
            annoLayer.TargetAxis = targetAxis;
            annoLayer.ItemsSource = new List<DataAnnotation>
            {
                new DataAnnotation() {
                    StartX = 48, StartY = 25,
                    EndX = 105, EndY = 250,
                    Label = "Growth &\nSupport" },

                new DataAnnotation() {
                    StartX = 108, StartY = 440,
                    EndX = 155, EndY = 210,
                    Label = "Decline &\nResistance" },
            };
            return annoLayer;
        }

    }
}