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
                    OnPropertyChanged();
                }
            }
        }

        public override void PopulateCollection()
        {
            base.PopulateCollection();
            enumerableCollection = ServiceManager.Instance.FilterService.
                CurrentLevel.Select(folder => FolderModel.FactoryMethod(folder));
            CurrentPath = FilterIndicatorViewModel.currenPathToString(new ObservableCollection<Folder>(ServiceManager.Instance.FilterService.CurrentPath));
            FetchCollection();
        }

        public FilteringPageViewModel(int i_filterID):base()
        {
            FilterID = i_filterID;
        }

        public override void OnFilterStateUpdated(object source, EventArgs e)
        {
            return;
        }
    }
}
