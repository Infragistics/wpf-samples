using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;

namespace IGDataGrid.Samples.Display
{
    /// <summary>
    /// Interaction logic for MovableFields.xaml
    /// </summary>
    public partial class MovableFields : SampleContainer
    {
        public MovableFields()
        {
            InitializeComponent();

            XamDataGrid1.DataSource = NWindDataSource.GetTable(NWindTable.Customers, true).DefaultView;
        }
    }
}
