using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using IGCompression.Resources;
using Infragistics.Compression;
using Infragistics.Samples.Framework;

namespace IGCompression.Samples.Data
{
    public partial class CreateNewArchive : SampleContainer
    {
        ZipFile MyZipFile;
        private byte[][] _arrays = null;

        public CreateNewArchive()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Create the Save File Dialog
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = "zip";
            save.Filter = CompressionStrings.Zip_SaveFileDialog_ZipFilter;


            // Display the save dialog
            bool? dialogResult = save.ShowDialog();
            if (dialogResult == true)
            {
                using (Stream fs = (Stream)save.OpenFile())
                {
                    // Save the file using the Save method of the ZipFile object
                    MyZipFile.Save(fs);
                }
                this.doneTextBlock.Text = CompressionStrings.Zip_KeyFeatures_DownloadCompleted;
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            this.doneTextBlock.Text = String.Empty;

            // Create a Zipfile object to store the file to be archived
            MyZipFile = new ZipFile();

            // Create an OpenFileDialog object to allow your end users to select a file
            OpenFileDialog open = new OpenFileDialog { Multiselect = true, Filter = CompressionStrings.Zip_OpenFileDialog_AllFilesFilter };

            // Open the dialog
            bool? dialogResult = open.ShowDialog();
            int k = 0;
            if (dialogResult == true)
            {

                foreach (FileInfo file in open.Files)
                {
                    // Add the file to the ZipFile object
                    Stream stream = file.OpenRead();
                    _arrays = new byte[open.Files.Count()][];
                    _arrays[k] = new byte[stream.Length];
                    int numBytes = stream.Read(_arrays[k], 0, (int)stream.Length); ;
                    ZipEntry entry = ZipEntry.CreateFile(file.Name, System.IO.Path.GetDirectoryName(file.Name), _arrays[k]);
                    MyZipFile.Entries.Add(entry);
                    k++;
                }
            }
        }
    }
}
