using System.Windows;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;

namespace IGBulletGraph.Samples.Data
{
    public partial class DataBinding : SampleContainer
    {
        private GraphViewModel viewModel;

        public DataBinding()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.viewModel = new GraphViewModel();
            this.DataContext = this.viewModel;
            this.LoadData();
        }

        private void SetKPIValues(KPIData dataSource, double minimum, double maximum,
            double actualPerformance, double targetPerformance, double featuredMeasure, double comparativeMeasure)
        {
            dataSource.Minimum = minimum;
            dataSource.Maximum = maximum;
            dataSource.ActualPerformance = actualPerformance;
            dataSource.TargetPerformance = targetPerformance;
            dataSource.FeaturedMeasure = featuredMeasure;
            dataSource.ComparativeMeasure = comparativeMeasure;
        }

        private void LoadData()
        {
            this.SetKPIValues(this.viewModel.YTDRevenue, 0, 10000, 5200, 6400, 6000, 5550);
            this.SetKPIValues(this.viewModel.Profit, 0, 10000, 3400, 4000, 5000, 4800);
            this.SetKPIValues(this.viewModel.AvgOrder, 0, 500, 254, 300, 350, 300);
            this.SetKPIValues(this.viewModel.NewCustomers, 0, 100, 60, 70, 50, 73);
        }
    }

    public class GraphViewModel
    {
        public GraphViewModel()
        {
            this.YTDRevenue = new KPIData();
            this.Profit = new KPIData();
            this.AvgOrder = new KPIData();
            this.NewCustomers = new KPIData();
        }

        public KPIData YTDRevenue { get; set; }
        public KPIData Profit { get; set; }
        public KPIData AvgOrder { get; set; }
        public KPIData NewCustomers { get; set; }
    }
}
