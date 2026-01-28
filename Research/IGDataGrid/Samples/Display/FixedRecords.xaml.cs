using System.Data;
using System.Windows;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;

namespace IGDataGrid.Samples.Display
{
    /// <summary>
    /// Interaction logic for FixedRecords.xaml
    /// </summary>
    public partial class FixedRecords : SampleContainer
    {
        public FixedRecords()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dt = NWindDataSource.GetTable(NWindTable.Customers);
            this.XamDataGrid1.DataSource = dt.DefaultView;

            this.XamDataGrid1.ViewableRecords[0].FixedLocation = Infragistics.Windows.DataPresenter.FixedRecordLocation.FixedToTop;
            this.XamDataGrid1.ViewableRecords[1].FixedLocation = Infragistics.Windows.DataPresenter.FixedRecordLocation.FixedToBottom;
        }
    }
}
