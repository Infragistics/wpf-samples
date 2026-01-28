using System.Data;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using IGDockManager.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;

namespace IGDockManager.Samples.Display
{
    /// <summary>
    /// Interaction logic for HWnd.xaml
    /// </summary>
    public partial class HWndSupport : SampleContainer
    {
        public HWndSupport()
        {
            InitializeComponent();

            // get sample date, which will be visualized in the WinForms date grid
            DataTable dt = NWindDataSource.GetTable(NWindTable.Customers);

            // create the host, which will contains the WinForms control
            WindowsFormsHost _WinFormsHost1 = new WindowsFormsHost();
            // create standard WinForms data grid
            DataGridView _WinFormsGrid1 = new DataGridView();
            // set the data source and set display fields for the columns
            _WinFormsGrid1.DataSource = dt.DefaultView;
            _WinFormsGrid1.AutoGenerateColumns = false;
            AddColumn(_WinFormsGrid1, DockManagerStrings.ColCustomerID, "CustomerID");
            AddColumn(_WinFormsGrid1, DockManagerStrings.ColContactName, "ContactName");
            AddColumn(_WinFormsGrid1, DockManagerStrings.ColCompanyName, "CompanyName");
            _WinFormsHost1.Child = _WinFormsGrid1;
            this.leftEdgeDock.Content = _WinFormsHost1;

            // create the host, which will contains the WinForms control
            WindowsFormsHost _WinFormsHost2 = new WindowsFormsHost();
            // create standard WinForms data grid
            DataGridView _WinFormsGrid2 = new DataGridView();
            // set the data source and set display fields for the columns
            _WinFormsGrid2.DataSource = dt.DefaultView;
            _WinFormsGrid2.AutoGenerateColumns = false;
            AddColumn(_WinFormsGrid2, DockManagerStrings.ColContactName, "ContactName");
            AddColumn(_WinFormsGrid2, DockManagerStrings.ColAddress, "Address");
            AddColumn(_WinFormsGrid2, DockManagerStrings.ColCity, "City");
            AddColumn(_WinFormsGrid2, DockManagerStrings.ColPhone, "Phone");
            _WinFormsHost2.Child = _WinFormsGrid2;
            this.rightEdgeDock.Content = _WinFormsHost2;
        }

        private void AddColumn(DataGridView Grid, string Header, string DataPropertyName)
        {
            DataGridViewColumn col = new DataGridViewColumn();
            col.CellTemplate = new DataGridViewTextBoxCell();
            col.HeaderText = Header;
            col.DataPropertyName = DataPropertyName;
            Grid.Columns.Add(col);
        }
    }
}
