using System.Windows;
using IGRibbon.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Windows.Ribbon;

namespace IGRibbon.Samples.Editing
{
    public partial class ScreenTips : SampleContainer
    {
        public ScreenTips()
        {
            InitializeComponent();
            CreateScreenTipFromCSharp();
        }

        private void CreateScreenTipFromCSharp()
        {
            // This code shows how to apply a tooltip to a tool in the XamRibbon from code.
            XamRibbonScreenTip screenTip = new XamRibbonScreenTip();
            screenTip.Header = RibbonStrings.Shared_Tab_Format_Group_TextStyle_Button_Bold_ScreenTip_Header;
            screenTip.Content = RibbonStrings.Shared_Tab_Format_Group_TextStyle_Button_Bold_ScreenTip_Content;
            screenTip.FooterSeparatorVisibility = Visibility.Collapsed;
            this.boldTool.ToolTip = screenTip;
        }
    }
}
