using System.Windows;
using System.Windows.Controls;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Tools;

namespace IGSchedule.Samples.Data
{
    /// <summary>
    /// Interaction logic for OutlookCalendarViewOverview.xaml
    /// </summary>
    public partial class OutlookCalendarViewOverview : SampleContainer
    {
        public OutlookCalendarViewOverview()
        {
            InitializeComponent();
        }
        
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (outlookCalendarView != null)
            {
                var curRadioButton = (RadioButton)sender;
                outlookCalendarView.CurrentViewMode = (Infragistics.Controls.Schedules.OutlookCalendarViewMode)int.Parse(curRadioButton.Tag.ToString());
            }
        }

        private void outlookCalendarView_CurrentViewModeChanged(object sender, RoutedPropertyChangedEventArgs<OutlookCalendarViewMode> e)
        {
            foreach (var item in pnlRadios.Children)
            {
                if (item is RadioButton && ((RadioButton)item).Tag.ToString() == ((int)e.NewValue).ToString())
                {
                    ((RadioButton)item).IsChecked = true;
                    return;
                }
            }
        }
    }
}
