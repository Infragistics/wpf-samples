using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using IGCompression.Resources;
using Infragistics.Compression;
using Infragistics.Samples.Framework;

namespace IGCompression.Samples.Data
{
    public partial class ZipFilePassword : SampleContainer
    {
        private ZipFile _zip;
        private string[] _fileName = null;

        public ZipFilePassword()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string password = TxtNew_EntryPassword.Text.Trim();

            if (_fileName != null)
            {
                _zip = new ZipFile { Password = password };

                for (int i = 0; i < 10; i++)
                {
                    byte[] array = ReadDataFromStore(_fileName[i]);

                    // Creates zip entries to fill the zip archive with.
                    if (array != null)
                    {
                        ZipEntry newEntry = ZipEntry.CreateFile(_fileName[i], "/", array);
                        _zip.Entries.Add(newEntry);
                    }
                }

                foreach (ZipEntry entry in _zip.Entries)
                {
                    PrintLog(entry.FileName);
                }

                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = CompressionStrings.Zip_OpenFileDialog_ZipFilter;
                bool? dialogResult = dialog.ShowDialog();

                if (dialogResult == true)
                {
                    // Stores the ZipFile into MemoryStream.
                    MemoryStream stream = new MemoryStream();
                    _zip.Save(stream);

                    byte[] fileBytes = stream.GetBuffer();

                    using (Stream fs = (Stream)dialog.OpenFile())
                    {
                        fs.Write(fileBytes, 0, fileBytes.Length);
                        fs.Close();

                        PrintLog(CompressionStrings.Zip_KeyFeatures_FileSaved);
                    }
                }
            }
        }

        private void GenerateItems_Click(object sender, RoutedEventArgs e)
        {
            txtBox.Text = String.Empty;

            _fileName = new string[10];

            for (int i = 0; i < 10; i++)
            {
                _fileName[i] = "myfile" + i + ".txt";
                string data = "Sample encoded simple and short text " + i;
                byte[] content = System.Text.Encoding.UTF8.GetBytes(data);

                WriteDataToStore(content, _fileName[i]);

                PrintLog(_fileName[i] + " " + CompressionStrings.Zip_KeyFeatures_SuccessfullyCreated);
            }

            PrintLog(CompressionStrings.Zip_ZipEntries_EntriesCreated);
        }


        #region ReadDataFromStore
        private static byte[] ReadDataFromStore(string fileName)
        {
            using (var store = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!store.FileExists(fileName))
                {
                    return null;
                }
                System.IO.IsolatedStorage.IsolatedStorageFileStream fileStream;
                fileStream = store.OpenFile(fileName, FileMode.Open);
                byte[] b = new byte[fileStream.Length];
                fileStream.Read(b, 0, (int)fileStream.Length);
                fileStream.Close();
                return b;
            }
        }
        #endregion ReadDataFromStore

        #region WriteDataToStore
        private static void WriteDataToStore(byte[] data, string fileName)
        {
            if (data == null || data.Length == 0)
                return;
            using (var store = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (store.FileExists(fileName))
                {
                    store.DeleteFile(fileName);
                }
                System.IO.IsolatedStorage.IsolatedStorageFileStream fileStream = store.CreateFile(fileName);
                fileStream.Write(data, 0, data.Length);
                fileStream.Close();
            }
        }
        #endregion WriteDataToStore

        #region PrintLog
        private void PrintLog(string msg)
        {
            string logMsg = msg + "\n";

            txtBox.Text += logMsg;
        }

        #endregion PrintLog
    }
}
