using Infragistics.Controls.Schedules;
using Prism.Events;
using System;
using System.Collections.ObjectModel;

namespace IgOutlook.Modules.Calendar
{
    public class DateNavigatorSelectedDatesChanged : PubSubEvent<ObservableCollection<DateTime>> { }
    public class OutlookDateNavigatorSelectedDatesChanged : PubSubEvent<ObservableCollection<DateTime>> { }

    public class ActivityChangedEvent : PubSubEvent<ActivityBase> { }

    public class CalendarClosedEvent : PubSubEvent<string> { };
    public class UnselectedCalendarChangedEvent : PubSubEvent<string> { };
    public class SelectedCalendarChangedEvent : PubSubEvent<string> { };
}
