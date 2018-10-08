#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AgentVI.Services;

namespace AgentVI.ViewModels
{
    public class FilterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public IEnumerator AccountFolders { get; private set; }
        private ObservableCollection<Folder> _selectedFoldersCache;
        public ObservableCollection<Folder> SelectedFoldersCache
        {
            get
            {
                return _selectedFoldersCache;
            }
            set
            {
                _selectedFoldersCache = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<FilteringPageViewModel> _filteringPagesContent;
        public ObservableCollection<FilteringPageViewModel> FilteringPagesContent
        {
            get
            {
                return _filteringPagesContent;
            }
            private set
            {
                _filteringPagesContent = value;
                OnPropertyChanged();
            }
        }

        public FilterViewModel()
        {
            FilteringPagesContent = new ObservableCollection<FilteringPageViewModel>();
            ServiceManager.Instance.FilterService.SelectRootLevel();
            AccountFolders = ServiceManager.Instance.FilterService.CurrentLevel;
            FilteringPagesContent.Add(new FilteringPageViewModel(AccountFolders, 0));
        }

        public void FetchNextFilteringDepth(Folder i_selectedFolder)
        {
            for (int i = FilteringPagesContent.Count - 1; i >= i_selectedFolder.Depth; i--)
            {
                FilteringPagesContent.RemoveAt(i);
            }
            ServiceManager.Instance.FilterService.SelectFolder(i_selectedFolder);
            if (i_selectedFolder.Folders != null && !i_selectedFolder.Folders.IsEmpty())
            {
                FilteringPagesContent.Add(new FilteringPageViewModel(ServiceManager.Instance.FilterService.CurrentLevel, i_selectedFolder.Depth + 1));
            }

            SelectedFoldersCache = new ObservableCollection<Folder>(ServiceManager.Instance.FilterService.CurrentPath);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
