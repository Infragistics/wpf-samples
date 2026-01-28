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

namespace IGToolbar.Samples
{
    public partial class LayoutActions : SampleContainer
    {
        public LayoutActions()
        {
            InitializeComponent();
            SvgIconRegistry.Instance.AddSvgPathString("ResetCollection", "ResetIcon", @"
<svg width=""28"" height=""28"" viewBox=""0 0 28 28"" fill=""none"" xmlns=""http://www.w3.org/2000/svg"">
<path d=""M8.84651 14.8205C8.5002 13.4853 8.57295 12.076 9.05488 10.7836C9.53681 9.49119 10.4046 8.3783 11.5405 7.59587C12.6765 6.81344 14.0256 6.39934 15.4049 6.40975C16.7842 6.42015 18.1269 6.85455 19.2509 7.65402C20.3749 8.45349 21.2258 9.57934 21.6882 10.8789C22.1506 12.1784 22.2021 13.5887 21.8357 14.9185C21.4693 16.2482 20.7027 17.4331 19.64 18.3124C18.5772 19.1917 17.2698 19.7229 15.8949 19.8338"" stroke-miterlimit=""10"" stroke-linecap=""round"" stroke-linejoin=""round""/>
<path d=""M11.7255 12.4324L8.83 15.1468L5.91614 12.4324"" stroke-miterlimit=""10"" stroke-linecap=""round"" stroke-linejoin=""round""/>
<path d=""M7.37671 21.5905H22.0839"" stroke-miterlimit=""10"" stroke-linecap=""round"" stroke-linejoin=""round""/>
</svg>");


        }

        private void myTool_OnCommand(object sender, ToolCommandEventArgs e)
        {
            switch (e.Command.CommandId)
            {
                case "ResetAction":
                    chart.ResetZoom();
                    break;
            }
        }
    }
}