using IGRibbon.Resources;
using Infragistics.Controls.Menus;
using Infragistics.Samples.Framework;

namespace IGRibbon.Samples.Display
{
    public partial class MenuTool : SampleContainer
    {
        public MenuTool()
        {
            InitializeComponent();
        }

        private void SegmentedMenuTool_Click(object sender, RibbonToolEventArgs e)
        {
            this.MenuOptions.Text = RibbonStrings.XWR_SegmentedMenuToolClicked;

        }

        private void SegmentedStateMenuTool_Click(object sender, RibbonToolEventArgs e)
        {
            this.MenuOptions.Text = RibbonStrings.XWR_SegmentedStateMenuToolClicked;

        }

        private void MenuTool_Click(object sender, RibbonToolEventArgs e)
        {
            this.MenuOptions.Text = RibbonStrings.XWR_MenuToolClicked;

        }
    }
}
