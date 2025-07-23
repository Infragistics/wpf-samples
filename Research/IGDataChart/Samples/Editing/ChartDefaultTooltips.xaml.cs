using System.Windows;

namespace IGDataChart.Samples.Editing
{
    public partial class ChartDefaultTooltips : Infragistics.Samples.Framework.SampleContainer
    {
        public ChartDefaultTooltips()
        {
            InitializeComponent();

            // change the visible chart
            ChartType.SelectionChanged += (s, e) =>
                {
                    var selectedChartIndex = ChartType.SelectedIndex + 1;
                    for (int i = 1; i < LayoutRoot.Children.Count; i++)
                    {
                        LayoutRoot.Children[i].Visibility = (i == selectedChartIndex) ?
                            Visibility.Visible : Visibility.Collapsed;
                    }
                };
        }
    }
}
