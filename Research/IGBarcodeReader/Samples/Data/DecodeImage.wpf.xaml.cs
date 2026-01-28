using System;
using System.Windows;
using System.Windows.Media.Imaging;
using IGBarcodeReader.Resources;
using Infragistics.Controls.Barcodes;
using Microsoft.Win32;

namespace IGBarcodeReader.Samples.Data
{
    public partial class DecodeImage : Infragistics.Samples.Framework.SampleContainer
    {
        //An instance of the Barcode Reader.
        private Infragistics.Controls.Barcodes.BarcodeReader _barcodeReader;

        private BitmapImage _image;

        private OpenFileDialog _openFileDialog;

        public DecodeImage()
        {
            InitializeComponent();

            //Initialization of the Barcode Reader.
            _barcodeReader = new Infragistics.Controls.Barcodes.BarcodeReader();
            _barcodeReader.DecodeComplete += new EventHandler<ReaderDecodeArgs>(BarcodeReader_DecodeComplete);

            //Loading a default image.
            _image = new BitmapImage(new Uri("/IGBarcodeReader;component/Images/QRCode.jpg", UriKind.Relative));
            ImageContainer.Source = _image;

            //Initialization of the Open File Dialog.
            _openFileDialog = new OpenFileDialog()
            {
                Filter = "Image Files (*.png, *.jpg)|*.png;*.jpg"
            };
        }

        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            if (_openFileDialog.ShowDialog() == true)
            {
                _image = new BitmapImage(new Uri(_openFileDialog.FileName, UriKind.Absolute));
                ImageContainer.Source = _image;

                TextBoxData.Text = string.Empty;
            }
        }

        private void ButtonDecode_Click(object sender, RoutedEventArgs e)
        {
            TextBoxData.Text = string.Empty;

            //Decoding an image.
            _barcodeReader.Decode(_image);
        }

        private void BarcodeReader_DecodeComplete(object sender, ReaderDecodeArgs e)
        {
            //Setting the displayed image to the FilteredImage property of the Barcode Reader.
            //The Filtered Image will show a rectangle with the found barcode.
            ImageContainer.Source = _barcodeReader.FilteredImage;

            if (e.SymbolFound)
            {
                TextBoxData.Text += string.Format
                    (
                        BarcodeReaderStrings.BarcodeReader_DecodedString,
                        e.Symbology, //The Symbology of the decoded barcode.
                        e.Value,     //The Value of the decoded barcode.
                        Environment.NewLine
                    );
            }
            else
            {
                TextBoxData.Text = BarcodeReaderStrings.BarcodeReader_NoSymbology;
            }

            ScrollViewerData.UpdateLayout();
            ScrollViewerData.ScrollToVerticalOffset(double.MaxValue);
        }
    }
}
