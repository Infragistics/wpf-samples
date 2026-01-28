using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using IGCompression.Resources;
using Infragistics.Compression;
using Infragistics.Samples.Framework;

namespace IGCompression.Samples.Data
{
    public partial class ZipFileCompressionLevel : SampleContainer
    {
        private ZipFile _zip = null;
        private bool _loaded;
        private string _tempString = "{0} " + CompressionStrings.Zip_KeyFeatures_Ratio + " {1:0.00}";

        public ZipFileCompressionLevel()
        {
            InitializeComponent();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            MemoryStream memoryStream = new MemoryStream();

            _zip = new ZipFile();

            // Set a compression level property
            _zip.CompressionLevel = (CompressionLevel)(((KeyValuePair<Enum, string>)combobox_CompressionLevels.SelectedItem).Key);

            // Create sample files with string content to archive.
            for (int i = 0; i < 10; i++)
            {
                string fileName = "file" + i + ".txt";
                string stringContent = " sample content that we need ";
                for (int k = 0; k <= 10; k++)
                {
                    stringContent += stringContent;
                }

                // Creates a ZipEntry that represens a file with parameters: file name, directory path in the archive and content.
                ZipEntry newFile = ZipEntry.CreateFile(fileName, "/", stringContent);

                // Add created zip entries to the zip archive.
                _zip.Entries.Add(newFile);
            }

            // Saves the ZipFile archive to a MemoryStream object.
            _zip.Save(memoryStream);

            // Print log messages
            PrintLog(CompressionStrings.Zip_KeyFeatures_FileCreated);
            PrintLog(CompressionStrings.Zip_KeyFeatures_Level + " = " + (((KeyValuePair<Enum, string>)combobox_CompressionLevels.SelectedItem).Key));
            foreach (ZipEntry entry in _zip.Entries)
            {
                PrintLog(String.Format(_tempString, entry.FileName, entry.CompressionRatio));
            }
        }

        private void BtnAddEntry_Click(object sender, RoutedEventArgs e)
        {
            MemoryStream memoryStream = new MemoryStream();

            string newFolderPath = TxtNewDirectory.Text;
            string newEntryName = TxtNewEntryName.Text;
            string newEntryContent = TxtNewEntryContent.Text;

            if (!newFolderPath.Equals(string.Empty) && !newEntryName.Equals(string.Empty))
            {
                if (_zip != null)
                {
                    try
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            newEntryContent += (" " + newEntryContent + "\n");
                        }

                        ZipEntry newFile = ZipEntry.CreateFile(newEntryName, newFolderPath, newEntryContent);

                        // Tries to add a new ZipEntry
                        // The name of the entry has to be unique.
                        // If not, an ArgumentException is thrown.
                        _zip.Entries.Add(newFile);

                        // Saves the archive in a MemoryStream object.
                        _zip.Save(memoryStream);

                        // Prints information
                        ZipEntry lastZipEntry = _zip.Entries.Last<ZipEntry>();
                        if (lastZipEntry != null)
                        {
                            PrintLog(String.Format(_tempString, lastZipEntry.FileName, lastZipEntry.CompressionRatio));
                        }
                    }
                    catch (ArgumentException exc)
                    {
                        // There is already an entry with the same name.
                        ShowWarning(exc.Message);

                        // Removes the duplicated zip entry.
                        _zip.Entries.Remove(_zip.Entries.Last<ZipEntry>());
                    }
                }
                else
                {
                    ShowWarning(CompressionStrings.Zip_KeyFeatures_Warning);
                }
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            MemoryStream memoryStream = new MemoryStream();

            if (_zip != null)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = CompressionStrings.Zip_SaveFileDialog_ZipFilter;
                bool? dialogResult = dialog.ShowDialog();

                if (dialogResult == true)
                {
                    _zip.Save(memoryStream);

                    byte[] fileBytes = memoryStream.GetBuffer();
                    using (Stream fs = (Stream)dialog.OpenFile())
                    {
                        fs.Write(fileBytes, 0, fileBytes.Length);
                        fs.Close();

                        PrintLog(CompressionStrings.Zip_KeyFeatures_FileSaved);
                    }
                }
            }
            else
            {
                ShowWarning(CompressionStrings.Zip_KeyFeatures_Warning);
            }

            _zip = null;
        }

        private void BtnClearLog_Click(object sender, RoutedEventArgs e)
        {
            txtBox.Text = String.Empty;
        }

        private void combobox_CompressionLevels_LayoutUpdated(object sender, EventArgs e)
        {
            if (combobox_CompressionLevels != null && !_loaded)
            {
                combobox_CompressionLevels.ItemsSource = GetEnumValues(CompressionLevel.Level1);
                combobox_CompressionLevels.DisplayMemberPath = "Key";

                // The selected compression level will be the default compression level (Level 6).
                // This level offers a good balance of speed and compression efficiency.
                combobox_CompressionLevels.SelectedIndex = 5;

                _loaded = true;
            }
        }

        #region GetEnumValues
        private Dictionary<Enum, string> GetEnumValues(Enum enumeration)
        {
            Dictionary<Enum, string> dict = new Dictionary<Enum, string>();
            IEnumerable<Enum> fields = from f in enumeration.GetType().GetFields(BindingFlags.Static | BindingFlags.Public)
                                       select (Enum)f.GetValue(enumeration);
            foreach (Enum eNum in fields)
            {
                if (!dict.ContainsKey(eNum))
                {
                    System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CurrentCulture;
                    string value = Enum.GetName(typeof(CompressionLevel), eNum);
                    if (!eNum.Equals(CompressionLevel.None))
                        dict.Add(eNum, CompressionStrings.ResourceManager.GetString(value, culture));
                }
            }

            return dict;
        }
        #endregion GetEnumValues

        #region PrintLog
        private void PrintLog(string msg)
        {
            string logMsg = msg + "\n";

            txtBox.Text += logMsg;
        }
        #endregion PrintLog

        #region ShowWarning
        private void ShowWarning(string msg)
        {
            MessageBox.Show(msg);
        }
        #endregion ShowWarning
    }
}
