using Infragistics.Controls.Grids;
using Infragistics.Controls.Layouts;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace IGDockManager.Samples.Organization
{
    public partial class OrganizingDataWithDockManager : SampleContainer
    {
        public OrganizingDataWithDockManager()
        {
            InitializeComponent();
        }

        private void xamDockManager_PaneClosing(object sender, CancellablePaneEventArgs e)
        {
            // Cancel closing of the main content panes with data
            if (e.Pane.Name == "CP_Data" || e.Pane.Name == "CP_Chart")
            {
                e.Cancel = true;
            }
        }

        private void xamGrid_SelectedRowsCollectionChanged(object sender, SelectionCollectionChangedEventArgs<SelectedRowsCollection> e)
        {
            bool isOpened = true;
            // Handle xamGrid row selection
            if (e.NewSelectedItems[0] != null && e.NewSelectedItems[0].RowType == RowType.DataRow)
            {
                //ContentPane detailsPane = xamDockManager.FindPane("CP_Properties") as ContentPane;
                foreach (PaneBase pane in xamDockManager.ClosedPanes)
                {
                    if (pane.Name == "CP_Properties")
                    {
                        isOpened = false;
                    }
                }
                if (isOpened)
                {
                    Row selectedRow = e.NewSelectedItems[0] as Row;

                    Product product = selectedRow.Data as Product;

                    TextBlock Txt_QuantityPerUnit = this.FindName("Txt_QuantityPerUnit") as TextBlock;
                    Txt_QuantityPerUnit.Text = product.QuantityPerUnit;

                    TextBlock Txt_UnitsOnOrder = this.FindName("Txt_UnitsOnOrder") as TextBlock;
                    Txt_UnitsOnOrder.Text = product.UnitsOnOrder.ToString();

                    TextBlock Txt_Supplier = this.FindName("Txt_Supplier") as TextBlock;
                    Txt_Supplier.Text = product.Supplier;
                }
            }
        }

        private Image createImageFromUri(Uri uri)
        {
            Image image = new Image();
            BitmapImage bmpImage = new BitmapImage(uri);
            image.Source = bmpImage;
            image.Stretch = Stretch.None;
            image.HorizontalAlignment = HorizontalAlignment.Center;
            image.VerticalAlignment = VerticalAlignment.Center;
            return image;
        }

        private void ChartRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            XamDockManager dockMan = this.FindName("xamDockManager") as XamDockManager;
            if (dockMan != null && dockMan.DocumentContentHost != null)
            {
                if (dockMan.DocumentContentHost.Panes.Count == 1)
                {
                    TabGroupPane tgp = dockMan.DocumentContentHost.Panes[0];
                    if (tgp.Panes.Count == 2)
                    {
                        ContentPane p = tgp.Panes[1];
                        Image img = p.Content as Image;
                        if (img != null)
                        {
                            Uri imageUri = ImageFilePathProvider.GetImageLocalUri(((RadioButton)sender).Tag.ToString());
                            img.Source = new BitmapImage(imageUri);
                        }
                    }
                }
            }
        }

        private void xamGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is XamGrid)
            {
                XamGrid dataGrid = (XamGrid)sender;
                if (dataGrid != null && dataGrid.Rows[0] != null)
                {
                    // Select the first row in the xamGrid
                    dataGrid.Rows[0].IsSelected = true;
                }
            }
        }
    }

    public class CustomViewModel : INotifyPropertyChanged
    {
        private IEnumerable iEnumerable;
        public event PropertyChangedEventHandler PropertyChanged;

        public CustomViewModel()
        {
            XmlDataProvider _xmlDataProvider = new XmlDataProvider();
            _xmlDataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            _xmlDataProvider.GetXmlDataAsync("Products.xml");
        }

        private void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            XDocument doc = e.Result;
            var dataSource = (from d in doc.Descendants("Products")
                              where d.Element("UnitsInStock").GetInt() > 50 && d.Element("UnitPrice").GetDecimal() > 0
                              select new Product
                              {
                                  SKU = d.Element("ProductID").Value,
                                  Name = d.Element("ProductName").Value,
                                  Category = d.Element("Category").Value,
                                  Supplier = d.Element("Supplier").Value,
                                  UnitPrice = d.Element("UnitPrice").GetDouble(),
                                  UnitsInStock = d.Element("UnitsInStock").GetInt(),
                                  UnitsOnOrder = d.Element("UnitsOnOrder").GetInt(),
                                  QuantityPerUnit = d.Element("QuantityPerUnit").Value
                              });

            this.iEnumerable = dataSource.ToList();
            OnPropertyChanged("Products");
        }

        public IEnumerable Products
        {
            get { return this.iEnumerable; }
        }

        private void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
    }
}
