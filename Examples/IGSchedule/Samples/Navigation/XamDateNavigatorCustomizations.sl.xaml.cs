using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Controls.Editors;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Controls;

namespace IGSchedule.Samples.Navigation
{
    public partial class XamDateNavigatorCustomizations : SampleContainer
    {
        public XamDateNavigatorCustomizations()
        {           
            InitializeComponent();
            this.dataManager.ColorScheme = new IGColorScheme();
            Loaded += new RoutedEventHandler(XamDateNavigatorCustomizations_Loaded);
        }

        void XamDateNavigatorCustomizations_Loaded(object sender, RoutedEventArgs e)
        {            
            this.DaysOfWeekHeaderFormat.SelectedIndex = 3;
        }

        private void HighlightCritiria_Changed(object sender, RoutedEventArgs e)
        {
            if (this.dateNavigator != null)
            {
                var curRadioButton = (RadioButton)sender;
                this.dateNavigator.HighlightDayCriteria = (HighlightDayCriteria)int.Parse(curRadioButton.Tag.ToString());
            }
        }

        private void DaysOfWeekHeaderFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.DaysOfWeekHeaderFormat != null)
            {
                var curCombo = this.DaysOfWeekHeaderFormat.SelectedItem as ComboBoxItem;
                var selected = (DayOfWeekHeaderFormat)int.Parse(curCombo.Tag.ToString());
                this.dateNavigator.DayOfWeekHeaderFormat = selected;
            }
        }

        private void chkWeekNumber_Checked(object sender, RoutedEventArgs e)
        {
            this.dateNavigator.WeekNumberVisibility = Visibility.Visible;
        }

        private void chkWeekNumber_UnChecked(object sender, RoutedEventArgs e)
        {
            this.dateNavigator.WeekNumberVisibility = Visibility.Collapsed;
        }

        private void ActivityTooltipTemplateCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ActivityTooltipTemplateCombo != null)
            {
                var currentSelection = this.ActivityTooltipTemplateCombo.SelectedItem as ComboBoxItem;
                if (currentSelection.Tag.ToString() != "Default")
                {
                    this.dateNavigator.ActivityToolTipTemplate = (DataTemplate)this.Resources["dateNavDayToolTipTemplateExample1"];
                }
                else
                {
                    this.dateNavigator.ClearValue(XamDateNavigator.ActivityToolTipTemplateProperty);
                }
            }
        }
    }
}
