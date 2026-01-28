using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using IGGrid.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Samples.Style
{
    public partial class ColumnSpecificStyles : SampleContainer
    {

        public ColumnSpecificStyles()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            DownloadDataSource();
        }

        private void DownloadDataSource()
        {
            var _dataProvider = new XmlDataProvider();
            _dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            _dataProvider.GetXmlDataAsync("Products.xml");
        }

        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            XDocument doc = e.Result;
            var dataSource = (from d in doc.Descendants("Products")
                              select new ProductLocal
                              {
                                  SKU = d.Element("ProductID").GetString(),
                                  Name = d.Element("ProductName").GetString(),
                                  Category = d.Element("Category").GetString(),
                                  UnitsInStock = d.Element("UnitsInStock").GetInt(),
                                  UnitsOnOrder = d.Element("UnitsOnOrder").GetInt()
                              });

            this.igGrid.ItemsSource = dataSource.ToList();
        }

        private void igGrid_BindingValidationError(object sender, ValidationErrorEventArgs e)
        {
            (e.OriginalSource as TextBox).Focus();
        }
    }

    public class ProductLocal : Product
    {
        public ProductLocal() { }
        private string sku;
        new public string SKU
        {
            get
            {
                return this.sku;
            }
            set
            {
                if (this.sku != value)
                {
                    this.sku = value;
                    this.OnPropertyChanged("SKU");
                    bool valid = ValidationHandler.ValidateRule("SKU", GridStrings.XWG_Notification,
                                                               () => (value.Length > 0));
                }
            }
        }
    }
}
