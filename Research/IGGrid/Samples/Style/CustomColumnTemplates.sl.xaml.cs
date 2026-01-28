using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Controls.Grids;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Samples.Style
{
    public partial class CustomColumnTemplates : SampleContainer
    {

        public CustomColumnTemplates()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {

            DownloadDataSource();
        }

        private void DownloadDataSource()
        {
            var _dataProvider = new XmlDataProvider();
            _dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            _dataProvider.GetXmlDataAsync("FurnitureData.xml");
        }

        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            XDocument doc = e.Result;

            this.dataGrid.ItemsSource = this.GetCategories(doc.Root);
            this.expandRows(this.dataGrid.Rows);
        }

        private void expandRows(RowCollection rows)
        {
            foreach (Row row in rows)
            {
                row.IsExpanded = true;
                if (row.HasChildren)
                {
                    // Grab this row's RowCollection, then recursively parse this collection //
                    expandRows(row.ChildBands[0].Rows);
                }
            }
        }

        private IEnumerable<Category> GetCategories(XElement parent)
        {
            return (from d in parent.Elements("Category")
                    select new Category
                    {
                        Name = d.Attribute("Name").GetString(),
                        Categories = this.GetCategories(d),
                        Products = this.GetProducts(d),
                    }).ToList();
        }

        private IEnumerable<Product> GetProducts(XElement parent)
        {
            return (from d in parent.Elements("Product")
                    select new Product
                    {
                        SKU = d.Attribute("SKU").GetString(),
                        Name = d.Attribute("Name").GetString(),
                        Designer = d.Attribute("Designer").GetString(),
                        InStock = d.Attribute("InStock").GetBool(),
                        UnitPrice = d.Attribute("Price").GetDouble(),
                        ImageUrl = d.Attribute("ImageUrl").GetString(),
                    }).ToList();
        }
    }
}
