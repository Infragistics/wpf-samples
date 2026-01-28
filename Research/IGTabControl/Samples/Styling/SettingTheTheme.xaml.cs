using System.Windows;
using IGTabControl.Resources;
using Infragistics.Themes;
using Infragistics.Samples.Shared.Models;
using System.Windows.Controls;

namespace IGTabControl.Samples.Styling
{
    /// <summary>
    /// Interaction logic for SettingTheTheme.xaml
    /// </summary>
    public partial class SettingTheTheme : Infragistics.Samples.Framework.SampleContainer
    {
        public SettingTheTheme()
        {
            InitializeComponent();
        }
        
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {  
            var theme = this.ThemeSelector.SelectedItem as ThemeInfo;           
            if (theme == null) return;

            // apply selected theme 
            ThemeManager.SetTheme(this.LayoutRoot, theme.Resources);
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.AddTabs(5);

            this.Loaded -= new RoutedEventHandler(Page_Loaded);
        }

        private void AddTabs(int count)
        {
            for (int i = 0; i < count; i++)
            {
                int index = this.tabControl.Items.Count + 1;

                var newTabItemEx = new Infragistics.Windows.Controls.TabItemEx();

                newTabItemEx.CloseButtonVisibility = Infragistics.Windows.Controls.TabItemCloseButtonVisibility.Visible;
                newTabItemEx.Header = string.Format(TabControlStrings.TabControl_AddRemoveTabs_DisplayArea_Header_Label, index.ToString());
                newTabItemEx.Content = string.Format(TabControlStrings.TabControl_AddRemoveTabs_DisplayArea_Content_Label, index.ToString());

                this.tabControl.Items.Add(newTabItemEx);
            }
        }
    }
}
