//using System;
//using System.Diagnostics;

namespace IGSchedule
{
    public class Preloader
    {
        static Preloader()
        {
            //long t = DateTime.Now.Ticks;
            new IGSchedule.DataSource.ScheduleData();
            new Infragistics.Controls.Schedules.ListScheduleDataConnector();
            new Infragistics.Controls.Schedules.XamScheduleDataManager();
            new Infragistics.Controls.Schedules.XamDayView();
            new Infragistics.Controls.Schedules.XamMonthView();
            new Infragistics.Controls.Schedules.XamScheduleView();
            new Infragistics.Controls.Schedules.XamOutlookCalendarView();
            //Debug.WriteLine("@Preloading " + typeof(Preloader) + ": " + (DateTime.Now.Ticks - t) / 10000 + " ms");
        }
    }
}
