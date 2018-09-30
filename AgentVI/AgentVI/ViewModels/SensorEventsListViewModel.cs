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
            for(int i=0;i<1&&canLoadMore;i++)
            {
                try
                {
                    ObservableCollection.Add(EventModel.FactoryMethod(collectionEnumerator.Current as SensorEvent));
                    updateFolderState();                    
                }catch(ArgumentOutOfRangeException e)
                {
                    updateFolderState();
                    Console.WriteLine(e.Message);
                    canLoadMore = false;
                }
                canLoadMore = collectionEnumerator.MoveNext();
            }
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
            collectionEnumerator = SensorSource.SensorEvents.GetEnumerator();
            canLoadMore = collectionEnumerator.MoveNext();
            ObservableCollection.Clear();
            downloadData();
        }
    }
}