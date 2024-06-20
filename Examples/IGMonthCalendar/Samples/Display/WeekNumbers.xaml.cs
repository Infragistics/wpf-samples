using System;
using System.Windows;
using Infragistics.Samples.Framework;

namespace IGMonthCalendar.Samples.Display
{
    /// <summary>
    /// Interaction logic for WeekNumbers.xaml
    /// </summary>
    public partial class WeekNumbers : SampleContainer
    {
        public WeekNumbers()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(WeekNumbers_Loaded);
        }

        void WeekNumbers_Loaded(object sender, RoutedEventArgs e)
        {
            xamCalendar.ActiveDate = DateTime.Now;
        }
    }
}
