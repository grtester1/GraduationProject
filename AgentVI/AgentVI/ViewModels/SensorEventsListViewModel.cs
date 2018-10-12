#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using System.Linq;
using System.Threading.Tasks;
using AgentVI.Models;
using Xamarin.Forms.Extended;

namespace AgentVI.ViewModels
{
    public class SensorEventsListViewModel : FilterDependentViewModel<EventModel>
    {
        private bool canLoadMore = false;
        public Sensor SensorSource { get; private set; }
        private bool _isEmptyFolder = true;
        public bool IsEmptyFolder
        {
            get => _isEmptyFolder;
            set
            {
                _isEmptyFolder = value;
                OnPropertyChanged();
            }
        }

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
            canLoadMore = true;
            foreach (EventModel eventModel in collectionEnumerator)
            {
                ObservableCollection.Add(eventModel);
                canLoadMore = false;
            }
            updateFolderState();
        }

        private void updateFolderState()
        {
            if(ObservableCollection.Count == 0)
            {
                IsEmptyFolder = true;
            }
            else
            {
                IsEmptyFolder = false;
            }
        }

        internal void UpdateSensorEvents()
        {
            collectionEnumerator = SensorSource.SensorEvents.
                Select(sensorEvent => EventModel.FactoryMethod(sensorEvent));
            ObservableCollection.Clear();
            downloadData();
        }
    }
}