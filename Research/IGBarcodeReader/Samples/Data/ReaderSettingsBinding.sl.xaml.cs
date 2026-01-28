using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using IGBarcodeReader.Resources;
using Infragistics.Controls.Barcodes;

namespace IGBarcodeReader.Samples.Data
{
    public partial class ReaderSettingsBinding : Infragistics.Samples.Framework.SampleContainer
    {
        //An instance of the Barcode Reader.
        private Infragistics.Controls.Barcodes.BarcodeReader _barcodeReader;

        private BitmapImage _image;

        private OpenFileDialog _openFileDialog;

        //Lists with predefined symbology presets.
        private List<Symbology> _linearSymbologies;
        private List<Symbology> _eanUpcSymbologies;
        private List<Symbology> _2dSymbologies;

        public ReaderSettingsBinding()
        {
            InitializeComponent();

            //Initialize the predefined presets with symbologies.
            _linearSymbologies = new List<Symbology>() { Symbology.Code128, Symbology.Code39Ext, Symbology.Interleaved2Of5 };
            _eanUpcSymbologies = new List<Symbology>() { Symbology.Ean13, Symbology.Ean8, Symbology.UpcA, Symbology.UpcE };
            _2dSymbologies = new List<Symbology>() { Symbology.QRCode };

            //Create bindings for the properties of the Barcode Reader.
            Binding maxNumberOfSymbolsToReadBinding = new Binding("Value") { Source = SliderNumberOfSymbols };
            Binding minSymbolSizeBinding = new Binding("Value") { Source = SliderMinSymbolSize };
            Binding barcodeOrientationBinding = new Binding("SelectedItem") { Source = ComboBoxOrientation };

            //Initialization of the Barcode Reader.
            _barcodeReader = new Infragistics.Controls.Barcodes.BarcodeReader();
            _barcodeReader.DecodeComplete += new EventHandler<ReaderDecodeArgs>(BarcodeReader_DecodeComplete);

            //Assign the bindings to the initialized Barcode Reader.
            BindingOperations.SetBinding
                (
                    _barcodeReader,
                    Infragistics.Controls.Barcodes.BarcodeReader.MaxNumberOfSymbolsToReadProperty,
                    maxNumberOfSymbolsToReadBinding
                );
            BindingOperations.SetBinding
                (
                    _barcodeReader,
                    Infragistics.Controls.Barcodes.BarcodeReader.MinSymbolSizeProperty,
                    minSymbolSizeBinding
                );
            BindingOperations.SetBinding
                (
                    _barcodeReader,
                    Infragistics.Controls.Barcodes.BarcodeReader.BarcodeOrientationProperty,
                    barcodeOrientationBinding
                );

            //Set the DataContext of the Filtered Image Container, which is bound to the FilteredImage property.
            FilteredImageContainer.DataContext = _barcodeReader;

            //Set predefined reader settings.
            LoadSettings("Linear", _linearSymbologies, 5);

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
                using (Stream stream = _openFileDialog.File.OpenRead())
                {
                    _image.SetSource(stream);
                }
                ImageContainer.Source = _image;

                TextBoxData.Text = string.Empty;
            }
        }

        private void ButtonDecode_Click(object sender, RoutedEventArgs e)
        {
            TextBoxData.Text = string.Empty;

            //Set the Maximum Number of Symbols to Read.
            _barcodeReader.MaxNumberOfSymbolsToRead = Convert.ToInt32(SliderNumberOfSymbols.Value);

            //Set the Minimum Symbol Size.
            _barcodeReader.MinSymbolSize = Convert.ToInt32(SliderMinSymbolSize.Value);

            //Set the Barcode Orientation.
            _barcodeReader.BarcodeOrientation = (SymbolOrientation)ComboBoxOrientation.SelectedItem;

            //Set the Symbology Types.
            Symbology symbologyTypes = Symbology.All;

            if (ListBoxSymbologies.SelectedItems.Count > 0)
            {
                //The Symbology enum uses bit flags.
                symbologyTypes = ListBoxSymbologies.SelectedItems
                    //Cast all selected items to Symbology.
                .Cast<Symbology>()
                    //Perform a bitwise OR on the items.
                .Aggregate((symbology, nextSymbology) => symbology | nextSymbology);
            }

            //Decoding the image.
            _barcodeReader.Decode(_image, symbologyTypes);
        }

        private void BarcodeReader_DecodeComplete(object sender, ReaderDecodeArgs e)
        {
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

        private void ButtonLinear_Click(object sender, RoutedEventArgs e)
        {
            LoadSettings("Linear", _linearSymbologies, 5);
        }

        private void ButtonEanUpc_Click(object sender, RoutedEventArgs e)
        {
            LoadSettings("EanUpc", _eanUpcSymbologies, 5);
        }

        private void Button2D_Click(object sender, RoutedEventArgs e)
        {
            LoadSettings("2D", _2dSymbologies, 2);
        }

        private void LoadSettings(string imageName, List<Symbology> symbologies, double maxNumberOfSymbolsToRead)
        {
            //Set the selected symbologies depending on the predefined demo configuration.
            ListBoxSymbologies.SelectedItems.Clear();
            symbologies.ForEach(symbology => ListBoxSymbologies.SelectedItems.Add(symbology));

            //Set the Maximum Number of Symbols to Read according to the predefined image.
            SliderNumberOfSymbols.Value = maxNumberOfSymbolsToRead;

            //Load an image depending on the predefined demo configuration.
            string imageUri = string.Format("/IGBarcodeReader;component/Images/{0}.png", imageName);
            _image = new BitmapImage(new Uri(imageUri, UriKind.Relative));
            ImageContainer.Source = _image;
        }

        private void ListBoxSymbologies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Symbology symbology in e.AddedItems)
            {
                if (symbology.ToString() == "All")
                {
                    ListBoxSymbologies.SelectAll();
                    break;
                }
            }
        }
    }
}
