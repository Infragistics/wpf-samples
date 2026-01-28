using Infragistics.Samples.Framework; 

namespace IGFinancialChart.Samples.Display
{ 
    public partial class ChartTypes : SampleContainer
    {
        public ChartTypes()
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
