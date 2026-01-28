using System;
using System.Collections.Generic;
using System.Windows;
using IGBarcode.Resources;
using Infragistics;
using Infragistics.Controls.Barcodes;

namespace IGBarcode.Samples.Performance
{
    public partial class BarcodeEvents : Infragistics.Samples.Framework.SampleContainer
    {
        public BarcodeEvents()
        {
            InitializeComponent();

            ListBoxData.ItemsSource = new Dictionary<string, string>()
                                      {
                                          {"Adjustable Race", "BA8327"},
                                          {"Bearing Ball", "AR5381"},
                                          {"Blade", "BL2036"},
                                          {BarcodeStrings.XWB_InvalidData, "bl2036"}
                                      };
            ListBoxData.SelectedIndex = 0;
        }

        private void Barcode_ErrorMessageDisplaying(object sender, ErrorMessageDisplayingEventArgs e)
        {
            UpdateOutput(e.ErrorMessage);

            TextBlockDataMessage.Visibility = Visibility.Visible;
        }

        private void Barcode_DataChanged(object sender, DataChangedEventArgs e)
        {
            UpdateOutput("NewData: " + e.NewData);

            TextBlockDataMessage.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClearOutput();
        }

        #region AuxiliaryMethods

        private void UpdateOutput(string text)
        {
            TextBlockOutput.Text += text + Environment.NewLine;
            ScrollViewerOutput.UpdateLayout();
            ScrollViewerOutput.ScrollToVerticalOffset(Double.MaxValue);
        }

        private void ClearOutput()
        {
            TextBlockOutput.Text = string.Empty;
        }

        #endregion
    }
}