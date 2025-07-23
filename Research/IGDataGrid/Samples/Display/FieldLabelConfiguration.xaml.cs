using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;

namespace IGDataGrid.Samples.Display
{
    /// <summary>
    /// Interaction logic for FieldLabelConfiguration.xaml
    /// </summary>
    public partial class FieldLabelConfiguration : SampleContainer
    {
        public FieldLabelConfiguration()
        {
            InitializeComponent();

            this.XamDataGrid1.DataSource = NWindDataSource.GetTable(NWindTable.Customers, true).DefaultView;
        }
    }
}
