using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Infragistics.Samples.Framework;
using Microsoft.Win32;

namespace IGDataChart.Samples.Organization
{
    public partial class ExportingDataChartToImage : SampleContainer
    {
        public ExportingDataChartToImage()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            RenderTargetBitmap bitmap = new RenderTargetBitmap((int)this.dataChartGrid.ActualWidth, (int)this.dataChartGrid.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(this.dataChartGrid);

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = ((ComboBoxItem)this.formatCombo.SelectedItem).Content.ToString();
            if (saveDialog.ShowDialog() == true)
            {
                using (Stream stream = saveDialog.OpenFile())
                {
                    if (this.formatCombo.SelectedIndex == 0) // Exporting to JPEG
                    {
                        JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                        encoder.QualityLevel = (int)this.qualitySlider.Value;
                        encoder.Frames.Add(BitmapFrame.Create(bitmap));

                        encoder.Save(stream);
                    }
                    else // Exporting to PNG
                    {
                        PngBitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(bitmap));
                        encoder.Save(stream);
                    }
                }
            }
        }
    }
}
