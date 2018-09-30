#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using System.Threading.Tasks;
using AgentVI.Models;
using Xamarin.Forms.Extended;

namespace AgentVI.ViewModels
{
    public class SensorEventsListViewModel : FilterDependentViewModel<EventModel>
    {
        private bool canLoadMore = false;
        public Sensor SensorSource { get; private set; }

        private SensorEventsListViewModel()
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

        public SensorEventsListViewModel(Sensor i_Sensor) : this()
        {
            SensorSource = i_Sensor;
        }

        public override void OnFilterStateUpdated(object source, EventArgs e) { return; }

        private void downloadData()
        {
            for(int i=0;i<1&&canLoadMore;i++)
            {
                try
                {
                    ObservableCollection.Add(EventModel.FactoryMethod(collectionEnumerator.Current as SensorEvent));
                }catch(ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    canLoadMore = false;
                }
                canLoadMore = collectionEnumerator.MoveNext();
            }
        }

        internal void UpdateSensorEvents()
        {
            collectionEnumerator = SensorSource.SensorEvents.GetEnumerator();
            canLoadMore = collectionEnumerator.MoveNext();
            ObservableCollection.Clear();
            downloadData();
        }
    }
}