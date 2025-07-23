using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Microsoft.Win32;

namespace IGBarcode.Samples.Data
{
    public partial class Printing : SampleContainer
    {
        private PrintDialog _printDialog;
        private SaveFileDialog _saveFileDialog;

        public Printing()
        {
            InitializeComponent();
            _printDialog = new PrintDialog();

            _saveFileDialog = new SaveFileDialog();
            _saveFileDialog.Filter = "PNG (*.png)|*.png";

            //Fill the Data Grid.
            DataGrid.ItemsSource = new List<ProductItem>()
                                   {
                                       new ProductItem("Adjustable Race", "1441780"),
                                       new ProductItem("Bearing Ball", "5302287"),
                                       new ProductItem("BB Ball Bearing", "5096471"),
                                       new ProductItem("Headset Ball Bearings", "5609091"),
                                       new ProductItem("Blade", "1124578"),
                                       new ProductItem("LL Crankarm", "2475662"),
                                       new ProductItem("ML Crankarm", "8251076"),
                                       new ProductItem("HL Crankarm", "4464661")
                                   };
        }

        //Print a simple barcode.
        private void OnPrintQRBracode(object sender, RoutedEventArgs e)
        {
            if (_printDialog.ShowDialog() == true)
            {
                _printDialog.PrintVisual(qrBarcode, "QR Barcode");
            }
        }

        //Save a barcode to an image.
        private void OnSaveQRBracode(object sender, RoutedEventArgs e)
        {
            int height = (int)qrBarcode.Height;
            int width = (int)qrBarcode.Width;

            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(qrBarcode);

            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            if (_saveFileDialog.ShowDialog() == true)
            {
                using (Stream stream = _saveFileDialog.OpenFile())
                {
                    encoder.Save(stream);
                }
            }
        }

        //Print the contents of the Data Grid.
        private void OnPrintDataGrid(object sender, RoutedEventArgs e)
        {
            if (_printDialog.ShowDialog() == true)
            {
                Size pageSize = new Size(_printDialog.PrintableAreaWidth, _printDialog.PrintableAreaHeight);

                DataGrid dataGrid = DataGrid;

                dataGrid.SelectedItems.Clear();
                dataGrid.Measure(pageSize);
                dataGrid.Arrange(new Rect(15, 15, pageSize.Width, pageSize.Height));

                _printDialog.PrintVisual(dataGrid, "Barcodes");
            }
        }
    }
   
    public class ProductItem : ObservableModel
    {
        public string Name { get; set; }
        public string Id { get; set; }

        public ProductItem(string name, string id)
        {
            this.Name = name;
            this.Id = id;
        }
    }
}
