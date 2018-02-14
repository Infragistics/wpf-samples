using IgWord.Business;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace IgWord.Services
{


    public class RecentFilesService : IRecentFilesService, INotifyPropertyChanged
    {
        private const int historyLimit = 25;
        private PropertyInfo recentFilesRepositoryProperty;
        private PropertyInfo recentFoldersRepositoryProperty;

        private ApplicationSettingsBase settings;

        private ObservableCollection<SystemInfoCustom> recentFiles;
        public ObservableCollection<SystemInfoCustom> RecentFiles
        {
            get { return recentFiles; }
            private set { recentFiles = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<SystemInfoCustom> recentFolders;
        public ObservableCollection<SystemInfoCustom> RecentFolders
        {
            get { return recentFolders; }
            private set { recentFolders = value; NotifyPropertyChanged(); }
        }

        public RecentFilesService()
        {
            RecentFolders = new ObservableCollection<SystemInfoCustom>();
            RecentFiles = new ObservableCollection<SystemInfoCustom>();
        }

        public void SetRepositories(ApplicationSettingsBase settings,
                                    Expression<Func<ApplicationSettingsBase, string>> recentFilesRepositoryExpresion,
                                    Expression<Func<ApplicationSettingsBase, string>> recentFoldersRepositoryExpresion)
        {
            this.settings = settings;


            recentFilesRepositoryProperty = (PropertyInfo)((MemberExpression)recentFilesRepositoryExpresion.Body).Member;
            var filesSerialized = (string)recentFilesRepositoryProperty.GetValue(settings, null);

            bool saveChanges = false;
            if (string.IsNullOrEmpty(filesSerialized))
            {
                this.recentFiles = new ObservableCollection<SystemInfoCustom>();
                saveChanges = true;

            }
            else
            {
                this.recentFiles = DeserializeObject<ObservableCollection<SystemInfoCustom>>(filesSerialized);
            }


            recentFoldersRepositoryProperty = (PropertyInfo)((MemberExpression)recentFoldersRepositoryExpresion.Body).Member;
            var foldersSerizlized = (string)recentFoldersRepositoryProperty.GetValue(settings, null);


            if (string.IsNullOrEmpty(foldersSerizlized))
            {
                this.recentFolders = new ObservableCollection<SystemInfoCustom>();
                saveChanges = true;
            }
            else
            {
                this.recentFolders = DeserializeObject<ObservableCollection<SystemInfoCustom>>(foldersSerizlized);
            }

            if (saveChanges)
                Save();

            if (this.recentFolders.Count == 0)
            {
                TryToDetectFolders();
            }
        }

        public void Save()
        {
            SortRecentFolders();
            SortRecentFiles();

            this.recentFilesRepositoryProperty.SetValue(settings, SerializeObject(this.recentFiles), null);
            this.recentFoldersRepositoryProperty.SetValue(settings, SerializeObject(this.recentFolders), null);

            this.settings.Save();
        }

        private void SortRecentFolders()
        {
            var sorted = this.recentFolders.OrderByDescending(x => x.DateOpened).ToList();
          
            this.recentFolders.Clear();

            foreach (var item in sorted)
            {
                this.recentFolders.Add(item);
            }

            NotifyPropertyChanged("RecentFolders");
        }

        private void SortRecentFiles()
        {
            var sorted = this.RecentFiles.OrderByDescending(x => x.DateOpened).ToList();
            
            this.recentFiles.Clear();
           
            foreach (var item in sorted)
            {
                this.recentFiles.Add(item);
            }

            NotifyPropertyChanged("RecentFiles");
        }

        public void AddFile(string filePath, bool save = false)
        {
            if (!RecentFilesContains(filePath))
            {
                if (RecentFiles.Count > historyLimit)
                {
                    RecentFiles.RemoveAt(RecentFiles.Count - 1);
                }

                RecentFiles.Add(new SystemInfoCustom(filePath));
            }

         
            if (save)
                Save();
            else
                SortRecentFiles();
        }

        public void AddFolder(string dir, bool save = false)
        {
            if (!RecentFoldersContains(dir))
            {
                if (RecentFolders.Count > historyLimit)
                {
                    RecentFolders.RemoveAt(RecentFolders.Count - 1);
                }

                RecentFolders.Add(new SystemInfoCustom(dir));
            }

            if (save)
                Save();
            else
                SortRecentFolders();
        }

        public void RemoveFile(string filePath, bool save = false)
        {
            filePath = filePath.ToLower();

            var file = RecentFiles.FirstOrDefault(f => f.FullName.ToLower() == filePath);

            if (file != null)
            {
                RecentFiles.Remove(file);
            }


            if (save)
                Save();
        }

        public void RemoveFolder(string dirPath, bool save = false)
        {
            dirPath = dirPath.ToLower();

            var dir = RecentFolders.FirstOrDefault(d => d.FullName.ToLower() == dirPath);

            if (dir != null)
            {
                RecentFolders.Remove(dir);
            }

            if (save)
                Save();
        }

        private void TryToDetectFolders()
        {
            try
            {
                AddFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                AddFolder(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments));
            }
            catch { }
        }

        private bool RecentFilesContains(string filePath)
        {
            filePath = filePath.ToLower();
            var item = RecentFiles.FirstOrDefault(f => f.FullName.ToLower() == filePath);

            if (item != null)
            {
                item.DateOpened = DateTime.Now;
                return true;
            }

            return false;
        }

        private bool RecentFoldersContains(string directory)
        {
            directory = directory.ToLower();
            var item = RecentFolders.FirstOrDefault(d => d.FullName.ToLower() == directory);

            if (item != null)
            {
                item.DateOpened = DateTime.Now;
                return true;
            }

            return false;
        }

        private static string SerializeObject<T>(T objectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, objectToSerialize);
            return textWriter.ToString();
        }

        private static T DeserializeObject<T>(string str)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            TextReader reader = new StringReader(str);

            return (T)xmlSerializer.Deserialize(reader);

        }

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion //PropertyChanged
    }
}
