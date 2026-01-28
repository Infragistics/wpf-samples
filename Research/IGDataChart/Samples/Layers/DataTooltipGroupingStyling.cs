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
    public partial class DataTooltipGroupingStyling : Infragistics.Samples.Framework.SampleContainer
    {
        public DataTooltipGroupingStyling()
        {
            InitializeComponent();
            UseDefaultTheme = true;
            this.DataContext = new SampleViewModel();
        }

    }
}
