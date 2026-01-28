using System;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using IGSlider.ViewModel;
using Infragistics.Controls.Editors;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Framework;

namespace IGSlider.Samples.Organization
{
    public partial class FilteringViewModelData : Infragistics.Samples.Framework.SampleContainer
    {
        private OrderDateFilteringViewModel viewModel;
        public FilteringViewModelData()
        {
            InitializeComponent();

            this.viewModel = new OrderDateFilteringViewModel();
            this.DataContext = this.viewModel;

            Loaded += new RoutedEventHandler(OnSampleLoaded);             
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.GetOrderHistory();
            this.SliderDateRange.ThumbValueChanged += SliderDateRange_ThumbValueChanged;
        }

        private void GetOrderHistory()
        {
            XmlDataProvider provider = new XmlDataProvider();
            provider.GetXmlDataAsync("OrderHistory.xml");
            provider.GetXmlDataCompleted += Provider_GetXmlDataCompleted;
        }

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

            if (this.SliderDateRange != null)
            {
                DateTime startingRange = this.SliderDateRange.Thumbs[0].Value;
                DateTime endingRange = this.SliderDateRange.Thumbs[1].Value;

                this.viewModel.FilterData(startingRange, endingRange);
            }
            else
            {
                this.viewModel.FilterData(new DateTime(2009, 3, 4), new DateTime(2009, 6, 5));
            }
        }

        private void SliderDateRange_ThumbValueChanged(object sender, ThumbValueChangedEventArgs<DateTime> e)
        {
            if (this.SliderDateRange != null)
            {
                DateTime startingRange = this.SliderDateRange.Thumbs[0].Value;
                DateTime endingRange = this.SliderDateRange.Thumbs[1].Value;

                this.viewModel.FilterData(startingRange, endingRange);
            }
        }
    }
}
