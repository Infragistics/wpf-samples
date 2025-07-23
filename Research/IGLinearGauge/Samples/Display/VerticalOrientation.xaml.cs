using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using System.Windows;
using System.Windows.Media;
using Infragistics.Controls.Gauges;

namespace IGLinearGauge
{
    public partial class VerticalOrientation : Infragistics.Samples.Framework.SampleContainer
    {
        public VerticalOrientation()
        {
            InitializeComponent();             
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            xamLinearGauge_VHOrientation.Orientation = xamLinearGauge_VHOrientation.Orientation == LinearScaleOrientation.Horizontal ? LinearScaleOrientation.Vertical : LinearScaleOrientation.Horizontal;
            xamLinearGauge_VHOrientation.HorizontalAlignment = xamLinearGauge_VHOrientation.HorizontalAlignment == HorizontalAlignment.Stretch ? HorizontalAlignment.Center : HorizontalAlignment.Stretch;
            xamLinearGauge_VHOrientation.VerticalAlignment = xamLinearGauge_VHOrientation.VerticalAlignment == VerticalAlignment.Top ? VerticalAlignment.Stretch : VerticalAlignment.Top;
        }
    }
}
