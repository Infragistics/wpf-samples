using System.Linq;
using System.Windows;
using System.Xml.Linq;
using IGSlider.ViewModel;
using Infragistics.Controls.Editors;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Framework;

namespace IGSlider.Samples.Style
{
    public partial class TrackFill : Infragistics.Samples.Framework.SampleContainer
    {
        private PriceFilteringViewModel viewModel;
        public TrackFill()
        {
            InitializeComponent();

            this.viewModel = new PriceFilteringViewModel();
            this.DataContext = this.viewModel;

            Loaded += new RoutedEventHandler(TrackFill_Loaded);    
        }

        void TrackFill_Loaded(object sender, RoutedEventArgs e)
        {
            this.GetOrderHistory();
        }

        private void GetOrderHistory()
        {
            XmlDataProvider provider = new XmlDataProvider();
            provider.GetXmlDataAsync("OrderHistory.xml");
            provider.GetXmlDataCompleted += Provider_GetXmlDataCompleted;
        }

        /// <summary>
        /// Ths methods' XLinq Query uses special extention methods for converting data. 
        /// The extention methods can be found in the Common\XElementExtension class. 
        /// </summary>
        void Provider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            XDocument doc = e.Result;
            var dataSource = (from d in doc.Descendants("Row")
                              select new OrderHistory
                              {
                                  OrderDate = d.Attribute("OrderDate").GetDateTime(),
                                  ShipName = d.Attribute("ShipName").Value,
                                  Price = d.Attribute("Price").GetDouble(),
                                  CategoryName = d.Attribute("CategoryName").Value,
                                  ProductName = d.Attribute("ProductName").Value,
                                  Supplier = d.Attribute("Supplier").Value
                              });

            this.viewModel.OrderHistory = dataSource.ToList();

            if (this.RangeSlider != null)
            {
                double startingRange = this.RangeSlider.Thumbs[0].Value;
                double endingRange = this.RangeSlider.Thumbs[1].Value;

                this.viewModel.FilterData(startingRange, endingRange);
            }
            else
            {
                this.viewModel.FilterData(1000, 9000);
            }
        }

        private void RangeSlider_ThumbValueChanged(object sender, ThumbValueChangedEventArgs<double> e)
        {
            if (this.viewModel != null)
            {
                double startingRange = this.RangeSlider.Thumbs[0].Value;
                double endingRange = this.RangeSlider.Thumbs[1].Value;

                this.viewModel.FilterData(startingRange, endingRange);
            }
        }
    }
}
