using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using IGGrid.Resources;
using Infragistics.Controls.Grids;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Samples.Editing
{
    public partial class PasteOperation : SampleContainer
    {

        public PasteOperation()
        {
            InitializeComponent();
            Loaded += OnSampleLoaded;

            // Attach an event handler for the xamGrid ClipboardPasting event
            igGridTarget.ClipboardPasting += new EventHandler<ClipboardPastingEventArgs>(igGrid_ClipboardPasting);
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
                                  OnBackOrder = d.Element("UnitsInStock").GetInt() == 0,
                                  QuantityPerUnit = d.Element("QuantityPerUnit").GetString()
                              });

            this.igGridSource.ItemsSource = dataSource.ToList();
        }

        private void igGrid_ClipboardPasting(object sender, ClipboardPastingEventArgs e)
        {
            ObservableCollection<Product> products = AsObservableCollection(igGridTarget.ItemsSource);

            // The ClipboardPastingEventArgs parameter provides information about the selected data that will be pasted. 
            List<List<string>> parsedClipboardData = e.Values;

            foreach (var dataRow in parsedClipboardData)
            {
                try
                {
                    Product product = new Product();

                    product.Name = dataRow.ElementAt(0);
                    product.Category = dataRow.ElementAt(1);
                    product.UnitPrice = Convert.ToDouble(dataRow.ElementAt(2));
                    product.UnitsInStock = Convert.ToInt32(dataRow.ElementAt(3));

                    products.Add(product);
                }
                catch (FormatException)
                {
                    MessageBox.Show(GridStrings.XG_PasteOperation_FormatException_Msg);
                    return;
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show(GridStrings.XG_PasteOperation_ArgOutOfRangeExc_Msg);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

            igGridTarget.ItemsSource = products;
        }

        private ObservableCollection<Product> AsObservableCollection(IEnumerable iEnumerable)
        {
            var observableCollection = new ObservableCollection<Product>();

            if (iEnumerable != null)
            {
                foreach (var item in iEnumerable)
                    observableCollection.Add(item as Product);
            }

            return observableCollection;
        }

        private void BtnClearData_Click(object sender, RoutedEventArgs e)
        {
            // Clear the data in the target xamGrid control.
            igGridTarget.ItemsSource = null;
        }
    }
}
