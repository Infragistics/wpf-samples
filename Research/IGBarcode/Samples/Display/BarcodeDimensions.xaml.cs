using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Infragistics.Controls.Barcodes;

namespace IGBarcode.Samples.Performance
{
    public partial class BarcodeDimensions : Infragistics.Samples.Framework.SampleContainer
    {
        private List<double> WidthToHeightRatios;

        private List<XamGridBarcode> Barcodes = new List<XamGridBarcode>()
                    {
                        new XamCode39Barcode()
                            {
                                Data = "CODE39",
                                Stretch = Stretch.None
                            },
                        new XamCode128Barcode()
                            {
                                Data = "Code 128",
                                Stretch = Stretch.None
                            },
                        new XamEanUpcBarcode()
                            {
                                CodeType = EanUpcCodeType.Ean13,
                                Data = "144178012098",
                                Stretch = Stretch.None
                            },
                        new XamGs1DataBarBarcode()
                            {
                                CodeType = GS1CodeType.StackedOmnidirectional,
                                Data = "01569063022",
                                Stretch = Stretch.None
                            },
                        new XamIntelligentMailBarcode()
                            {
                                Data = "1212312345612345678912345",
                                Stretch = Stretch.None
                            },
                        new XamRoyalMailBarcode()
                            {
                                Data = "541321DSGAS32D1A6",
                                Stretch = Stretch.None
                            },
                        new XamInterleaved2Of5Barcode()
                            {
                                Data = "12345",
                                Stretch = Stretch.None
                            },
                        new XamPdf417Barcode()
                            {
                                Data = "PDF 417",
                                Stretch = Stretch.None
                            },
                        new XamQRCodeBarcode()
                            {
                                Data = "QR Code",
                                Stretch = Stretch.None
                            }
                    };

        public BarcodeDimensions()
        {
            InitializeComponent();

            //Get the default WidthToHeight ratios
            WidthToHeightRatios = new List<double>();
            foreach (XamGridBarcode barcode in Barcodes)
            {
                WidthToHeightRatios.Add(barcode.WidthToHeightRatio);
            }

            cbBarcodes.SelectionChanged += new SelectionChangedEventHandler(cbBarcodes_SelectionChanged);
            cbBarcodes.SelectedIndex = 0;

            SliderWidthToHeightRatio.ValueChanged +=
                new RoutedPropertyChangedEventHandler<double>(SliderWidthToHeightRatio_ValueChanged);
            SliderXDimension.ValueChanged += new RoutedPropertyChangedEventHandler<double>(SliderXDimension_ValueChanged);

            rbFillSpace.IsChecked = true;
        }

        private void SliderWidthToHeightRatio_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            (BarcodeContainer.Child as XamGridBarcode).WidthToHeightRatio = SliderWidthToHeightRatio.Value;
        }

        private void SliderXDimension_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            (BarcodeContainer.Child as XamGridBarcode).XDimension = SliderXDimension.Value;
        }

        private void rbFillSpace_Checked(object sender, RoutedEventArgs e)
        {
            SetBarsFillMode(BarsFillMode.FillSpace);
        }

        private void rbEnsureEqualSize_Checked(object sender, RoutedEventArgs e)
        {
            SetBarsFillMode(BarsFillMode.EnsureEqualSize);
        }

        #region Buttons Next and Previous

        private void cbBarcodes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BarcodeContainer.Child = Barcodes[cbBarcodes.SelectedIndex];

            if (BarcodeContainer.Child is XamQRCodeBarcode)
            {
                //The WidthToHeightRation of the XamQRCodeBarcode is always 1.
                SliderWidthToHeightRatio.Minimum = 1;
                SliderWidthToHeightRatio.Maximum = 1;
                SliderWidthToHeightRatio.Value = 1;
                SliderWidthToHeightRatio.IsEnabled = false;

                return;
            }

            (BarcodeContainer.Child as XamGridBarcode).XDimension = SliderXDimension.Value;

            //Reset the WidthToHeightRatio slider to the default value for each barcode
            double value = WidthToHeightRatios[cbBarcodes.SelectedIndex];
            SliderWidthToHeightRatio.Minimum = value - 5 > 0 ? value - 5 : 0.93;
            SliderWidthToHeightRatio.Maximum = value + 5;
            SliderWidthToHeightRatio.Value = value;
            SliderWidthToHeightRatio.IsEnabled = true;
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (cbBarcodes.SelectedIndex == 0)
            {
                cbBarcodes.SelectedIndex = cbBarcodes.Items.Count - 1;
            }
            else
            {
                cbBarcodes.SelectedIndex -= 1;
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            cbBarcodes.SelectedIndex = (cbBarcodes.SelectedIndex + 1) % cbBarcodes.Items.Count;
        }

        #endregion

        private void SetBarsFillMode(BarsFillMode fillMode)
        {
            for (int i = 0; i < Barcodes.Count; i++)
            {
                Barcodes[i].BarsFillMode = fillMode;
            }
        }
    }
}