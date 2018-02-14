using IgWord.Business;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq.Expressions;

namespace IgWord.Services
{
    public interface IRecentFilesService
    {
        ObservableCollection<SystemInfoCustom> RecentFolders { get; }
        ObservableCollection<SystemInfoCustom> RecentFiles { get; }
        event PropertyChangedEventHandler PropertyChanged;

        void Save();
        void AddFile(string filePath, bool save);
        void RemoveFile(string filePath, bool save);
        void AddFolder(string dirPath, bool save);
        void RemoveFolder(string dirPath, bool save);
        void SetRepositories(ApplicationSettingsBase settings,
                                      Expression<Func<ApplicationSettingsBase, string>> recentFilesRepositoryExpresion,
                                      Expression<Func<ApplicationSettingsBase, string>> recentFoldersRepositoryExpresion);

    }
}
