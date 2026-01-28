using System.Windows.Controls;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Tools;

namespace IGSchedule.Samples.Display
{
    /// <summary>
    /// Interaction logic for MonthViewGroupsResizing.xaml
    /// </summary>
    public partial class MonthViewGroupsResizing : SampleContainer
    {
        public MonthViewGroupsResizing()
        {
            InitializeComponent();
        }
        
        private void cboCalendarDisplayMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            monthView.CalendarDisplayMode = (CalendarDisplayMode)cboCalendarDisplayMode.SelectedIndex;
        }
    }
}
