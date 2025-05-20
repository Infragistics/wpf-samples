using Infragistics.Controls.Dashboards;
using Infragistics.Controls.DataSource;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
