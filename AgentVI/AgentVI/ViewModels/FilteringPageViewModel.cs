#if DPROXY
using DummyProxy;
#else
using AgentVI.Models;
using AgentVI.Services;
using InnoviApiProxy;
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Extended;

namespace AgentVI.ViewModels
{
    public class FilteringPageViewModel : FilterDependentViewModel<FolderModel>
    {
        private bool canLoadMore = false;
        public int FilterID { get; private set; }

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
            for(int i=0; i<1 && canLoadMore; i++)
            {
                try
                {
                    ObservableCollection.Add(FolderModel.FactoryMethod(collectionEnumerator.Current as Folder));
                }catch(ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    canLoadMore = false;
                }
                canLoadMore = collectionEnumerator.MoveNext();
            }
        }

        public void UpdateFolders()
        {
            collectionEnumerator = ServiceManager.Instance.FilterService.CurrentLevel;
            canLoadMore = collectionEnumerator.MoveNext();
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
