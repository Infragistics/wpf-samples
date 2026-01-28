using System.Data;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;

namespace IGDataGrid.Samples.Display
{
    /// <summary>
    /// Interaction logic for EnablingFieldChooserUi.xaml
    /// </summary>
    public partial class EnablingFieldChooserUi : SampleContainer
    {
        public EnablingFieldChooserUi()
        {
            InitializeComponent();

            // Asign a data source.
            // 
            DataTable dt = NWindDataSource.GetTable(NWindTable.Orders);
            this.XamDataGrid1.DataSource = dt.DefaultView;
        }
    }
}
