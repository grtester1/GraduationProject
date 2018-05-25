using AgentVI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using InnoviApiProxy;
//using DummyProxy;
using AgentVI.Services;

namespace AgentVI.ViewModels
{
    public class FilterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IFilterService _filterService;

        private List<String> _SelectedFoldersNames;
        public List<String> SelectedFoldersCache
        {
            get
            {
                return _SelectedFoldersNames;
            }
            set
            {
                _SelectedFoldersNames = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<FilteringPageViewModel> _FilteringPagesContent;
        public ObservableCollection<FilteringPageViewModel> FilteringPagesContent
        {
            get
            {
                return _FilteringPagesContent;
            }
            private set
            {
                _FilteringPagesContent = value;
                OnPropertyChanged();
            }
        }
        public List<Folder> AccountFolders { get; private set; }


        public FilterViewModel(IFilterService i_filterService)
        {
            FilteringPagesContent = new ObservableCollection<FilteringPageViewModel>();
            _filterService = i_filterService;
            AccountFolders = _filterService.getAccountFolders(ServiceManager.Instance.LoginService.LoggedInUser);

            FilteringPagesContent.Add(new FilteringPageViewModel(_filterService.getAccountFolders(ServiceManager.Instance.LoginService.LoggedInUser), 0));
        }


        public void fetchNextFilteringDepth(Folder i_selectedFolder, int i_nextDepthValue)
        {
            for(int i=FilteringPagesContent.Count-1;i>=i_nextDepthValue;i--)
            {
                FilteringPagesContent.RemoveAt(i);
            }
            FilteringPagesContent.Add(new FilteringPageViewModel(_filterService.selectFolder(i_selectedFolder), i_nextDepthValue));
            SelectedFoldersCache = ServiceManager.Instance.FilterService.getSelectedFoldersHirearchy();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
