using System;
using System.Xml.Linq;
using System.Linq;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;

namespace Infragistics.Samples.Shared.Models.TeamForce
{
    public class SalesRepository
    {
        public SalesRepository()
        {

        }

        private string selectedCustomerId = string.Empty;
        private Action<SalesRepositoryResult> callback;
        public void Execute(Action<SalesRepositoryResult> callback, string selectedCustomerId)
        {
            this.callback = callback;

            this.selectedCustomerId = selectedCustomerId;
            this.DownloadData();
        }

        private XmlDataProvider xmlDataProvider;
        private void DownloadData()
        {
            xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.GetXmlDataCompleted += new EventHandler<GetXmlDataCompletedEventArgs>(xmlDataProvider_GetXmlDataCompleted);
            xmlDataProvider.GetXmlDataAsync("TeamForceSales.xml");
        }

        void xmlDataProvider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs gxdcea)
        {
            XDocument doc = gxdcea.Result;
            if (doc != null)
            {
                SalesRepositoryResult salesData = this.MapSalesData(doc);
                this.callback.Invoke(salesData);
            }
        }

        private SalesRepositoryResult MapSalesData(XDocument doc)
        {
            var result = (from d in doc.Descendants("Customer")
                          where d.Attribute("ID").Value == this.selectedCustomerId
                          select new SalesRepositoryResult
                          {
                              ProductSalesData = this.MapProductSales(d),
                              SalesData = this.MapMonthlySales(d)
                          }).SingleOrDefault();

            return result;
        }

        private ProductSaleCollection MapProductSales(XElement parent)
        {
            var dataSource = (from d in parent.Descendants("ProductSales").Descendants("ProductSale")
                              select new ProductSale
                              {
                                  Category = d.Element("CategoryName").Value,
                                  TotalSales = d.Element("TotalSales").GetDouble(),
                                  Commission = d.Element("Commission").GetDouble(),
                                  MonthlyForcast = d.Element("MonthlyForcast").GetDouble(),
                                  FavoriteProduct = d.Element("FavoriteProduct").Value
                              });

            return new ProductSaleCollection(dataSource.ToList());
        }

        private SalesMonthCollection MapMonthlySales(XElement parent)
        {
            var dataSource = (from d in parent.Descendants("MonthSales").Descendants("MonthSale")
                              select new SalesMonth
                              {
                                  Name = d.Element("Month").Value,
                                  Value = d.Element("TotalSales").GetDouble()
                              });

            return new SalesMonthCollection(dataSource.ToList());
        }
    }
}