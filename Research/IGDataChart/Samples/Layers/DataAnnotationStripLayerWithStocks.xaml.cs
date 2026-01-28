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
    public partial class DataAnnotationStripLayerWithStocks : Infragistics.Samples.Framework.SampleContainer
    { 
        public DataAnnotationStripLayerWithStocks()
        {
            InitializeComponent();
            UseDefaultTheme = true;

            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var dataSource = new StockTeslaData();

            this.Chart.DataContext = dataSource;

            this.Chart.Series.Add(CreateStockMarketEvents(xAxisTop));

            var tooltip = new DataToolTipLayer();
            tooltip.IncludedColumns = new string[] { "High", "Low", "Open", "Close" };
            tooltip.LayoutMode = DataLegendLayoutMode.Vertical;

            this.Chart.Series.Add(tooltip);
        }

        public static Series CreateStockMarketEvents(Axis targetAxis)
        {
            var annoLayer = new DataAnnotationStripLayer();
            annoLayer.EndLabelDisplayMode = DataAnnotationDisplayMode.Hidden;
            annoLayer.StartLabelDisplayMode = DataAnnotationDisplayMode.Hidden;
            annoLayer.Brush = new SolidColorBrush(Colors.Black);
            annoLayer.Outline = new SolidColorBrush(Colors.Black);
            annoLayer.CenterLabelMemberPath = "Label";
            annoLayer.StartValueMemberPath = "Start";
            annoLayer.EndValueMemberPath = "End";
            annoLayer.TargetAxis = targetAxis;
            annoLayer.ItemsSource = new List<DataAnnotation>
            {
                new DataAnnotation() { Start = 40, End = 45, Label = "Covid - Market Crash" },
                new DataAnnotation() { Start = 100, End = 144, Label = "Fed Rate Up  0.25 - 5.25%"  },
                new DataAnnotation() { Start = 190, End = 205, Label = "Fed Rate Down 5.25% to 4.45%"  },
            };
            return annoLayer;
        }
    }
}