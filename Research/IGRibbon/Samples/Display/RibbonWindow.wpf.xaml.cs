using System.Windows;
using IGRibbon.Display;
using Infragistics.Samples.Framework;

namespace IGRibbon.Samples.Display
{
    public partial class RibbonWindow : SampleContainer
    {
        public RibbonWindow()
        {
            InitializeComponent();
        }

        public void btnLaunchWindowWithRibbon_Click(object sender, RoutedEventArgs e)
        {
            xamRibbonWindow newWin = new xamRibbonWindow();
            newWin.Show();
        }

        private void btnLaunchWindowWithoutRibbon_Click(object sender, RoutedEventArgs e)
        {
            xamRibbonWindowNoRibbon newWin = new xamRibbonWindowNoRibbon();
            newWin.Show();
        }
    }
}