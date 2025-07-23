using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using System.Windows.Media;
using System.Windows;
using Infragistics.Controls.Editors;
using System.Windows.Data;
using Infragistics.Samples.Shared.Converters;
using System.Windows.Media.Animation;
using System;
using IGBulletGraph.Resources;
using Infragistics.Controls.Gauges;

namespace IGBulletGraph
{
    public partial class ScaleLabelingSettings : Infragistics.Samples.Framework.SampleContainer
    {
        public ScaleLabelingSettings()
        {
            InitializeComponent();
        }

    

        private void xamBulletGraph_LabelSettings_AlignLabel(object sender, AlignLinearGraphLabelEventArgs args)
        {
            if (args.Value == 90000000)
            {
                args.OffsetX += 20;
            }
        }

        private void xamBulletGraph_LabelSettings_FormatLabel(object sender, FormatLinearGraphLabelEventArgs args)
        {
            if (args.Value == 90000000 )
            {
                args.Label = args.Label.Replace(",000,000 K", " " + BulletGraphStrings.million);
            }
            else
            {
                int index = args.Label.LastIndexOf(",000");
                if (index >= 0)
                {
                    args.Label = args.Label.Remove(index, ",000".Length).Insert(index, "");
                }
            }
        }

       
    }
}

