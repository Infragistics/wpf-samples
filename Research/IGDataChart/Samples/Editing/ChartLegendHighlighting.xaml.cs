using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Infragistics.Controls.Charts;

namespace IGDataChart.Samples.Editing
{
    public partial class ChartLegendHighlighting : Infragistics.Samples.Framework.SampleContainer
    {
        public ChartLegendHighlighting()
        {
            InitializeComponent();

            this.HighlightLegendModeComboBox.SelectionChanged += OnHighlightLegendModeChanged;
            this.HighlightLegendModeComboBox.SelectedIndex = 0;

        }

        private void OnHighlightLegendModeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HighlightLegendModeComboBox.SelectedIndex == -1)
            {
                this.DataChart.LegendHighlightingMode = LegendHighlightingMode.Auto;
            }
            else if (HighlightLegendModeComboBox.SelectedIndex == 0)
            {
                this.DataChart.LegendHighlightingMode = LegendHighlightingMode.Auto;                
            }
            else if (HighlightLegendModeComboBox.SelectedIndex == 1)
            {
                this.DataChart.LegendHighlightingMode = LegendHighlightingMode.MatchSeries;
            }
            else if (HighlightLegendModeComboBox.SelectedIndex == 2)
            {
                this.DataChart.LegendHighlightingMode = LegendHighlightingMode.None;
            }
        }
    }
}
