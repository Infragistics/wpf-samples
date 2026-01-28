using System.Collections.ObjectModel;
using System.Windows;
using IGTimeline.Resources;
using Infragistics.Samples.Shared.Models;

namespace IGTimeline.Samples
{
    public partial class TimelinePerformance : Infragistics.Samples.Framework.SampleContainer
    {
        private ObservableCollection<NumericData> _entries;

        public TimelinePerformance()
        {
            InitializeComponent();
        }

        private void OnButtonBind_Click(object sender, RoutedEventArgs e)
        {
            GenerateData();
        }

        public void GenerateData()
        {
            _entries = new ObservableCollection<NumericData>();

            // Retrieve a string for date title once.
            var dateTitle = TimelineStrings.XWT_TimelinePerformance_Title;

            // Retrieve a string for date description once.
            var dateDescription = TimelineStrings.XWT_TimelinePerformance_Description;

            //Generating 100000 entries
            for (var ind = 1; ind < 100000; ind++)
            {
                var dataEntry = new NumericData
                {
                    Time = ind * 100,
                    Duration = 10,
                    Title = dateTitle + " " + ind,
                    Details = dateDescription + " " + ind
                };
                _entries.Add(dataEntry);
            }

            //Binding the Timeline series to the collection of NumericData. 
            igTimeline.Series[0].DataSource = _entries;
        }
    }
}
