#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using AgentVI.Services;
using AgentVI.Models;
using Xamarin.Forms.Extended;
using System.Threading.Tasks;

namespace AgentVI.ViewModels
{
    public class SensorsListViewModel : FilterDependentViewModel<SensorModel>
    {
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
                    return ObservableCollection.Count > -1;
                }
            };
        }

        public override void OnFilterStateUpdated(object source, EventArgs e)
        {
            UpdateCameras();
        }

        private void downloadData()
        {
            for (int i = 0; i < 1 && collectionEnumerator.Current != null; i++)
            {
                ObservableCollection.Add(SensorModel.FactoryMethod(collectionEnumerator.Current as Sensor));
                collectionEnumerator.MoveNext();
            }
        }

        public void UpdateCameras()
        {
            collectionEnumerator = ServiceManager.Instance.FilterService.GetFilteredSensorsEnumerator();
            collectionEnumerator.MoveNext();
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