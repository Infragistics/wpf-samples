using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Infragistics;
using Infragistics.Controls;
using Infragistics.Controls.Editors;
using Infragistics.Samples.Framework;

namespace IGCalendar.Samples.Editing
{
    public partial class CalendarMinMax : SampleContainer
    {
        public CalendarMinMax()
        {
            InitializeComponent();
            myCalendar.ResourceProvider = new CalendarResourceProvider() { ResourceSet = CalendarResourceSet.IGTheme };
        }

        private void btnDisableDate_Click(object sender, RoutedEventArgs e)
        {
            if (dpDisableDate.SelectedDate.HasValue)
            {
                if (chkRange.IsChecked == true && dpDisableDateRangeEnd.SelectedDate.HasValue)
                    myCalendar.DisabledDates.Add(new DateRange(dpDisableDate.SelectedDate.Value, dpDisableDateRangeEnd.SelectedDate.Value));
                else

                    myCalendar.DisabledDates.Add(new DateRange(dpDisableDate.SelectedDate.Value));
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var btnRemove = (Button)sender;
            myCalendar.DisabledDates.Remove((DateRange)btnRemove.DataContext);
        }

        private void lstDisabledDaysofWeek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedTags = String.Join(",", lstDisabledDaysofWeek.SelectedItems.Cast<ListBoxItem>().Select(t => t.Tag.ToString()));
            if (!String.IsNullOrWhiteSpace(SelectedTags))
                myCalendar.DisabledDaysOfWeek = (DayOfWeekFlags)Enum.Parse(typeof(DayOfWeekFlags), SelectedTags, true);
            else
                myCalendar.DisabledDaysOfWeek = DayOfWeekFlags.None;
        }
    }
}
