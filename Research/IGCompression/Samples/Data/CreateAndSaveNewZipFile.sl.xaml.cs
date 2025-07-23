using System.IO;
using System.Windows;
using System.Windows.Controls;
using IGCompression.Resources;
using Infragistics.Compression;
using Infragistics.Samples.Framework;

namespace IGCompression.Samples.Data
{
    public partial class CreateAndSaveNewZipFile : SampleContainer
    {
        private ZipFile _zip;

        public CreateAndSaveNewZipFile()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_zip != null)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = CompressionStrings.Zip_SaveFileDialog_ZipFilter;
                bool? dialogResult = dialog.ShowDialog();

                if (dialogResult == true)
                {
                    using (Stream fs = (Stream)dialog.OpenFile())
                    {
                        // Saves the zip archive to a file
                        _zip.Save(fs);
                        PrintLog(CompressionStrings.Zip_KeyFeatures_FileSaved);
                    }
                }
            }
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            _zip = new ZipFile();

            for (int i = 1; i < 11; i++)
            {
                // Creates some sample data
                string fileName = "myfile" + i + ".txt";
                string data = CompressionStrings.Zip_ZipEntries_SampleText + " " + i;

                // Creates a ZipEntry. 
                ZipEntry entry = ZipEntry.CreateFile(fileName, "/", data);

                // Adds the ZipEntry to the zip archive.
                _zip.Entries.Add(entry);
            }

            txtBox.Text = string.Empty;

            foreach (ZipEntry entry in _zip.Entries)
            {
                PrintLog(entry.FileName);
            }
        }

        #region PrintLog
        private void PrintLog(string msg)
        {
            string logMsg = msg + "\n";

            txtBox.Text += logMsg;
        }
        #endregion PrintLog
    }
}
