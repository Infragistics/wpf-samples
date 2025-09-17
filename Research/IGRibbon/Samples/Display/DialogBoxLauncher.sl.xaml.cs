using System.Windows;
using IGRibbon.Resources;
using Infragistics.Controls.Menus;
using Infragistics.Samples.Framework;

namespace IGRibbon.Samples.Display
{
    public partial class DialogBoxLauncher : SampleContainer
    {
        public DialogBoxLauncher()
        {
            InitializeComponent();
        }

        private void ButtonTool_Click(object sender, RibbonToolEventArgs e)
        {
            MessageBox.Show(RibbonStrings.XWR_DialogBoxLauncherMessageBox, RibbonStrings.XWR_DialogBoxLauncherMessageBoxTitle, MessageBoxButton.OK);
        }
    }
}
