using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using IGCompression.Resources;
using Infragistics.Compression;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;

namespace IGCompression.Samples.Data
{
    public partial class ExtractZipFile : SampleContainer
    {
        private ZipFile _zip;
        FileProvider provider;

        public ExtractZipFile()
        {
            InitializeComponent();
            provider = new FileProvider();
        }

        private void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            if (_zip != null) return;
            this.ZipEntriesTextBox.Text = CompressionStrings.Zip_KeyFeatures_Downloading;
            provider.GetFileAsync("zip/Pictures.zip");
            provider.GetFileCompleted += new EventHandler<GetFileCompletedEventArgs>(provider_GetFileCompleted);
        }

        void provider_GetFileCompleted(object sender, GetFileCompletedEventArgs e)
        {
            _zip = new ZipFile(e.Result);
            this.ZipEntriesListBox.Items.Clear();
            this.ZipEntriesListBox.ItemsSource = this._zip.Entries;
            this.ZipEntriesTextBox.Text = CompressionStrings.Zip_KeyFeatures_DownloadCompleted;
        }

        private void ZipEntriesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ZipEntry entry = this.ZipEntriesListBox.SelectedItem as ZipEntry;
            if (entry != null)
            {
                CrcCalculatorStream crcStream = entry.OpenReader();
                if (crcStream != null)
                {
                    WriteDataToStore(crcStream, entry.FileName);
                    crcStream.Close();
                }

                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    IsolatedStorageFileStream fileStream = store.OpenFile(entry.FileName, FileMode.Open);
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.SetSource(fileStream);
                    this.ImgPreview.Source = bitmap;
                    fileStream.Close();
                    this.ZipEntriesTextBox.Text = entry.FileName;
                }
            }
        }

        private void BtnExtractSelectedEntry_Click(object sender, RoutedEventArgs e)
        {
            if (this._zip != null)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                ZipEntry entry = this.ZipEntriesListBox.SelectedItem as ZipEntry;
                if (entry == null) return;

                int index = entry.FileName.LastIndexOf(".");
                string ext = entry.FileName.Substring(index + 1);
                dialog.Filter = CompressionStrings.Zip_SaveFileDialog_ImageFilter + " (*." + ext + ")|*." + ext;
                bool? dialogResult = dialog.ShowDialog();

                if (dialogResult == true && this.ZipEntriesListBox.SelectedItems.Count > 0)
                {
                    CrcCalculatorStream crcStream = entry.OpenReader();
                    if (crcStream != null)
                    {
                        using (Stream fs = (Stream)dialog.OpenFile())
                        {
                            byte[] buffer = new byte[65536];
                            int bytesCount = crcStream.Read(buffer, 0, buffer.Length);
                            while (bytesCount > 0)
                            {
                                fs.Write(buffer, 0, bytesCount);
                                bytesCount = crcStream.Read(buffer, 0, buffer.Length);
                            }
                            fs.Close();
                        }
                        crcStream.Close();
                    }
                }
            }
        }

        public static void WriteDataToStore(Stream dataStream, string fileName)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                String[] dirNames = store.GetDirectoryNames("*");
                String[] fileNames = store.GetFileNames("\\*");

                if (fileNames.Length > 0)
                {
                    for (int i = 0; i < fileNames.Length; ++i)
                    {
                        // Delete the files.
                        store.DeleteFile("\\" + fileNames[i]);
                    }
                    // Confirm that no files remain.
                    fileNames = store.GetFileNames("\\*");
                }

                if (dirNames.Length > 0)
                {
                    for (int i = 0; i < dirNames.Length; ++i)
                    {
                        // Delete the Archive directory.
                        store.DeleteDirectory(dirNames[i]);
                    }
                }

                IsolatedStorageFileStream fileStream = store.CreateFile(fileName);
                byte[] buffer = new byte[65536];
                int bytesCount = dataStream.Read(buffer, 0, buffer.Length);
                while (bytesCount > 0)
                {
                    fileStream.Write(buffer, 0, bytesCount);
                    bytesCount = dataStream.Read(buffer, 0, buffer.Length);
                }
                fileStream.Close();
            }
        }

    }
}
