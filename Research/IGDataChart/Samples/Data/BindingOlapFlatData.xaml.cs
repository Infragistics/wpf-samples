using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;

namespace IGDataChart.Samples.Data
{
    public partial class BindingOlapFlatData : SampleContainer
    {
        public BindingOlapFlatData()
        {
            InitializeComponent();
        }

        // Create a tooltip for each of the series that are created
        private void OlapXAxis_SeriesCreating(object sender, Infragistics.Controls.Charts.SeriesCreatingEventArgs e)
        {
            TextBlock titleTextBlock = new TextBlock();
            Binding seriesTitleBinding = new Binding("Series.Title");
            titleTextBlock.SetBinding(TextBlock.TextProperty, seriesTitleBinding);

            TextBlock valueTextBlock = new TextBlock();
            Binding cellValueBinding = new Binding("Item.Cell.FormattedValue");
            cellValueBinding.StringFormat = ": {0}";
            valueTextBlock.SetBinding(TextBlock.TextProperty, cellValueBinding);

            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;
            sp.Children.Add(titleTextBlock);
            sp.Children.Add(valueTextBlock);
            if (e.Series is LineSeries ||
                e.Series is PointSeries ||
                e.Series is StepLineSeries)
            {
                e.Series.MarkerType = MarkerType.Circle;
            }

            e.Series.ToolTip = sp;
        }
    }

    public class CarsSalesDataProvider
    {
        public CarsSalesDataProvider()
        {
            this.Data = new ObservableCollection<CarsSales>(CarsSales.GenerateData());
        }

        public ObservableCollection<CarsSales> Data { get; set; }
    }
}
