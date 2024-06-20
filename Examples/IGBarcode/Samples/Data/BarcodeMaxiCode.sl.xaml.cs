using Infragistics.Controls.Barcodes;

namespace IGBarcode.Samples.Data
{
    public partial class BarcodeMaxiCode : Infragistics.Samples.Framework.SampleContainer
    {
        public BarcodeMaxiCode()
        {
            InitializeComponent();
            ComboBoxMode.SelectedIndex = 0;
        }
        private void ComboBoxMode_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ModeIndicator mode = (ModeIndicator)ComboBoxMode.SelectedItem;

            if (mode == ModeIndicator.Mode2 || mode == ModeIndicator.Mode3)
            {
                ComboBoxPostalCode.IsEnabled = true;
                ComboBoxCountryCode.IsEnabled = true;
            }
            else
            {
                ComboBoxPostalCode.IsEnabled = false;
                ComboBoxCountryCode.IsEnabled = false;
            }
        }
    }
}
