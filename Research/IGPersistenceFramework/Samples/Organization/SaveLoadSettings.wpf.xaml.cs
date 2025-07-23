using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Media.Imaging;
using Infragistics.Persistence;
using Infragistics.Samples.Framework;

namespace IGPersistenceFramework.Samples.Organization
{
    public partial class SaveLoadSettings : SampleContainer
    {
private const string fileName = "tempFile";
        PersistenceSettings settings = new PersistenceSettings();

        public SaveLoadSettings()
        {
            InitializeComponent();
            settings.Events.PersistenceSaved += new System.EventHandler<PersistenceSavedEventArgs>(Events_PersistenceSaved);
        }

        void Events_PersistenceSaved(object sender, PersistenceSavedEventArgs e)
        {
            string imageResourcePath = "/IGPersistenceFramework;component/Images/Save_Confirm.png";
            BitmapImage bmi = new BitmapImage(new Uri(imageResourcePath, UriKind.Relative));

            StateImage.Source = bmi;
        }

        private void BtnSaveState_Click(object sender, RoutedEventArgs e)
        {
            SaveIntoIsolatedStorage(xamWDWindow, fileName);
        }

        private void BtnLoadState_Click(object sender, RoutedEventArgs e)
        {
            LoadFromIsolatedStorage(xamWDWindow, fileName);
        }

        private void SaveIntoIsolatedStorage(DependencyObject obj, string fileName)
        {
            // Save DependencyObject properties into MemoryStream
            MemoryStream memoryStream = PersistenceManager.Save(obj, settings);

            // Obtains user-scoped isolated storage and creates a file with persistence settings
            using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                if (iso.FileExists(fileName))
                    iso.DeleteFile(fileName);

                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(fileName, FileMode.OpenOrCreate, iso))
                {
                    stream.Write(memoryStream.ToArray(), 0, (int)memoryStream.Length);
                }
            }
        }

        private void LoadFromIsolatedStorage(DependencyObject obj, string fileName)
        {
            using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                if (iso.FileExists(fileName))
                {
                    using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(fileName, FileMode.OpenOrCreate, iso))
                    {
                        // loads persistence settings from IsolatedStorageFileStream into DependencyObject
                        PersistenceManager.Load(obj, stream, settings);
                    }
                }
            }
        }
    }
}
