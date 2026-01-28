using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Printing;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;

namespace IGBarcode.Samples.Data
{
    public partial class Printing : SampleContainer
    {
        //An items control, which will be used to represent
        //the print page.
        private ItemsControl _printPage;

        public Printing()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            //Initialization of the print page.
            _printPage = new ItemsControl();
            _printPage.Width = 400;
            _printPage.ItemTemplate = (DataTemplate)this.Resources["ProductTemplate"];

            //Fill the Data Grid.
            DataGrid.ItemsSource = GetData();
        }

        //Print a simple barcode.
        private void OnPrintQRBracode(object sender, RoutedEventArgs e)
        {
            var printDocument = new PrintDocument();

            printDocument.PrintPage += (eventSender, eventArgs) =>
            {
                //Set the QR Barcode as PageVisual.
                eventArgs.PageVisual = qrBarcode;
            };

            printDocument.Print("QR Barcode");
        }

        //Print the contents of the Data Grid.
        private void OnPrintDataGrid(object sender, RoutedEventArgs e)
        {
            var printDocument = new PrintDocument();

            printDocument.PrintPage += (eventSender, eventArgs) =>
            {
                //Get the items from the Data Grid
                //and pass them to the print page.
                _printPage.ItemsSource = DataGrid.ItemsSource;

                //Set the print page as PageVisual.
                eventArgs.PageVisual = _printPage;
            };

            printDocument.Print("Barcodes");
        }

        private static ObservableCollection<Product> GetData()
        {
            return new ObservableCollection<Product>()
            {
                new Product("Adjustable Race", "1441780"),
                new Product("Bearing Ball", "5302287"),
                new Product("BB Ball Bearing", "5096471"),
                new Product("Headset Ball Bearings", "5609091"),
                new Product("Blade", "1124578"),
                new Product("LL Crankarm", "2475662"),
                new Product("ML Crankarm", "8251076"),
                new Product("HL Crankarm", "4464661")
            };
        }
    }

    public class Product : ObservableModel
    {
        public string Name { get; set; }
        public string Id { get; set; }

        public Product(string name, string id)
        {
            this.Name = name;
            this.Id = id;
        }
    }
}
