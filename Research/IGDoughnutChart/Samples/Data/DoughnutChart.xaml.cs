using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework;
using System.Windows.Data;

namespace IGDoughnutChart.Samples
{
    public partial class DoughnutChart : SampleContainer
    {
        public DoughnutChart()
        {
            InitializeComponent();
            //this.Loaded += (s, e) =>
            //    doughnutChart.Rings[0].ArcItems[0].SliceItems[0].Slice.IsExploded = true;
        }

        private void DoughnutChart_SliceClick(object sender, SliceClickEventArgs e)
        {
            e.IsExploded = !e.IsExploded;
        }
    }


    // Simple converter used to disable the LabelExtent slider when the LabelPosition is not OutsideEnd
    public class LabelsPositionToBoolConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is LabelsPosition && (LabelsPosition)value == LabelsPosition.OutsideEnd)
            {
                return true;
            }

            return false;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }

}
