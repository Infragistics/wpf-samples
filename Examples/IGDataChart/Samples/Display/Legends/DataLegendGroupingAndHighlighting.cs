using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using IGDataChart.Resources;                    // DataChartStrings
using IGDataChart.Samples.ChartData.ChartSeries;
using Infragistics;                             // BrushCollection
using Infragistics.Controls.Charts;             // XamDataChart 
using Infragistics.Controls.Description;
using Infragistics.Samples.Shared.Models;       // GallerySampleViewModel
using Infragistics.Samples.Shared.Resources;    // CommonStrings

namespace IGDataChart.Samples.Display.Series
{
    public partial class DataLegendGroupingAndHighlighting : Infragistics.Samples.Framework.SampleContainer
    {
        public DataLegendGroupingAndHighlighting()
        {
            InitializeComponent();
            UseDefaultTheme = true;
            this.DataContext = new SampleViewModel();
            ColumnChart.Loaded += ColumnChart_Loaded;
        }

        private void ColumnChart_Loaded(object sender, RoutedEventArgs e)
        {
            Legend.Target = ColumnChart;
        }
    }

    

}
