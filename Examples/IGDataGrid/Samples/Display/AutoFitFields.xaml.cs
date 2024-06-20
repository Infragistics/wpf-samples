using System.Data;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Windows.DataPresenter;

namespace IGDataGrid.Samples.Display
{
    /// <summary>
    /// Interaction logic for AutoFitFields.xaml
    /// </summary>
    public partial class AutoFitFields : SampleContainer
    {
        public AutoFitFields()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataTable dt = NWindDataSource.GetTable(NWindTable.Customers);
            this.XamDataGrid1.DataSource = dt.DefaultView;
        }

        private void useStarSizing_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;

            if (cb.IsChecked == true)
            {
                XamDataGrid1.DefaultFieldLayout.Fields["CustomerID"].Width = new FieldLength(1, FieldLengthUnitType.Star);
                XamDataGrid1.DefaultFieldLayout.Fields["CompanyName"].Width = new FieldLength(4, FieldLengthUnitType.Star);
            }
            else
            {
                XamDataGrid1.DefaultFieldLayout.Fields["CustomerID"].Width = null;
                XamDataGrid1.DefaultFieldLayout.Fields["CompanyName"].Width = null;
            }
        }
    }
}
