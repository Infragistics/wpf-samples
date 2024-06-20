using System.Windows;
using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework;

namespace IGBulletGraph.Samples.Data
{
    public partial class ProgrammaticallySettingValues : SampleContainer
    {
        public ProgrammaticallySettingValues()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.SetData(this.BulletGraph_Hardware, 0, 100000, 45000, 52000, 48000, 45000, 25000);
            this.SetData(this.BulletGraph_Software, 0, 100000, 68000, 65000, 78000, 74000, 25000);
            this.SetData(this.BulletGraph_Service, 0, 50000, 27000, 29000, 38000, 34000, 15000);
        }

        private void SetData(XamBulletGraph graph,
            double minimum,
            double maximum,
            double actualPerformance,
            double targetPerformance,
            double featuredMeasure,
            double comparativeMeasure,
            double interval)
        {
            graph.Scale.Minimum = minimum;
            graph.Scale.Maximum = maximum;
            graph.Scale.Interval = interval;
            graph.Scale.Ranges[0].Value = actualPerformance;
            graph.Scale.Ranges[1].Value = targetPerformance;
            graph.FeaturedMeasures[0].Value = featuredMeasure;
            graph.ComparativeMeasures[0].Value = comparativeMeasure;
        }
    }
}
