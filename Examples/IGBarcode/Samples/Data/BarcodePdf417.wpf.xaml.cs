using System.Windows;
using System.Windows.Controls;
using Infragistics.Controls.Barcodes;

namespace IGBarcode.Samples.Data
{
    public partial class BarcodePdf417 : Infragistics.Samples.Framework.SampleContainer
    {
        public BarcodePdf417()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }
        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            ComboBoxErrorCorrection.SelectedIndex = 0;
        }

        private void ComboBoxErrorCorrection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Pdf417ErrorCorrectionLevel currentLevel = 0;
            if (ComboBoxErrorCorrection != null)
                currentLevel = (Pdf417ErrorCorrectionLevel)ComboBoxErrorCorrection.SelectedItem;

            if (Barcode != null)
            {
                if (Barcode.UseMinimumErrorCorrectionLevel)
                {
                    Pdf417ErrorCorrectionLevel minimumLevel = Barcode.ErrorCorrectionLevel;

                    if (minimumLevel < currentLevel)
                    {
                        Barcode.ErrorCorrectionLevel = currentLevel;
                    }
                    else
                    {
                        ComboBoxErrorCorrection.SelectedItem = minimumLevel;
                    }
                }
                else
                {
                    Barcode.ErrorCorrectionLevel = currentLevel;
                }
            }
        }

        private void CheckBoxMinimumErrorCorrection_Checked(object sender, RoutedEventArgs e)
        {
            Barcode.UseMinimumErrorCorrectionLevel = true;
            ComboBoxErrorCorrection.SelectedItem = Barcode.ErrorCorrectionLevel;
        }

        private void CheckBoxMinimumErrorCorrection_Unchecked(object sender, RoutedEventArgs e)
        {
            Barcode.UseMinimumErrorCorrectionLevel = false;
        }
    }
}
