#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using System.Collections;
using AgentVI.Services;
using AgentVI.Models;
using System.Threading.Tasks;
using Xamarin.Forms.Extended;

namespace AgentVI.ViewModels
{
    public class EventsListViewModel : FilterDependentViewModel<EventModel>
    {
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
                    return ObservableCollection.Count > -1;
                }
            };
        }

        private void downloadData()
        {
            try
            {
                for (int i = 0; i < 1 && collectionEnumerator.Current != null; i++)
                {
                    ObservableCollection.Add(EventModel.FactoryMethod(collectionEnumerator.Current as SensorEvent));
                    collectionEnumerator.MoveNext();
                }
            }
        }

        public override void OnFilterStateUpdated(object source, EventArgs e)
        {
            UpdateEvents();
        }

        public void UpdateEvents()
        {

            collectionEnumerator = ServiceManager.Instance.FilterService.GetFilteredEventsEnumerator();
            collectionEnumerator.MoveNext();
            ObservableCollection.Clear();
            downloadData();
        }
    }
}