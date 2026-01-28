using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;

namespace IGDataGrid.Samples.Display
{
    /// <summary>
    /// Interaction logic for FixedFields.xaml
    /// </summary>
    public partial class FixedFields : SampleContainer
    {
        public FixedFields()
        {
            InitializeComponent();

            this.XamDataGrid1.DataSource = NWindDataSource.GetTable(NWindTable.Customers, true).DefaultView;
        }
    }
}
