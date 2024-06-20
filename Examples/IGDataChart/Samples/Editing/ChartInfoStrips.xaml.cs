using System;
using System.Collections.ObjectModel;

namespace IGDataChart.Samples.Editing
{
    public partial class ChartInfoStrips : Infragistics.Samples.Framework.SampleContainer
    {
        public ChartInfoStrips()
        {
            InitializeComponent();

            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            RootLayout.Width = RootLayout.ActualWidth - 1; 
        }
    }

    public class StripDataCollection : ObservableCollection<StripDataPoint>
    {
        private static readonly Random Rand = new Random();

        public StripDataCollection()
        {
            this.Generate();
        }

        private void Generate()
        {
            this.Clear();
            double curr = 100;
            DateTime date = new DateTime(2009, 6, 01, 12, 00, 00);

            for (int i = 0; i < 52; i++)
            {
                if (Rand.NextDouble() > .5)
                {
                    curr += Rand.NextDouble();
                }
                else
                {
                    curr -= Rand.NextDouble();
                }
                this.Add(
                    new StripDataPoint
                    {
                        Date = date,
                        Index = i,
                        Label = (i + 1).ToString(),
                        Value = curr
                    });
                date = date.AddMonths(1);
            }

        }
    }
    public class StripDataPoint
    {
        public string Label { get; set; }
        public double Value { get; set; }
        public int Index { get; set; }
        public DateTime Date { get; set; }
    }
}
