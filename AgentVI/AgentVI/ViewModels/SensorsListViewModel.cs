#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using System.Linq;
using AgentVI.Services;
using AgentVI.Models;
using Xamarin.Forms.Extended;
using System.Threading.Tasks;

namespace AgentVI.ViewModels
{
    public class SensorsListViewModel : FilterDependentViewModel<SensorModel>
    {
        private bool canLoadMore = false;

        public SensorsListViewModel()
        {
            ObservableCollection = new InfiniteScrollCollection<SensorModel>()
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

        public override void OnFilterStateUpdated(object source, EventArgs e)
        {
            UpdateCameras();
        }

        private void downloadData()
        {
            canLoadMore = true;
            foreach(SensorModel sensorModel in collectionEnumerator)
            {
                ObservableCollection.Add(sensorModel);
                canLoadMore = false;
            }
        }

        public void UpdateCameras()
        {
            collectionEnumerator = ServiceManager.Instance.FilterService.
                FilteredSensorCollection.Select(sensor => SensorModel.FactoryMethod(sensor));
            ObservableCollection.Clear();
            downloadData();
        }

        /*======Old working ver.=======
        public void UpdateCameras()
        {
            List<Sensor> filteredSensors = ServiceManager.Instance.FilterService.GetFilteredSensorCollection();
            ObservableCollection.Clear();
            filteredSensors.ForEach(sensor => ObservableCollection.Add(SensorModel.FactoryMethod(sensor)));
        }*/
    }
}