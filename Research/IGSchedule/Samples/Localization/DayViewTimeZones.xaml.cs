using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Tools;

namespace IGSchedule.Samples.Localization
{
    /// <summary>
    /// Interaction logic for DayViewTimeZones.xaml
    /// </summary>
    public partial class DayViewTimeZones : SampleContainer
    {
        public DayViewTimeZones()
        {
            InitializeComponent();

            this.dataManager.DialogFactory = new Infragistics.Controls.Schedules.ScheduleDialogFactory();
            this.Loaded += new RoutedEventHandler(TimeZones_Loaded);
        }
        
        void TimeZones_Loaded(object sender, RoutedEventArgs e)
        {
            // retrieve the time zone info provider
            TimeZoneInfoProvider tzip = scheduleDataConnector.TimeZoneInfoProviderResolved;
            if (tzip != null)
            {
                // retrieve a list of available time zones - time zone tokens
                ReadOnlyObservableCollection<TimeZoneToken> TimeZoneTokens = tzip.TimeZoneTokens;
                primaryTZChooser.DisplayMemberPath = "DisplayName";
                primaryTZChooser.SelectedValuePath = "Id";
                secondaryTZChooser.DisplayMemberPath = "DisplayName";
                secondaryTZChooser.SelectedValuePath = "Id";

                primaryTZChooser.ItemsSource = TimeZoneTokens;
                secondaryTZChooser.ItemsSource = TimeZoneTokens;
                primaryTZChooser.SelectedItem = tzip.LocalToken;
            }
        }

        private void primaryTZChooser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TimeZoneInfoProvider tzip = scheduleDataConnector.TimeZoneInfoProviderResolved;
            if (tzip != null)
            {
                tzip.LocalTimeZoneId = primaryTZChooser.SelectedValue.ToString();
                dayView.PrimaryTimeZoneLabel = ExtractUTCportion((primaryTZChooser.SelectedItem as TimeZoneToken).DisplayName);
            }
        }

        private void secondaryTZChooser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dayView.SecondaryTimeZoneLabel = ExtractUTCportion((secondaryTZChooser.SelectedItem as TimeZoneToken).DisplayName);
            dayView.SecondaryTimeZoneId = secondaryTZChooser.SelectedValue.ToString();
        }

        private string ExtractUTCportion(string DisplayName)
        {
            if (DisplayName.Contains(")"))
            {
                return DisplayName.Substring(0, DisplayName.IndexOf(")") + 1);
            }
            else
            {
                return DisplayName;
            }
        }
    }
}
