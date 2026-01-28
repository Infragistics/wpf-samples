using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;

namespace IGDataGrid.Samples.Display
{
    /// <summary>
    /// Interaction logic for FieldSettingsOverrides.xaml
    /// </summary>
    public partial class FieldSettingsOverrides : SampleContainer
    {
        public FieldSettingsOverrides()
        {
            InitializeComponent();

            this.XamDataGrid1.DataSource = NWindDataSource.GetTable(NWindTable.Customers).DefaultView;
        }
    }
}
