using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using IGCompression.Resources;
using Infragistics.Compression;
using Infragistics.Samples.Framework;

namespace IGCompression.Samples.Data
{
    public partial class DeflateStream : SampleContainer
    {
        public DeflateStream()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;

            cboStrategy.ItemsSource = typeof(CompressionStrategy).GetFields(BindingFlags.Public | BindingFlags.Static).Select(t => t.Name);
            cboStrategy.SelectedIndex = 0;
            cboLevel.ItemsSource = typeof(CompressionLevel).GetFields(BindingFlags.Public | BindingFlags.Static).Select(t => t.Name);
            cboLevel.SelectedIndex = 8;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            txtSource.Text = CompressionStrings.Deflate_Data;
        }

        private void btnCompress_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtSource.Text))
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(txtSource.Text);
                using (var stream = new MemoryStream())
                {
                    using (var ds = new Infragistics.Compression.DeflateStream(stream, Infragistics.Compression.CompressionMode.Compress, (CompressionLevel)Enum.Parse(typeof(CompressionLevel), (string)cboLevel.SelectedItem, true)))
                    {
                        ds.Strategy = (CompressionStrategy)Enum.Parse(typeof(CompressionStrategy), (string)cboStrategy.SelectedItem, true);
                        ds.Write(byteArray, 0, byteArray.Length);
                    }
                    txtResult.Text = Convert.ToBase64String(stream.ToArray());
                }
                lblRatio.Text = CompressionStrings.Deflate_PercentCompressed + " " + ((double)(txtSource.Text.Length - txtResult.Text.Length) / (double)txtSource.Text.Length).ToString("P2");
            }
        }
    }
}
