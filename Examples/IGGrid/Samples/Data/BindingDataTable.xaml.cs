using Infragistics.Controls.Grids;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Models.Common;
using System;
using System.Data;

namespace IGGrid.Samples.Data
{
    public partial class BindingDataTable : SampleContainer
    {
        public BindingDataTable()
        {
            InitializeComponent();        
        }

        private void OnDataObjectRequested(object sender, DataObjectCreationEventArgs e)
        {
            e.NewObject = DataProvider.ProcessDataObjectCreation(e.ObjectType);
        }
    }

    public class DataProvider : ObservableModel
    {
        private static DataTable _dataTable;
        public DataView DataViewOrders { get; set; }

        public DataProvider()
        {
            _dataTable = NWindDataSource.GetTable(NWindTable.Customers);
            DataViewOrders = _dataTable.DefaultView;
        }

        public static DataRowView ProcessDataObjectCreation(Type objectType)
        {
            DataRowView result = null;
            if (_dataTable != null && objectType == typeof(DataRowView))
            {
                result = new DataView(_dataTable).AddNew();
            }

            return result;
        }
    }
}
