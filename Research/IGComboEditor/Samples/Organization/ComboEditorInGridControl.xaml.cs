using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Controls.Editors;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGComboEditor.Samples.Organization
{
    public partial class ComboEditorInGridControl : SampleContainer
    {
        private string _isoLangName;

        public ComboEditorInGridControl()
        {
            InitializeComponent();           
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            _isoLangName = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            DownloadDataSource();
        }

        private void DownloadDataSource()
        {
            var xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.GetXmlDataCompleted += new EventHandler<GetXmlDataCompletedEventArgs>(xmlDataProvider_GetXmlDataCompleted);
            xmlDataProvider.GetXmlDataAsync("Products.xml");
        }

        void xmlDataProvider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs gxdcea)
        {
            XDocument doc = gxdcea.Result;

            var dataSource = (from d in doc.Descendants("Products")
                              select new Product
                              {
                                  SKU = d.Element("ProductID").Value,
                                  Name = d.Element("ProductName").Value,
                                  Category = d.Element("Category").Value,
                                  UnitPrice = d.Element("UnitPrice").GetDouble(),
                                  UnitsInStock = d.Element("UnitsInStock").GetInt(),
                              }).ToList();

            this.dataGrid.ItemsSource = dataSource;
        }

        private void CategoryEditList_Loaded(object sender, RoutedEventArgs e)
        {
            var comboEditor = sender as XamComboEditor;
            JPCustomValueEnteredActionCheck(comboEditor);
        }

        private void JPCustomValueEnteredActionCheck(XamComboEditor comboEditor)
        {
            string isoLanguage = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            // customization
            if (isoLanguage.ToLower().Equals("ja"))
            {
                comboEditor.CustomValueEnteredAction = CustomValueEnteredActions.Allow;
            }
        }
    }
}