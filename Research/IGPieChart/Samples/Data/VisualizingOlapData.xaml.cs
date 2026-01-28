using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace IGPieChart.Samples.Data
{
    public partial class VisualizingOlapData : SampleContainer
    {
        public VisualizingOlapData()
        {
            InitializeComponent();
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            olapPieChart.DataSource = null;
            base.OnNavigatingFrom(e);
        }
    }

    public class CarsSalesDataProvider
    {
        public CarsSalesDataProvider()
        {
            this.Data = new ObservableCollection<CarsSales>(CarsSales.GenerateData());
        }

        public ObservableCollection<CarsSales> Data { get; set; }

    }
}
