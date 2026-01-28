using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Infragistics.Imaging;
using Infragistics.Samples.Framework;

namespace IGDataChart.Samples.Organization
{
    public partial class ExportingDataChartToImage : SampleContainer
    {
        private IImageEncoder encoder;

        public ExportingDataChartToImage()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            WriteableBitmap bitmap = new WriteableBitmap(this.dataChartGrid, null);

            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.DefaultExt = ((ComboBoxItem)this.formatCombo.SelectedItem).Content.ToString();
            if (savedialog.ShowDialog() == true)
            {
                using (Stream stream = savedialog.OpenFile())
                {
                    if (this.formatCombo.SelectedIndex == 0) // Exporting to JPEG
                    {
                        encoder = new JpegImageEncoder();
                        List<EncodingProperty> qualityList = new List<EncodingProperty>();
                        qualityList.Add(new JpegQualityProperty() { Quality = (int)this.qualitySlider.Value });

                        encoder.Encode(bitmap, qualityList, stream);
                    }
                    else // Exporting to PNG
                    {
                        encoder = new PngImageEncoder();
                        encoder.Encode(bitmap, new List<EncodingProperty>(0), stream);
                    }
                }
            }
        }
    }
}
