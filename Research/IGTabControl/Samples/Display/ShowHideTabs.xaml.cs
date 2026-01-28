using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using IGTabControl.Resources;

namespace IGTabControl.Samples.Display
{
    public partial class ShowHideTabs : Infragistics.Samples.Framework.SampleContainer
    {
        public ShowHideTabs()
        {
            InitializeComponent();

            // Initialize the combobox we use to display possible values for the TabItemEx's nullable CloseButtonVisibility property by adding all the values
            // from the TabItemCloseButtonVisibility enumeration plus an entry for 'Default' (which we will treat as null)
            this.cbTabItemCloseButtonVisibility.Items.Add("[Default]");
            foreach (Infragistics.Windows.Controls.TabItemCloseButtonVisibility closeButtonVisibility in Enum.GetValues(typeof(Infragistics.Windows.Controls.TabItemCloseButtonVisibility)))
                this.cbTabItemCloseButtonVisibility.Items.Add(closeButtonVisibility);
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

                newTabItemEx.Header = string.Format(TabControlStrings.TabControl_AddRemoveTabs_DisplayArea_Header_Label, index.ToString());
                newTabItemEx.Content = string.Format(TabControlStrings.TabControl_AddRemoveTabs_DisplayArea_Content_Label, index.ToString());

                this.tabControl.Items.Add(newTabItemEx);
            }
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Infragistics.Windows.Controls.TabItemEx selectedTabItem = this.GetSelectedTab();
            if (selectedTabItem != null)
            {
                this.gbCurrentTab.Header = string.Format(TabControlStrings.TabControl_ShowHideTabs_ConfigArea_CurrentTab_Label, selectedTabItem.Header.ToString());

                // Bind and enable elements in the UI that operate on the selected tab.
                this.chkEnabled.SetBinding(CheckBox.IsCheckedProperty, Infragistics.Windows.Utilities.CreateBindingObject(Infragistics.Windows.Controls.TabItemEx.IsEnabledProperty, BindingMode.TwoWay, selectedTabItem));
                this.chkAllowClose.SetBinding(CheckBox.IsCheckedProperty, Infragistics.Windows.Utilities.CreateBindingObject(Infragistics.Windows.Controls.TabItemEx.AllowClosingProperty, BindingMode.TwoWay, selectedTabItem));

                Infragistics.Windows.Controls.TabItemCloseButtonVisibility? closeButtonVisibility = selectedTabItem.CloseButtonVisibility;

                if (closeButtonVisibility.HasValue)
                    this.cbTabItemCloseButtonVisibility.SelectedItem = closeButtonVisibility;
                else
                    this.cbTabItemCloseButtonVisibility.SelectedIndex = 0;

                this.chkEnabled.IsEnabled = true;
                this.chkAllowClose.IsEnabled = true;
                this.cbTabItemCloseButtonVisibility.IsEnabled = true;
            }
            else
            {
                this.gbCurrentTab.Header = string.Format(TabControlStrings.TabControl_ShowHideTabs_ConfigArea_CurrentTab_Label, "????");

                // Since we do not have a selected tab, unbind and disable elements in the UI that operate on the selected tab.
                BindingOperations.ClearBinding(this.chkEnabled, CheckBox.IsCheckedProperty);
                BindingOperations.ClearBinding(this.chkAllowClose, CheckBox.IsCheckedProperty);

                this.chkEnabled.IsEnabled = false;
                this.chkAllowClose.IsEnabled = false;
                this.cbTabItemCloseButtonVisibility.IsEnabled = false;
            }
        }

        private void cbTabItemCloseButtonVisibility_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // This shouldn't happen, but if the combo box is not enabled, just exit.
            if (!this.cbTabItemCloseButtonVisibility.IsEnabled)
                return;

            // Get the selected tab and then apply the TabItemCloseButtonVisibility value that was just selected to the selected tab's
            // CloseButtonVisibility property.  If the 'Default' entry was selected, then set the property to null (note: the TabItemEx 
            // CloseButtonVisibility property is a nullable type, with null interpreted as 'use the value set at the control level')
            Infragistics.Windows.Controls.TabItemEx selectedTabItem = this.GetSelectedTab();
            if (selectedTabItem != null)
            {
                object selectedVisibilty = this.cbTabItemCloseButtonVisibility.SelectedItem;

                if (selectedVisibilty is Infragistics.Windows.Controls.TabItemCloseButtonVisibility)
                    selectedTabItem.CloseButtonVisibility = (Infragistics.Windows.Controls.TabItemCloseButtonVisibility)selectedVisibilty;
                else
                    selectedTabItem.CloseButtonVisibility = null;
            }
        }

        private Infragistics.Windows.Controls.TabItemEx GetSelectedTab()
        {
            return this.tabControl.SelectedItem as Infragistics.Windows.Controls.TabItemEx;
        }
    }
}