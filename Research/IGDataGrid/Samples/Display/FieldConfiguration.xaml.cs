using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Samples.Framework;

namespace IGDataGrid.Samples.Display
{
    /// <summary>
    /// Interaction logic for FieldConfiguration.xaml
    /// </summary>
    public partial class FieldConfiguration : SampleContainer
    {
        public FieldConfiguration()
        {
            InitializeComponent();

            this.XamDataGrid1.DataSource = NWindDataSource.GetTable(NWindTable.Customers, true).DefaultView;
        }
    }
}
