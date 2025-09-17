using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using IGGrid.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Samples.Display
{
    public partial class ComboBoxColumn : SampleContainer
    {

        public ComboBoxColumn()
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
                              select new Product
                              {
                                  SKU = d.Element("ProductID").GetString(),
                                  Name = d.Element("ProductName").GetString(),
                                  Category = d.Element("Category").GetString(),
                                  Supplier = d.Element("Supplier").GetString(),
                                  UnitPrice = d.Element("UnitPrice").GetDouble(),
                                  UnitsInStock = d.Element("UnitsInStock").GetInt(),
                                  UnitsOnOrder = d.Element("UnitsOnOrder").GetInt(),
                                  QuantityPerUnit = d.Element("QuantityPerUnit").GetString()
                              });

            this.dataGrid.ItemsSource = dataSource.ToList();
        }
    }
    public class OptionsList : ObservableCollection<OptionElement>
    {
        public OptionsList()
        {
            this.Add(new OptionElement() { Name = GridStrings.CategoryProvider_Beverages });
            this.Add(new OptionElement() { Name = GridStrings.CategoryProvider_Condiments });
            this.Add(new OptionElement() { Name = GridStrings.CategoryProvider_Confections });
            this.Add(new OptionElement() { Name = GridStrings.CategoryProvider_DiaryProducts });
            this.Add(new OptionElement() { Name = GridStrings.CategoryProvider_Grains });
            this.Add(new OptionElement() { Name = GridStrings.CategoryProvider_Meat });
            this.Add(new OptionElement() { Name = GridStrings.CategoryProvider_Produce });
            this.Add(new OptionElement() { Name = GridStrings.CategoryProvider_Seafood });
        }
    }

    public class OptionElement : ObservableModel
    {
        private string name;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }
    }
}
