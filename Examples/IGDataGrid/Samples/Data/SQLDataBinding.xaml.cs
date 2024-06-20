using System.Data;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;
using System.Globalization;
using System.Threading;

namespace IGDataGrid.Samples.Data
{
    /// <summary>
    /// Interaction logic for SQLDataBinding.xaml
    /// </summary>
    public partial class SQLDataBinding : SampleContainer
    {
        public SQLDataBinding()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            DataTable dt = NWindDataSource.GetTable(NWindTable.Customers, cultureInfo);
            this.XamDataGrid1.DataSource = dt.DefaultView;
        }
    }
}
