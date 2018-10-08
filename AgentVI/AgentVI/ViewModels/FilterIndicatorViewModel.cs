using AgentVI.Services;
using InnoviApiProxy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AgentVI.ViewModels
{
    public class FilterIndicatorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Folder> _SelectedFoldersNamesCache = null;
        public ObservableCollection<Folder> SelectedFoldersNamesCache
        {
            get
            {
                return _SelectedFoldersNamesCache;
            }
            set
            {
                _SelectedFoldersNamesCache = new ObservableCollection<Folder>(value);
                OnPropertyChanged();
            }
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
