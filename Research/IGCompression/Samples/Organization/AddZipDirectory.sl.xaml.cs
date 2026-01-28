using System.IO;
using System.Windows;
using System.Windows.Controls;
using IGCompression.Resources;
using Infragistics.Compression;
using Infragistics.Samples.Framework;

namespace IGCompression.Samples.Organization
{
    public partial class AddZipDirectory : SampleContainer
    {
        private ZipFile _zip;

        public AddZipDirectory()
        {
            InitializeComponent();
        }

        private void BtnAddDirectory_Click(object sender, RoutedEventArgs e)
        {
            if (_zip != null)
            {
                txtBox.Text = string.Empty;
                string newFolderPath = TxtNewDirectory.Text;
                if (!newFolderPath.Equals(string.Empty))
                {
                    ZipEntry zipEntry = ZipEntry.CreateDirectory(TxtNewDirectory.Text.Trim());
                    _zip.Entries.Add(zipEntry);
                    foreach (ZipEntry entry in _zip.Entries)
                    {
                        txtBox.Text += entry.FileName + "\n";
                    }
                }
            }
            else
            {
                ShowWarning(CompressionStrings.Zip_KeyFeatures_Warning);
            }
        }


        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            _zip = new ZipFile();

            for (int i = 0; i < 10; i++)
            {
                string fileName = "myfile" + i + ".txt";
                string data = "Sample encoded simple and short text " + i;
                ZipEntry entry = ZipEntry.CreateFile(fileName, "/", data);
                _zip.Entries.Add(entry);
            }

            txtBox.Text = "";

            foreach (ZipEntry entry in _zip.Entries)
            {
                txtBox.Text += entry.FileName + "\n";
            }

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
                    MemoryStream stream = new MemoryStream();
                    _zip.Save(stream);
                    byte[] fileBytes = stream.GetBuffer();

                    using (Stream fs = (Stream)dialog.OpenFile())
                    {
                        fs.Write(fileBytes, 0, fileBytes.Length);
                        txtBox.Text = CompressionStrings.Zip_KeyFeatures_FileSaved;
                    }
                }
            }
            else
            {
                ShowWarning(CompressionStrings.Zip_KeyFeatures_Warning);
            }
        }

        #region ShowWarning
        private void ShowWarning(string msg)
        {
            MessageBox.Show(msg);
        }
        #endregion ShowWarning
    }
}
