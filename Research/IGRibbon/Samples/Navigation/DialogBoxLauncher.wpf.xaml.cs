using System.Windows;
using IGRibbon.Resources;
using Infragistics.Samples.Framework;

namespace IGRibbon.Samples.Navigation
{
    public partial class DialogBoxLauncher : SampleContainer
    {
        public DialogBoxLauncher()
        {
            InitializeComponent();
            CreateLauncherTool();
        }

        private void CreateLauncherTool()
        {
            Infragistics.Windows.Ribbon.ButtonTool launchBtn = new Infragistics.Windows.Ribbon.ButtonTool();
            launchBtn.Click += new RoutedEventHandler(LaunchBtn_Click);
            Infragistics.Windows.Ribbon.XamRibbonScreenTip tip = new Infragistics.Windows.Ribbon.XamRibbonScreenTip();
            tip.Header = RibbonStrings.CompositionWalkthrough_DialogBoxLauncher_Tab_Format_Group_TextStyle_LaunchButton_ScreenTip_Header;
            tip.Footer = RibbonStrings.CompositionWalkthrough_DialogBoxLauncher_Tab_Format_Group_TextStyle_LaunchButton_ScreenTip_Footer;
            tip.FooterSeparatorVisibility = Visibility.Visible;
            tip.Content = RibbonStrings.CompositionWalkthrough_DialogBoxLauncher_Tab_Format_Group_TextStyle_LaunchButton_ScreenTip_Content;
            tip.FooterTemplate = (DataTemplate)this.FindResource("FooterTemplateWithHelpIcon");
            launchBtn.ToolTip = tip;

            grpTextStyle.DialogBoxLauncherTool = launchBtn;
        }

        protected void LauncherShow_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                RibbonStrings.CompositionWalkthrough_DialogBoxLauncher_MessageBox_FontMessage,
                RibbonStrings.CompositionWalkthrough_DialogBoxLauncher_MessageBox_Title,
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        void LaunchBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                RibbonStrings.CompositionWalkthrough_DialogBoxLauncher_MessageBox_Message,
                RibbonStrings.CompositionWalkthrough_DialogBoxLauncher_MessageBox_Title,
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}