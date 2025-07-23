using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;

namespace Infragistics.Samples.Shared.Models.TeamForce
{
    public class CustomerRepository
    {

        public CustomerRepository()
        {

        }

        private Action<IList<Customer>> callback;
        public void Execute(Action<IList<Customer>> callback)
        {
            this.callback = callback;
            this.DownloadData();
        }

        private XmlDataProvider xmlDataProvider;
        private void DownloadData()
        {
            xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.GetXmlDataCompleted += new EventHandler<GetXmlDataCompletedEventArgs>(xmlDataProvider_GetXmlDataCompleted);
            xmlDataProvider.GetXmlDataAsync("TeamForceContacts.xml");
        }

        void xmlDataProvider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs gxdcea)
        {
            XDocument doc = gxdcea.Result;
            if (doc != null)
            {
                IList<Customer> customers = this.MapCustomers(doc);
                this.callback.Invoke(customers);
            }
        }

        private IList<Customer> MapCustomers(XDocument doc)
        {
            var dataSource = (from d in doc.Descendants("Customers")
                              select new Customer
                              {
                                  CustomerID = d.Element("CustomerID").Value,
                                  Company = d.Element("CompanyName").Value,
                                  ContactName = d.Element("ContactName").Value,
                                  ContactTitle = d.Element("ContactTitle").Value,
                                  ContactEmail = d.Element("Email").Value,
                                  AddressOne = d.Element("Address").Value,
                                  Phone = d.Element("Phone").GetString(),
                                  Fax = d.Element("Fax").GetString(),
                                  City = d.Element("City").Value,
                                  Region = d.Element("Region").GetString(),
                                  Country = d.Element("Country").Value,
                                  ImageResourcePath = ImageFilePathProvider.GetImageLocalPath() + d.Element("ImageResourcePath").Value
                              });

            return dataSource.ToList();
        }
    }
}