using Infragistics.Samples.Framework; 

namespace IGFinancialChart.Samples.Display
{ 
    public partial class VolumeTypes : SampleContainer
    {
        public VolumeTypes()
        {
            InitializeComponent(); 

            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Chart.IsToolbarVisible = false;
        }
    }
}
