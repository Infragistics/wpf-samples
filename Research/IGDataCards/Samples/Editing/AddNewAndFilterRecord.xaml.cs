using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;

namespace IGDataCards.Samples.Editing
{
    /// <summary>
    /// Interaction logic for AddNewAndFilterRecord.xaml
    /// </summary>
    public partial class AddNewAndFilterRecord : SampleContainer
    {
        public AddNewAndFilterRecord()
        {
            InitializeComponent();

            this.xamDataCards1.DataSource = NWindDataSource.GetTable(NWindTable.Customers, true).DefaultView;
        }
    }
}