#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using AgentVI.Services;
using AgentVI.Models;
using System.Threading.Tasks;
using Xamarin.Forms.Extended;

namespace AgentVI.ViewModels
{
    public class EventsListViewModel : FilterDependentViewModel<EventModel>
    {
        bool canLoadMore = false;

        public EventsListViewModel()
        {
            ObservableCollection = new InfiniteScrollCollection<EventModel>()
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
            for (int i = 0; i < 3 && canLoadMore; i++)
            {
                try
                {
                    ObservableCollection.Add(EventModel.FactoryMethod(collectionEnumerator.Current as SensorEvent));
                }catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    canLoadMore = false;
                }
                canLoadMore = collectionEnumerator.MoveNext();
            }
        }

        public override void OnFilterStateUpdated(object source, EventArgs e)
        {
            UpdateEvents();
        }

        public void UpdateEvents()
        {
            collectionEnumerator = ServiceManager.Instance.FilterService.GetFilteredEventsEnumerator();
            canLoadMore = collectionEnumerator.MoveNext();
            ObservableCollection.Clear();
            downloadData();
        }
    }
}