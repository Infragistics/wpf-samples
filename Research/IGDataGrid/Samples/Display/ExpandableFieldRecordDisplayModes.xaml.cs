using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;

namespace IGDataGrid.Samples.Display
{
    /// <summary>
    /// Interaction logic for ExpandableFieldRecordDisplayModes.xaml
    /// </summary>
    public partial class ExpandableFieldRecordDisplayModes : SampleContainer
    {
        public ExpandableFieldRecordDisplayModes()
        {
            InitializeComponent();

            this.XamDataGrid1.DataSource = NWindDataSource.GetTable(NWindTable.Customers).DefaultView;
        }
    }
}
