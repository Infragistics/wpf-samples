using Infragistics.Controls.Schedules;
using Prism.Interactivity;

namespace IgOutlook.Infrastructure.Prism
{
    public class XamOutlookCalendarViewCommandBehavior : CommandBehaviorBase<XamOutlookCalendarView>
    {
        public XamOutlookCalendarViewCommandBehavior(XamOutlookCalendarView outlookCalendarView)
            : base(outlookCalendarView)
        {
            outlookCalendarView.SelectedTimeRangeChanged += outlookCalendarView_SelectedTimeRangeChanged;
        }

        void outlookCalendarView_SelectedTimeRangeChanged(object sender, Infragistics.Controls.NullableRoutedPropertyChangedEventArgs<Infragistics.DateRange> e)
        {
            CommandParameter = ((XamOutlookCalendarView)sender).SelectedTimeRange;
            ExecuteCommand(CommandParameter);
        }
    }
}
