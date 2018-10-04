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
using System.Collections.Generic;

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
            for (int i = 0; i < 3 && canLoadMore ; i++)
            {
                try
                {
                    ObservableCollection.Add(SensorModel.FactoryMethod(collectionEnumerator.Current as Sensor));
                }catch(ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    canLoadMore = false;
                }
                canLoadMore = collectionEnumerator.MoveNext();
            }
        }

        public void UpdateCameras()
        {
            collectionEnumerator = ServiceManager.Instance.FilterService.GetFilteredSensorsEnumerator();
            canLoadMore = collectionEnumerator.MoveNext();
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