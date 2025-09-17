namespace IGPieChart.Samples.Display
{
    /// <summary>
    /// Interaction logic for PieChart.xaml
    /// </summary>
    public partial class PieChart : Infragistics.Samples.Framework.SampleContainer
    {
        public PieChart()
        {
            InitializeComponent();
        }
        private void pieChart_SliceClick(object sender, Infragistics.Controls.Charts.SliceClickEventArgs e)
        {
            e.IsExploded = !e.IsExploded; // toggle slice explosion
        }
    }
}