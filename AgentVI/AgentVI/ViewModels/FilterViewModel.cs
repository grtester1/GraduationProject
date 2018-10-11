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
using Xamarin.Forms;

namespace AgentVI.ViewModels
{
    public class FilterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _isFetching = false;
        public bool IsFetching
        {
            get => _isFetching;
            set
            {
                _isFetching = value;
                OnPropertyChanged(nameof(IsFetching));
            }
        }
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
        public Command OnTesting = new Command(InTesting);

        public static void InTesting(object param)
        {

        }

        public FilterViewModel()
        {
            IsFetching = false;
            FilteringPagesContent = new ObservableCollection<FilteringPageViewModel>();
            ServiceManager.Instance.FilterService.SelectRootLevel();
            FilteringPageViewModel currentFiltrationLevel = new FilteringPageViewModel(0);
            currentFiltrationLevel.UpdateFolders();
            FilteringPagesContent.Add(currentFiltrationLevel);
        }

        public void FetchNextFilteringDepth(Folder i_selectedFolder)
        {
            IsFetching = true;
            for (int i = FilteringPagesContent.Count - 1; i > i_selectedFolder.Depth; i--)
            {
                FilteringPagesContent.RemoveAt(i);
            }
            ServiceManager.Instance.FilterService.SelectFolder(i_selectedFolder);
            if (i_selectedFolder.Folders != null && !i_selectedFolder.Folders.IsEmpty())
            {
                FilteringPageViewModel currentFiltrationLevel = new FilteringPageViewModel(i_selectedFolder.Depth + 1);
                currentFiltrationLevel.UpdateFolders();
                FilteringPagesContent.Add(currentFiltrationLevel);
            }

            SelectedFoldersCache = new ObservableCollection<Folder>(ServiceManager.Instance.FilterService.CurrentPath);
            IsFetching = false;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
