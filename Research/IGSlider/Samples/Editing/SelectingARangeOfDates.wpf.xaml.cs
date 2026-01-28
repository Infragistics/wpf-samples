using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;

namespace IGSlider.Samples.Editing
{
    public partial class SelectingARangeOfDates : SampleContainer
    {
        private Boolean isRangeChanging = false;
        private Boolean isInitialized = false;

        public SelectingARangeOfDates()
        {
            InitializeComponent();

            calendarRight.SelectionMode = CalendarSelectionMode.SingleRange;
            calendarRight.DisplayDateStart = sliderRight.MinValue;
            calendarRight.DisplayDateEnd = sliderRight.MaxValue;

            this.calendarRight.SelectedDates.Clear();
            this.calendarRight.SelectedDates.AddRange(sliderRight.Thumbs[0].Value, sliderRight.Thumbs[1].Value);

            isInitialized = true;
  
            
        }

        private void RangeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime> e)
        {
            if (!isRangeChanging && isInitialized)
            {
                isRangeChanging = true;

                calendarRight.SelectedDates.Clear();
                calendarRight.SelectedDates.AddRange(sliderRight.Thumbs[0].Value, sliderRight.Thumbs[1].Value);

                isRangeChanging = false;
            }
        }

        private void calendarRight_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isRangeChanging)
            {
                isRangeChanging = true;

                var first = calendarRight.SelectedDates.First();
                var last = calendarRight.SelectedDates.Last();

                if (first == last)
                {
                    if (last == sliderRight.MaxValue)
                    {
                        sliderRight.Thumbs[0].Value = last.AddDays(-1);
                        sliderRight.Thumbs[1].Value = last;
                    }
                    else
                    {
                        sliderRight.Thumbs[0].Value = first;
                        sliderRight.Thumbs[1].Value = first.AddDays(1);
                    }
                }
                else if (first > last)
                {
                    sliderRight.Thumbs[0].Value = last;
                    sliderRight.Thumbs[1].Value = first;
                }
                else
                {
                    sliderRight.Thumbs[0].Value = first;
                    sliderRight.Thumbs[1].Value = last;
                }

                isRangeChanging = false;
            }
        }
    }
}
