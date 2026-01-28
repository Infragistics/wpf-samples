using System.Collections.Generic;

namespace IGBarcode.Samples.Data
{
    public partial class BarcodeCode39 : Infragistics.Samples.Framework.SampleContainer
    {
        private List<string> _data = new List<string>() { "XAMCODE39BARCODE", "CODE 39", "012345678", "458963741", "BA8327", "CS2812", "BEARING BALL", "LOREM IPSUM", "-.$/+%" };
        private List<string> _extendedData = new List<string>() { "Code 39", "012345678", "458963741", "ba8327", "cs2812", "Bearing", "-.$/+%", "()_?^*@" };

        public BarcodeCode39()
        {
            InitializeComponent();
            CheckBoxChecksum.IsChecked = true;
            CheckBoxExtended.IsChecked = true;
        }

        private void CheckBoxExtended_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            ListBoxData.ItemsSource = _extendedData;
            ListBoxData.SelectedIndex = 0;
        }

        private void CheckBoxExtended_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            ListBoxData.ItemsSource = _data;
            ListBoxData.SelectedIndex = 0;
        }
    }
}
