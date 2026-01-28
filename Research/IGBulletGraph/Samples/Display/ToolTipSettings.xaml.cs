using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using System.Windows.Media;
using Infragistics.Controls.Gauges;
using System.Windows.Data;
using IGBulletGraph.Resources;

namespace IGBulletGraph
{
    public partial class ToolTipSettings : Infragistics.Samples.Framework.SampleContainer
    {
        public ToolTipSettings()
        {
            InitializeComponent();
            DevBulletGraph.LabelFormat = "{0}" + BulletGraphStrings.Tasks_Hours;
            QABulletGraph.LabelFormat = "{0}" + BulletGraphStrings.Tasks_Hours;
        }
    }
}
