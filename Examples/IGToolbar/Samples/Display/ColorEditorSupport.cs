using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Controls.Layouts;
using Infragistics.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Tools;
using Infragistics.Themes;
using Infragistics.Controls.Charts;
using System.Windows.Media;
using System.ComponentModel;

namespace IGToolbar.Samples
{
    public partial class ColorEditorSupport : SampleContainer
    {
        public ColorEditorSupport()
        {
            InitializeComponent();
        }

        public void ColorEditorToggleSeriesBrush(object sender, ToolCommandEventArgs e)
        {
            var target = (XamDataChart)((XamToolbar)sender).Target;
            var colorEditorTool = ((XamToolbar)sender).Actions[0];
            var color = ((ToolActionColorEditor)colorEditorTool).Value;
            if (e.Command.CommandId == "ToggleSeriesBrush" && target.Series.Count != 0)
            {
                Series series = target.Series[0];
                series.Brush = color;
                series.LegendItemBadgeMode = LegendItemBadgeMode.MatchSeries;
            }
        }
    }
}