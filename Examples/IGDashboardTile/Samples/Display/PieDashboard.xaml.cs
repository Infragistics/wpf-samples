using Infragistics.Controls.Dashboards;
using Infragistics.Samples.Framework;
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
    /// Interaction logic for PieDashboard.xaml
    /// </summary>
    public partial class PieDashboard : SampleContainer
    {
        public PieDashboard()
        {
            DataChartDashboardTileFeature.Register();
            GeographicMapDashboardTileFeature.Register();
            LinearGaugeDashboardTileFeature.Register();
            RadialGaugeDashboardTileFeature.Register();
            PieChartDashboardTileFeature.Register();

            InitializeComponent();

            dashboard.IncludedProperties = "Spending;Label".Split(';');
        }
    }
}
