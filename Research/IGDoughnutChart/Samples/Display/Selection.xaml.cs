using Infragistics.Samples.Framework;
using Infragistics.Controls.Charts;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows;

namespace IGDoughnutChart.Samples
{
    public partial class Selection : SampleContainer
    {
        public Selection()
        {
            InitializeComponent();
        }

        private void Series_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            // Select and explode the first slice
            var firstSlice = ((RingSeries)this.doughnutChart.Series[0]).Ring.ArcItems[0].SliceItems[0].Slice;
            firstSlice.IsExploded = true;
            firstSlice.IsSelected = true;
        }

        private void DoughnutChart_SliceClick(object sender, Infragistics.Controls.Charts.SliceClickEventArgs e)
        {
            e.IsSelected = !e.IsSelected;
            e.IsExploded = !e.IsExploded;
        }
    }
}

