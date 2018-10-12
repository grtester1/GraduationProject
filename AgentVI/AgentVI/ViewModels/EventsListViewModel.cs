#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using System.Linq;
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
            canLoadMore = true;

            foreach(EventModel eventModel in collectionEnumerator)
            {
                ObservableCollection.Add(eventModel);
                canLoadMore = false;
            }
        }

        public override void OnFilterStateUpdated(object source, EventArgs e)
        {
            UpdateEvents();
        }

        public void UpdateEvents()
        {
            collectionEnumerator = ServiceManager.Instance.FilterService.
                FilteredEvents.Select(sensorEvent => EventModel.FactoryMethod(sensorEvent));
            ObservableCollection.Clear();
            downloadData();
        }
    }
}