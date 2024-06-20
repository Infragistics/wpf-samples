using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Infragistics.Controls.Barcodes;

namespace IGBarcode.Samples.Performance
{
    public partial class BarcodeStretch : Infragistics.Samples.Framework.SampleContainer
    {
        private List<XamBarcode> Barcodes = new List<XamBarcode>()
                    {
                        new XamCode39Barcode()
                            {
                                Data = "CODE39"
                            },
                        new XamCode128Barcode()
                            {
                                Data = "Code 128"
                            },
                        new XamEanUpcBarcode()
                            {
                                CodeType = EanUpcCodeType.Ean13,
                                Data = "144178012098"
                            },
                        new XamGs1DataBarBarcode()
                            {
                                CodeType = GS1CodeType.StackedOmnidirectional,
                                Data = "01569063022"
                            },
                        new XamIntelligentMailBarcode()
                            {
                                Data = "1212312345612345678912345",
                                BarsFillMode = BarsFillMode.EnsureEqualSize
                            },
                        new XamRoyalMailBarcode()
                            {
                                Data = "541321DSGAS32D1A6",
                                BarsFillMode = BarsFillMode.EnsureEqualSize
                            },
                        new XamInterleaved2Of5Barcode()
                            {
                                Data = "12345"
                            },
                        new XamPdf417Barcode()
                            {
                                Data = "PDF 417"
                            },
                        new XamMaxiCodeBarcode()
                            {
                                Data = "MaxiCode"
                            },
                        new XamQRCodeBarcode()
                            {
                                Data = "QR Code"
                            }
                    };

        public BarcodeStretch()
        {
            InitializeComponent();

            ListBoxStretchSettings.SelectionChanged += new SelectionChangedEventHandler(ListBoxStretchSettings_SelectionChanged);
            ListBoxStretchSettings.SelectedIndex = 0;

            ComboBoxBarcodes.SelectionChanged += new SelectionChangedEventHandler(ComboBoxBarcodes_SelectionChanged);
            ComboBoxBarcodes.SelectedIndex = 0;
        }

        #region Events
        private void ListBoxStretchSettings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (XamBarcode barcode in Barcodes)
            {
                barcode.Stretch = (Stretch)ListBoxStretchSettings.SelectedItem;
            }
        }

        private void ComboBoxBarcodes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BarcodeContainer.Child = Barcodes[ComboBoxBarcodes.SelectedIndex];
            if (ComboBoxBarcodes.SelectedItem.ToString() == "XamIntelligentMailBarcode" ||
                ComboBoxBarcodes.SelectedItem.ToString() == "XamRoyalMailBarcode")
            {
                BarcodeContainer.Height = 100;
            }
            else
            {
                BarcodeContainer.Height = 185;
            }
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxBarcodes.SelectedIndex == 0)
            {
                ComboBoxBarcodes.SelectedIndex = ComboBoxBarcodes.Items.Count - 1;
            }
            else
            {
                ComboBoxBarcodes.SelectedIndex -= 1;
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxBarcodes.SelectedIndex = (ComboBoxBarcodes.SelectedIndex + 1) % ComboBoxBarcodes.Items.Count;
        }

        #endregion
    }
}