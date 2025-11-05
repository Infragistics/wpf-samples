using Infragistics.Controls.Dashboards;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using System;
using System.Windows;

namespace IGDashboardTile.Samples.Display
{
    /// <summary>
    /// Interaction logic for LocalDataSourceOperations.xaml
    /// </summary>
    public partial class LocalDataSourceOperations : SampleContainer
    {
        public LocalDataSourceOperations()
        {
            DataChartDashboardTileFeature.Register();
            GeographicMapDashboardTileFeature.Register();
            LinearGaugeDashboardTileFeature.Register();
            RadialGaugeDashboardTileFeature.Register();
            PieChartDashboardTileFeature.Register();

            InitializeComponent();
        }
    }
}
