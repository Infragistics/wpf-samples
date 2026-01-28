using System.Windows;
using Infragistics.Samples.Framework;

namespace IGOrgChart.Samples.Display
{
    public partial class MaximumNodeDepth : SampleContainer
    {
        public MaximumNodeDepth()
        {
            InitializeComponent();
            this.SampleLoaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, System.EventArgs e)
        {
            this.OrgChart.ScaleToFit();
        }

        private void SliderMaxDepth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.OrgChart!= null)
                this.OrgChart.ScaleToFit();
        }
    }
}
