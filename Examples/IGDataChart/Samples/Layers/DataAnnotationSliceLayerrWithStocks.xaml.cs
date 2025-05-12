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
    public partial class DataAnnotationSliceLayerrWithStocks : Infragistics.Samples.Framework.SampleContainer
    { 
        public DataAnnotationSliceLayerrWithStocks()
        {
            InitializeComponent();
            UseDefaultTheme = true;

            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var dataSource = new StockTeslaData();

            this.Chart.DataContext = dataSource;

            this.Chart.Series.Add(CreateStockSplitAnnotations(xAxisBottom));
            this.Chart.Series.Add(CreateStockEarningsMissAnnotations(xAxisBottom));
            this.Chart.Series.Add(CreateStockEarningsBeatAnnotations(xAxisBottom));

            var tooltip = new DataToolTipLayer();
            tooltip.IncludedColumns = new string[] { "High", "Low", "Open", "Close" };
            tooltip.LayoutMode = DataLegendLayoutMode.Vertical;

            this.Chart.Series.Add(tooltip);
        }

        public static Series CreateStockSplitAnnotations(Axis targetAxis)
        {
            var annoLayer = new DataAnnotationSliceLayer();
            annoLayer.Brush = new SolidColorBrush(Colors.DodgerBlue);
            annoLayer.TargetAxis = targetAxis;
            annoLayer.OverlayTextMemberPath = "Label";
            annoLayer.OverlayTextLocation = OverlayTextLocation.OutsideTopRight;
            annoLayer.OverlayTextAngle = 90;
            annoLayer.OverlayTextHorizontalMargin = 0;
            annoLayer.AnnotationLabelMemberPath = "Label";
            annoLayer.AnnotationValueMemberPath = "Value";
            annoLayer.AnnotationTextColor = new SolidColorBrush(Colors.White);
            annoLayer.ItemsSource = new List<DataAnnotation>
            {
                new DataAnnotation() { Value = 126, Label = "Stock Split 3-1" },
                new DataAnnotation() { Value = 61, Label = "Stock Split 5-1" },
            };
            return annoLayer;
        }

        public static Series CreateStockEarningsMissAnnotations(Axis targetAxis)
        {
            var annoLayer = new DataAnnotationSliceLayer();
            annoLayer.Brush = new SolidColorBrush(Colors.Red);
            annoLayer.TargetAxis = targetAxis;
            annoLayer.OverlayTextMemberPath = "Label";
            annoLayer.OverlayTextLocation = OverlayTextLocation.OutsideTopRight;
            annoLayer.OverlayTextAngle = 90;
            annoLayer.OverlayTextHorizontalMargin = 0;
            annoLayer.AnnotationLabelMemberPath = "Label";
            annoLayer.AnnotationValueMemberPath = "Value";
            annoLayer.AnnotationTextColor = new SolidColorBrush(Colors.White);
            annoLayer.ItemsSource = new List<DataAnnotation>
            {
                new DataAnnotation() { Value = 9, Label = "Earnings Miss" },
                new DataAnnotation() { Value = 179, Label = "Earnings Miss" },
                //new DataAnnotation() { Value = 215, Label = "Earning Miss" },
            };
            return annoLayer;
        }

        public static Series CreateStockEarningsBeatAnnotations(Axis targetAxis)
        {
            var annoLayer = new DataAnnotationSliceLayer();
            annoLayer.Brush = new SolidColorBrush(Colors.Green);
            annoLayer.TargetAxis = targetAxis;
            annoLayer.OverlayTextMemberPath = "Label";
            annoLayer.OverlayTextLocation = OverlayTextLocation.OutsideTopRight;
            annoLayer.OverlayTextAngle = 90;
            annoLayer.OverlayTextHorizontalMargin = 0;
            annoLayer.AnnotationLabelMemberPath = "Label";
            annoLayer.AnnotationValueMemberPath = "Value";
            annoLayer.AnnotationTextColor = new SolidColorBrush(Colors.White);
            annoLayer.ItemsSource = new List<DataAnnotation>
            {
                new DataAnnotation() { Value = 155, Label = "Earnings Beat" },
                new DataAnnotation() { Value = 86, Label = "Earnings Beat" },
                new DataAnnotation() { Value = 28, Label = "Earnings Beat" },
            };
            return annoLayer;
        }
    }
}