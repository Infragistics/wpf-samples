using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework; 

namespace IGShapeChart.Samples
{
    public partial class ChartLegend : SampleContainer
    {  
        public ChartLegend()
        {
            InitializeComponent();
             
        }
        
        private void OnSeriesAdded(object sender, ChartSeriesEventArgs args)
        {
            var series = args.Series;
            series.Title = series.SeriesViewer.Title;
        }
    }
}
