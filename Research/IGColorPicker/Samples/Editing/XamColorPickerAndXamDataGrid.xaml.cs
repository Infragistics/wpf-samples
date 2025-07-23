using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;
using Infragistics.Windows.DataPresenter;

namespace IGColorPicker.Samples.Editing
{
    public partial class XamColorPickerAndXamDataGrid : SampleContainer
    {
        public XamColorPickerAndXamDataGrid()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }
        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.DownloadDataSource();
        }

        private void DownloadDataSource()
        {
            var _dataProvider = new XmlDataProvider();
            _dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            _dataProvider.GetXmlDataAsync("CustomersOrders.xml");
        }

        /// <summary>
        /// Ths methods' XLinq Query uses special extention methods for converting data. 
        /// The extention methods can be found in the Common\XElementExtension class. 
        /// </summary>
        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            XDocument doc = e.Result;
            var dataSource = (from d in doc.Descendants("Customers")
                              select new Customer
                              {
                                  CustomerID = d.Element("CustomerID").GetString(),
                                  Company = d.Element("CompanyName").GetString(),
                                  ContactName = d.Element("ContactName").GetString(),
                                  ContactTitle = d.Element("ContactTitle").GetString(),
                                  AddressOne = d.Element("Address").GetString(),
                                  City = d.Element("City").GetString(),
                                  Region = d.Element("Region").GetString(),
                                  Country = d.Element("Country").GetString(),
                                  Orders = d.GetOrders()
                              }).ToList<Customer>();

            this.MyDataGrid.DataSource = dataSource;
        }

        private void MyColorPicker_SelectedColorChanged(object sender, Infragistics.Controls.Editors.SelectedColorChangedEventArgs e)
        {
            SolidColorBrush MySelectedColor = new SolidColorBrush((Color)e.NewColor);

            // create a new style based on the default cell style of the cell
            System.Windows.Style s = new System.Windows.Style(typeof(CellValuePresenter));              

            // add a new setter for the foreground property using the selected color from the color picker
            Setter NewSetter = new Setter();
            NewSetter.Property = ForegroundProperty;
            NewSetter.Value = MySelectedColor;
            s.Setters.Add(NewSetter);

            // set the style on the selected cell
            if(this.MyDataGrid.SelectedItems.Cells.Count != 0)
            {
                Cell cell = this.MyDataGrid.SelectedItems.Cells[0];
                cell.Field.CellValuePresenterStyle = s;
            }                   
        }
    }
}
