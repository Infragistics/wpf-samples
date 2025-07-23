using System;
using System.Windows.Controls;

namespace IGSlider.Samples.Data
{
    public partial class DateTimeValueBinding : Infragistics.Samples.Framework.SampleContainer
    {
        public DateTimeValueBinding()
        {
            InitializeComponent();

            this.calendarRight.SelectedDate = new DateTime(2013, 9, 15);
            this.calendarRight.DisplayDateStart = this.sliderRight.MinValue;
            this.calendarRight.DisplayDateEnd = this.sliderRight.MaxValue;
        }

        private void calendarRight_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            this.calendarRight.DisplayDate = this.calendarRight.SelectedDate.Value;
        }

        private void calendarRight_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            this.calendarRight.SelectedDate = this.calendarRight.DisplayDate;
        }
    }
}
