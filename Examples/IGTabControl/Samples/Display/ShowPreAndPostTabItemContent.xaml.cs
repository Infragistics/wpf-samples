using System.Windows;
using IGTabControl.Resources;

namespace IGTabControl.Samples.Display
{
    /// <summary>
    /// Interaction logic for Calendar_SettingTheTheme.xaml
    /// </summary>
    public partial class ShowPreAndPostTabItemContent : Infragistics.Samples.Framework.SampleContainer
    {
        public ShowPreAndPostTabItemContent()
        {
            InitializeComponent();
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.AddTabs(6);

            this.Loaded -= new RoutedEventHandler(Page_Loaded);
        }

        private void AddTabs(int count)
        {
            for (int i = 0; i < count; i++)
            {
                int index = this.tabControl.Items.Count + 1;

                Infragistics.Windows.Controls.TabItemEx newTabItemEx = new Infragistics.Windows.Controls.TabItemEx();

                newTabItemEx.CloseButtonVisibility = Infragistics.Windows.Controls.TabItemCloseButtonVisibility.Visible;
                newTabItemEx.Header = string.Format(TabControlStrings.TabControl_AddRemoveTabs_DisplayArea_Header_Label, index.ToString());
                newTabItemEx.Content = string.Format(TabControlStrings.TabControl_AddRemoveTabs_DisplayArea_Content_Label, index.ToString());

                this.tabControl.Items.Add(newTabItemEx);
            }
        }
    }
}
