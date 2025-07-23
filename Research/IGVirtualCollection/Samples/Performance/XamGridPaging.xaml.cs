using System;
using System.Windows;
using Infragistics.Collections;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;

namespace IGVirtualCollection.Samples.Performance
{
    public partial class XamGridPaging : SampleContainer
    {
        VirtualCollection<Customer> vc;

        public XamGridPaging()
        {
            InitializeComponent();
        }

        private void GridContainer_Loaded(object sender, RoutedEventArgs e)
        {
            // obtain total products count on page load
            GetIndexedProductsCount();
        }

        private void GetIndexedProductsCount()
        {
            CustomersDataProvider cdp = new CustomersDataProvider();
            cdp.LoadProductsCountComplete += client_SelectNodesFromXmlFileCountCompleted;
            cdp.LoadProductsCountAsync();
        }

        void client_SelectNodesFromXmlFileCountCompleted(object Sender, LoadCustomersCountCompletedEventArgs e)
        {
            int productsCount = e.Result;
            this.ASizeTB.Text = "" + productsCount;
            this.vcSlider.MaxValue = productsCount;

            // create the Vitual Collection, with the received anticipated size from the service
            vc = new VirtualCollection<Customer>(productsCount);
            vc.ItemDataRequested += new EventHandler<ItemDataRequestedEventArgs>(vc_ItemDataRequested);
            vc.LoadSize = 18;
            vc.PageSize = 18;

            // set the Virtual Collection on the grid
            this.XamGrid1.ItemsSource = vc;
        }

        void vc_ItemDataRequested(object sender, ItemDataRequestedEventArgs e)
        {
            // when the grid is requesting data, pass the async request to the service
            GetIndexProductsRange(e.StartIndex, e.ItemsCount);
        }

        private void GetIndexProductsRange(int startIndex, int count)
        {
            CustomersDataProvider cdp = new CustomersDataProvider();
            cdp.LoadProductsRangeComplete += client_SelectNodesFromXmlFileRangeCompleted;
            cdp.LoadProductsRangeAsync(startIndex, count);
        }

        void client_SelectNodesFromXmlFileRangeCompleted(object sender, LoadCustomersRangeCompletedEventArgs e)
        {
            int count = e.Result.Count;
            this.LSizeTB.Text = "" + count;
            this.StartTB.Text = "" + e.startIndex;
            this.vcSlider.Thumbs[0].Value = e.startIndex;
            this.vcSlider.Thumbs[1].Value = e.startIndex + count - 1;

            ObservableCollectionExtended<Customer> items = new ObservableCollectionExtended<Customer>();
            foreach (Customer cus in e.Result)
            {
                items.Add(cus);
            }

            // when data is received from the service - load it in the Virtual Collection, so the grid can display it
            vc.LoadItems(e.startIndex, items);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (vc != null)
            {
                vc.Refresh();
            }
        }
    }
}
