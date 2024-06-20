using System.IO;
using System.Windows;
using System.Windows.Controls;
using IGCompression.Resources;
using Infragistics.Compression;
using Infragistics.Samples.Framework;

namespace IGCompression.Samples.Organization
{
    public partial class ZipEntryPassword : SampleContainer
    {
        private ZipFile _zip;
        private FileInfo _file = null;
        private byte[] _array = null;

        public ZipEntryPassword()
        {
            InitializeComponent();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            // Create an OpenFileDialog object to allow your end users to select a file
            OpenFileDialog openDialog = new OpenFileDialog { Multiselect = false, Filter = CompressionStrings.Zip_OpenFileDialog_AllFilesFilter };

            // Open the dialog
            bool? dialogResult = openDialog.ShowDialog();
            if (dialogResult == true)
            {
                // Add the file to the ZipFile object
                Stream stream = openDialog.File.OpenRead();
                _file = openDialog.File;
                _array = new byte[stream.Length];
                int numBytes = stream.Read(_array, 0, (int)stream.Length);
            }
        }

        private void BtnAddEntry_Click(object sender, RoutedEventArgs e)
        {
            txtBox.Text = string.Empty;
            string password = TxtNew_EntryPassword.Text;

            if ((_array != null) && (_file != null))
            {
                _zip = new ZipFile();
                ZipEntry newFile = ZipEntry.CreateFile(_file.Name, "/", _array);
                _zip.Password = password.Trim();
                _zip.Entries.Add(newFile);

                foreach (ZipEntry entry in _zip.Entries)
                {
                    txtBox.Text += entry.FileName + "\n";
                }
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
        }
    }
}
