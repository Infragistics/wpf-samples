using System.Windows;
using System.Windows.Controls;
using IGTabControl.Resources;

namespace IGTabControl.Samples.Data
{
    /// <summary>
    /// Interaction logic for UsingCommands.xaml
    /// </summary>
    public partial class UsingCommands : Infragistics.Samples.Framework.SampleContainer
    {
        public UsingCommands()
        {
            InitializeComponent();
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.AddTabs(10);

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

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Infragistics.Windows.Controls.TabItemEx selectedTab = this.tabControl.SelectedItem as Infragistics.Windows.Controls.TabItemEx;

            this.btnCloseSelectedTab.CommandTarget = selectedTab;
            this.btnCloseAllButSelectedTab.CommandTarget = selectedTab;
        }
    }
}
