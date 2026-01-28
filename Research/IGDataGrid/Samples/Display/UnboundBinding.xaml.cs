using System.Globalization;
using System.Windows.Data;
using Infragistics.Samples.Framework;
using Infragistics.Windows.DataPresenter;
using Infragistics.Windows.DataPresenter.Events;

namespace IGDataGrid.Samples.Display
{
    /// <summary>
    /// Interaction logic for AlternateBindings.xaml
    /// </summary>
    public partial class AlternateBindings : SampleContainer
    {
        public AlternateBindings()
        {
            InitializeComponent();
            SetDataSourcePath();
        }

        private void SetDataSourcePath()
        {
            var source = this.TryFindResource("OrdersData") as XmlDataProvider;
            if (source != null)
            {
                source.Source = Infragistics.Samples.Shared.DataProviders.XmlDataProvider.GetXmlUri("Orders.xml");
                source.XPath = "/Orders";
            }
        }

        private void XamDataGrid1_InitializeRecord(object sender, InitializeRecordEventArgs e)
        {
            if (e.Record is DataRecord)
            {
                //get the current records data, perform the appropriate
                //calculations and assign the values to the unbound fields
                var dr = (DataRecord)e.Record;

                int quantity = int.Parse(dr.Cells["Quantity"].Value.ToString());
                double cost = double.Parse(dr.Cells["CostPerUnit"].Value.ToString(), CultureInfo.InvariantCulture);
                double sh = double.Parse(dr.Cells["ShipAndHandle"].Value.ToString(), CultureInfo.InvariantCulture);

                double subtotal = (quantity * cost) + sh;
                double salestax = subtotal * .07;
                double total = subtotal + salestax;
                dr.Cells["SubTotal"].Value = subtotal;
                dr.Cells["SalesTax"].Value = salestax;
                dr.Cells["Total"].Value = total;
            }
        }
    }
}
