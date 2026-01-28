using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Controls.Editors;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Tools;

namespace IGSchedule.Samples.Navigation
{
    /// <summary>
    /// Interaction logic for XamDateNavigatorCustomizations.xaml
    /// </summary>
    public partial class XamDateNavigatorCustomizations : SampleContainer
    {
        public XamDateNavigatorCustomizations()
        {
            InitializeComponent();
        }
        
        private void HighlightCritiria_Changed(object sender, RoutedEventArgs e)
        {
            var curRadioButton = (RadioButton)sender;
            this.dateNavigator.HighlightDayCriteria = (Infragistics.Controls.Schedules.HighlightDayCriteria)int.Parse(curRadioButton.Tag.ToString());
        }

        private void DaysOfWeekHeaderFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var curCombo = this.DaysOfWeekHeaderFormat.SelectedItem as ComboBoxItem;
            var selected = (Infragistics.Controls.Editors.DayOfWeekHeaderFormat)int.Parse(curCombo.Tag.ToString());
            this.dateNavigator.DayOfWeekHeaderFormat = selected;
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
