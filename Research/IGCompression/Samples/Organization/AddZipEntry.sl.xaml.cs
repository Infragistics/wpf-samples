using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using IGCompression.Resources;
using Infragistics.Compression;
using Infragistics.Samples.Framework;

namespace IGCompression.Samples.Organization
{

    public partial class AddZipEntry : SampleContainer
    {
        private ZipFile _zip;

        public AddZipEntry()
        {
            InitializeComponent();
        }


        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            _zip = new ZipFile();

            for (int i = 0; i < 10; i++)
            {
                string fileName = "myfile" + i + ".txt";
                string data = CompressionStrings.Zip_ZipEntries_SampleText + " " + i;
                ZipEntry entry = ZipEntry.CreateFile(fileName, "/", data);
                _zip.Entries.Add(entry);
            }

            txtBox.Text = "";

            foreach (ZipEntry entry in this._zip.Entries)
            {
                txtBox.Text += entry.FileName + "\n";
            }

        }

        private void BtnAddEntry_Click(object sender, RoutedEventArgs e)
        {
            txtBox.Text = string.Empty;
            string newFolderPath = TxtNewDirectory.Text;
            string newEntryName = TxtNewEntryName.Text;
            string newEntryContent = TxtNewEntryContent.Text;

            if (_zip != null)
            {
                if (!newFolderPath.Equals(string.Empty) && !newEntryName.Equals(string.Empty))
                {
                    ZipEntry newFile = ZipEntry.CreateFile(newEntryName, newFolderPath, newEntryContent);
                    try
                    {
                        _zip.Entries.Add(newFile);
                    }
                    catch (ArgumentException exc)
                    {
                        // There is already an entry with the same name.
                        ShowWarning(exc.Message);

                        // Removes the duplicated zip entry.
                        _zip.Entries.Remove(_zip.Entries.Last<ZipEntry>());
                    }

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
