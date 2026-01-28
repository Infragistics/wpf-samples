using System;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;

namespace IGMonthCalendar.Samples.Display
{
    /// <summary>
    /// Interaction logic for DisablingDates.xaml
    /// </summary>
    public partial class DisablingDates : SampleContainer
    {
        public DisablingDates()
        {
            InitializeComponent();
        }

        void btnAddRange_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = (DateTime)this.dteNewRangeStart.Value;
            DateTime endDate = (DateTime)this.dteNewRangeEnd.Value;

            // create a new disabled range to disable one or more dates
            Infragistics.Windows.Editors.CalendarDateRange range;
            if (endDate.CompareTo(startDate) < 0)
            {
                range = new Infragistics.Windows.Editors.CalendarDateRange(endDate, startDate);
            }
            else
            {
                range = new Infragistics.Windows.Editors.CalendarDateRange(startDate, endDate);
            }

            if (xamCalendar.DisabledDates.Contains(range) == false)
                xamCalendar.DisabledDates.Add(range);
        }

        void btnDeleteRanges_Click(object sender, RoutedEventArgs e)
        {
            ListBox list = this.lstRanges;
            object[] array = new object[list.SelectedItems.Count];
            list.SelectedItems.CopyTo(array, 0);


            // remove the selected date ranges from the DisabledDates
            foreach (Infragistics.Windows.Editors.CalendarDateRange range in array)
                xamCalendar.DisabledDates.Remove(range);
        }

        void lstRanges_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // need to have at least 1 selected range to be able to delete
            this.btnDeleteRanges.IsEnabled = ((ListBox)sender).SelectedItems.Count > 0;
        }
    }
}
