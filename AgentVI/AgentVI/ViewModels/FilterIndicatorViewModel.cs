using AgentVI.Services;
using InnoviApiProxy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AgentVI.ViewModels
{
    public class FilterIndicatorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _selectedFoldersNamesCacheStr = null;
        public string SelectedFoldersNamesCacheStr
        {
            get => _selectedFoldersNamesCacheStr;
            private set
            {
                _selectedFoldersNamesCacheStr = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Folder> _selectedFoldersNamesCache = null;
        public ObservableCollection<Folder> SelectedFoldersNamesCache
        {
            get => _selectedFoldersNamesCache;
            set
            {
                _selectedFoldersNamesCache = new ObservableCollection<Folder>(value);
                SelectedFoldersNamesCacheStr = currenPathToString(_selectedFoldersNamesCache);
                OnPropertyChanged();
            }
        }

        public static string currenPathToString(ObservableCollection<Folder> i_FolderCollection)
        {
            string separator = "/";
            string prefix = "root" + separator;
            StringBuilder resBuilder = new StringBuilder(prefix);

            foreach(Folder folder in i_FolderCollection)
            {
                resBuilder.Append(folder.Name).Append(separator);
            }

            return resBuilder.ToString();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public FilterIndicatorViewModel()
        {
            SelectedFoldersNamesCache = new ObservableCollection<Folder>();
        }

        internal void UpdateCurrentPath()
        {
            SelectedFoldersNamesCache = new ObservableCollection<Folder>(ServiceManager.Instance.FilterService.CurrentPath);
        }
    }
}
