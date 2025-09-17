using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using IGCompression.Resources;
using Infragistics;
using Infragistics.Compression;
using Infragistics.Samples.Framework;

namespace IGCompression.Samples.Data
{
    public partial class ReadZipFile : SampleContainer
    {
        private ZipFile _zip;

        public ReadZipFile()
        {
            InitializeComponent();
        }

        // Executes when upload button is clicked.
        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            Stream stream;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = CompressionStrings.Zip_OpenFileDialog_ZipFilter;

            bool iszipSelected = dialog.ShowDialog() ?? false;

            if (iszipSelected == true)
            {
                stream = dialog.File.OpenRead();

                CultureInfo jpCulture = new CultureInfo("ja-JP");

                CultureInfo current = CultureInfo.CurrentCulture;

                Encoding defaultEncoding = System.Text.Encoding.UTF8;

                if (current.Equals(jpCulture))
                {
                    defaultEncoding = new Windows932Encoding();
                }

                // Creates new ZipFile instance 
                _zip = new ZipFile(stream, defaultEncoding);

                // Prints information about the ZipEntry
                foreach (ZipEntry entry in _zip.Entries)
                {
                    PrintLog(entry.FileName);
                }
            }
        }

        private void BtnClearLog_Click(object sender, RoutedEventArgs e)
        {
            txtBox.Text = String.Empty;
        }

        private void PrintLog(string msg)
        {
            string logMsg = msg + "\n";

            txtBox.Text += logMsg;
        }
    }
}
