using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using System.Windows.Data;
using System.Windows;
using Infragistics.Controls.Gauges;

namespace IGBulletGraph
{
    public partial class VerticalOrientation : Infragistics.Samples.Framework.SampleContainer
    {
        public VerticalOrientation()
        {
            InitializeComponent(); 
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            xamBulletGraph_VHOrientation.Orientation = xamBulletGraph_VHOrientation.Orientation == LinearScaleOrientation.Horizontal ? LinearScaleOrientation.Vertical : LinearScaleOrientation.Horizontal;
            xamBulletGraph_VHOrientation.HorizontalAlignment = xamBulletGraph_VHOrientation.HorizontalAlignment == HorizontalAlignment.Stretch ? HorizontalAlignment.Center : HorizontalAlignment.Stretch;
            xamBulletGraph_VHOrientation.VerticalAlignment = xamBulletGraph_VHOrientation.VerticalAlignment == VerticalAlignment.Top ? VerticalAlignment.Stretch : VerticalAlignment.Top;
        }
    }
}
