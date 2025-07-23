using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;

namespace IGDataGrid.Samples.Organization
{
    /// <summary>
    /// Interaction logic for FieldSorting.xaml
    /// </summary>
    public partial class FieldSorting : SampleContainer
    {
        public FieldSorting()
        {
            InitializeComponent();

            this.XamDataGrid1.DataSource = NWindDataSource.GetTable(NWindTable.Customers, false).DefaultView;
        }
    }
}