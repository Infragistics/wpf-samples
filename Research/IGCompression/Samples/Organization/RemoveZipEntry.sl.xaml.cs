using System.IO;
using System.Windows;
using System.Windows.Controls;
using IGCompression.Resources;
using Infragistics.Compression;
using Infragistics.Samples.Framework;

namespace IGCompression.Samples.Organization
{
    public partial class RemoveZipEntry : SampleContainer
    {
        private ZipFile _zip;

        public RemoveZipEntry()
        {
            InitializeComponent();
        }


        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            _zip = new ZipFile();

            for (int i = 0; i < 10; i++)
            {
                string fileName = "myfile" + i + ".txt";
                string folderName = "myfolder" + i;
                string data = "Sample encoded simple and short text " + i;
                ZipEntry entry = ZipEntry.CreateFile(fileName, "/", data);
                _zip.Entries.Add(entry);
                entry = ZipEntry.CreateDirectory(folderName, "myroot");
                _zip.Entries.Add(entry);
            }

            ZipEntriesListBox.ItemsSource = _zip.Entries;

            txtBox.Text = "";

            txtBox.Text = CompressionStrings.Zip_ZipEntries_EntriesCreated;

        }

        private void BtnRemoveEntry_Click(object sender, RoutedEventArgs e)
        {
            if (_zip != null)
            {
                for (int i = _zip.Entries.Count - 1; i >= 0; i--)
                {
                    var entry = _zip.Entries[i];
                    if (ZipEntriesListBox.SelectedItems.Contains(entry))
                    {
                        _zip.Entries.Remove(entry);
                    }
                }

                txtBox.Text = string.Empty;

                ZipEntriesListBox.ItemsSource = _zip.Entries;

            }
            else
            {
                ShowWarning(CompressionStrings.Zip_KeyFeatures_Warning);
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
