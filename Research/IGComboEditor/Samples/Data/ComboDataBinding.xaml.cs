using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Controls.Editors;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;

namespace IGComboEditor.Samples.Data
{
    public partial class ComboDataBinding : SampleContainer
    {
        public ComboDataBinding()
        {
            InitializeComponent();            
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {           
            DownloadDataSource();
            JPCustomValueEnteredActionCheck();
        }

        private void DownloadDataSource()
        {
            var xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.GetXmlDataCompleted += new EventHandler<GetXmlDataCompletedEventArgs>(xmlDataProvider_GetXmlDataCompleted);
            xmlDataProvider.GetXmlDataAsync("Customers.xml");
        }

        private void xmlDataProvider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs gxdcea)
        {
            XDocument doc = gxdcea.Result;

            var dataSource = (from d in doc.Descendants("Customers")
                              select new Customer
                              {
                                  Company = d.Element("CompanyName").Value,
                                  ContactName = d.Element("ContactName").Value
                              }).ToList();

            this.ComboEditor.ItemsSource = dataSource;
        }

        private void JPCustomValueEnteredActionCheck()
        {
            string isoLanguage = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            // customization
            if (isoLanguage.ToLower().Equals("ja"))
            {
                ComboEditor.CustomValueEnteredAction = CustomValueEnteredActions.Allow;
            }
        }
    }
}