using System.Windows;
using IGTabControl.Resources;

namespace IGTabControl.Samples.Organization
{
    /// <summary>
    /// Interaction logic for AddRemoveTabs.xaml
    /// </summary>
    public partial class AddRemoveTabs : Infragistics.Samples.Framework.SampleContainer
    {
        public AddRemoveTabs()
        {
            InitializeComponent();
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.AddTabs();

            this.Loaded -= new RoutedEventHandler(Page_Loaded);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            this.AddTabs();
        }

        private void AddTabs()
        {
            if (this.numEditorAdd.Value == null) return;

            int count = (int)this.numEditorAdd.Value;

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

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            this.tabControl.Items.Clear();
        }
    }
}
