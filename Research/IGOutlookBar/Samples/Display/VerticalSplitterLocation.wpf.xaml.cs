using System.Windows;
using Infragistics.Samples.Framework;

namespace IGOutlookBar.Samples.Display
{
    public partial class VerticalSplitterLocation : SampleContainer
    {
        public VerticalSplitterLocation()
        {
            InitializeComponent();
        }

        private void cboMinimizedLocation_ValueChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (this.xamOutlookBar.VerticalSplitterLocation == Infragistics.Windows.OutlookBar.VerticalSplitterLocation.Right)
            {
                stackPanelMain.HorizontalAlignment = HorizontalAlignment.Left;
            }
            else
            {
                stackPanelMain.HorizontalAlignment = HorizontalAlignment.Right;
            }
        }
    }
}
