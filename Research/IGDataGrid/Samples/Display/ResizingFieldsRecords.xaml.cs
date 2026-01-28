using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;
using System.Data;

namespace IGDataGrid.Samples.Display
{
    public partial class ResizingFieldsRecords : SampleContainer
    {
        public ResizingFieldsRecords()
        {
            InitializeComponent();
            //this.xamDataGrid1.FieldLayoutSettings.DataRecordSizingMode = Infragistics.Windows.DataPresenter.DataRecordSizingMode
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataTable dt = NWindDataSource.GetTable(NWindTable.Customers);
            this.xamDataGrid1.DataSource = dt.DefaultView;
        }
    }
}
