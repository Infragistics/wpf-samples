using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;

namespace IGComboEditor.Samples.Data
{
    public class CustomersDataSource : ObservableModel
    {
        public CustomersDataSource()
        {
            DownloadDataSource();
        }

        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get
            {
                return this._customers;
            }
            set
            {
                if (this._customers != value)
                {
                    this._customers = value;
                    this.OnPropertyChanged("Customers");
                }
            }
        }

        private void DownloadDataSource()
        {
            var xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.GetXmlDataCompleted += new EventHandler<GetXmlDataCompletedEventArgs>(xmlDataProvider_GetXmlDataCompleted);
            xmlDataProvider.GetXmlDataAsync("Customers.xml");
        }

        private void xmlDataProvider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs gxdcea)
        {
            if (gxdcea.Error != null)
            {
                return;
            }

            XDocument doc = gxdcea.Result;

            var dataSource = (from d in doc.Descendants("Customers")
                              select new Customer
                              {
                                  Company = d.Element("CompanyName").Value,
                                  ContactName = d.Element("ContactName").Value,
                                  City = d.Element("City").Value,
                                  AddressOne = d.Element("Address").Value,
                                  Country = d.Element("Country").Value,
                                  ContactTitle = d.Element("ContactTitle").Value,
                                  Phone = d.Element("Phone").Value,
                                  ImageResourcePath = ImageFilePathProvider.GetImageLocalPath("People/" + d.Element("ImageResourcePath").Value)
                              });

            this.Customers = new ObservableCollection<Customer>(dataSource);
        }
    }
}
