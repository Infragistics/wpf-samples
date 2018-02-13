using Infragistics.Controls.Editors;
using Infragistics.Controls.Schedules;
using System;
using System.Linq;
using System.Collections.Generic;

namespace IgOutlook.Modules.Calendar
{
    public class OutlookDateNavigator : IOutlookDateNavigator
    {
        public OutlookDateNavigator()
        {
            selectedDates = new List<DateTime>();
        }

        private IList<DateTime> selectedDates;

        public IList<DateTime> GetSelectedDates()
        {
            return selectedDates;
        }

        public event EventHandler<SelectedDatesChangedEventArgs> SelectedDatesChanged;

        public void SetSelectedDates(IList<DateTime> dates)
        {
            var removed = selectedDates.Except(dates).ToList();
            var added = dates.Except(selectedDates).ToList();

            selectedDates = dates;

                SelectedDatesChanged(this, new SelectedDatesChangedEventArgs(removed, added));
        }
    }
}
