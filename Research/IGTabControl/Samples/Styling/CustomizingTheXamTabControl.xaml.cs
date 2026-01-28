using System.Windows;
using IGTabControl.Resources;

namespace IGTabControl.Samples.Styling
{
    /// <summary>
    /// Interaction logic for CustomTabs.xaml
    /// </summary>
    public partial class CustomTabs : Infragistics.Samples.Framework.SampleContainer
    {
        int index = 0;

        public CustomTabs()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            this.AddTabs();
        }

        private void AddTabs()
        {
            index++;
            Infragistics.Windows.Controls.TabItemEx newTabItemEx = new Infragistics.Windows.Controls.TabItemEx();

            newTabItemEx.CloseButtonVisibility = Infragistics.Windows.Controls.TabItemCloseButtonVisibility.Visible;
            newTabItemEx.Header = string.Format(TabControlStrings.TabControl_AddRemoveTabs_DisplayArea_Header_Label, index.ToString());
            newTabItemEx.Content = string.Format(TabControlStrings.TabControl_AddRemoveTabs_DisplayArea_Content_Label, index.ToString());

            this.tabControl.Items.Add(newTabItemEx);
        }
    }
}
