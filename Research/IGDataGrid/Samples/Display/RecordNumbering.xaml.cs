using System.Data;
using System.Windows;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;

namespace IGDataGrid.Samples.Display
{
    public partial class RecordNumbering : SampleContainer
    {
        public RecordNumbering()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            DataTable dt = NWindDataSource.GetTable(NWindTable.Customers);
            this.XamDataGrid1.DataSource = dt.DefaultView;
        }
    }
}
