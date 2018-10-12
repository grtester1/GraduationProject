#if DPROXY
using DummyProxy;
#else
using AgentVI.Models;
using AgentVI.Services;
using InnoviApiProxy;
#endif
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms.Extended;
using System.Collections.ObjectModel;

namespace AgentVI.ViewModels
{
    public class FilteringPageViewModel : FilterDependentViewModel<FolderModel>
    {
        private bool canLoadMore = false;
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

        private FilteringPageViewModel()
        {
            ObservableCollection = new InfiniteScrollCollection<FolderModel>()
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;
                    await Task.Factory.StartNew(() => downloadData());
                    IsBusy = false;
                    return null;
                },
                OnCanLoadMore = () =>
                {
                    return canLoadMore;
                }
            };
        }

        private void downloadData()
        {
            canLoadMore = true;

            foreach(FolderModel folder in collectionEnumerator)
            {
                ObservableCollection.Add(folder);
                canLoadMore = false;
            }
        }

        public void UpdateFolders()
        {
            collectionEnumerator = ServiceManager.Instance.FilterService.
                CurrentLevel.Select(folder => FolderModel.FactoryMethod(folder));
            CurrentPath = FilterIndicatorViewModel.currenPathToString(new ObservableCollection<Folder>(ServiceManager.Instance.FilterService.CurrentPath));
            ObservableCollection.Clear();
            downloadData();
        }

        public FilteringPageViewModel(int i_filterID) : this()
        {
            FilterID = i_filterID;
        }

        public override void OnFilterStateUpdated(object source, EventArgs e)
        {
            return;
        }
    }
}
