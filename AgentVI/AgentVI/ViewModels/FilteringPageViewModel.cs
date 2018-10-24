#if DPROXY
using DummyProxy;
#else
using AgentVI.Models;
using AgentVI.Services;
using InnoviApiProxy;
#endif
using System;
using System.Linq;
using System.Collections.ObjectModel;

namespace AgentVI.ViewModels
{
    public class FilteringPageViewModel : FilterDependentViewModel<FolderModel>
    {
        public int FilterID { get; private set; }
        private string _currentPath;
        public string CurrentPath
        {
            get => _currentPath;
            private set
            {
                if (_currentPath == null || String.IsNullOrEmpty(_currentPath))
                {
                    _currentPath = value;
                    OnPropertyChanged(nameof(CurrentPath));
                }
            }
        }

        public override void PopulateCollection()
        {
            base.PopulateCollection();
            enumerableCollection = ServiceManager.Instance.FilterService.CurrentLevel as ObservableCollection<FolderModel>;
            if (enumerableCollection == null)
            {
                enumerableCollection = ServiceManager.Instance.FilterService.CurrentLevel;
            }
            CurrentPath = FilterIndicatorViewModel.currenPathToString(ServiceManager.Instance.FilterService.CurrentPath);
            FetchCollection();
        }

        public FilteringPageViewModel(int i_filterID) : base()
        {
            FilterID = i_filterID;
        }

        protected override void FetchCollection()
        {
            if (enumerableCollection is ObservableCollection<FolderModel>)
            {
                ObservableCollection = enumerableCollection as ObservableCollection<FolderModel>;
                canLoadMore = false;
            }
            else
            {
                base.FetchCollection();
            }
        }

        public override void OnFilterStateUpdated(object source, EventArgs e)
        {
            return;
        }
    }
}
