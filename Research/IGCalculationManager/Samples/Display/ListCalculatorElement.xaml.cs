using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using IGCalculationManager.ViewModels;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;

namespace IGCalculationManager.Samples.Display
{
    public partial class ListCalculatorElement : Infragistics.Samples.Framework.SampleContainer
    {
        public ListCalculatorElement()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        public IEnumerable<ShippingDetail> AllShippingDetails { get; set; }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            XmlDataProvider xmlProvider = new XmlDataProvider();
            xmlProvider.GetXmlDataAsync("ShippingDetails.xml");
            xmlProvider.GetXmlDataCompleted += xmlProvider_GetXmlDataCompleted;
        }

        void xmlProvider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            XDocument doc = e.Result;
            var dataSource = (from d in doc.Descendants("ShippingDetails")
                              select new ShippingDetail
                              {
                                  ItemDescription = d.Element("ItemDescription").Value,
                                  Price = d.Element("Price").GetDecimal(),
                                  Shipping = d.Element("Shipping").GetDecimal(),
                                  Quantity = d.Element("Quantity").GetInt()
                              });

            AllShippingDetails = dataSource.ToList();
            dataGrid.DataSource = dataSource.ToList();
            this.DataContext = this;
        }
    }
}
