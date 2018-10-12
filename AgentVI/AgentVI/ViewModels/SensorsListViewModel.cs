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
        public override void OnFilterStateUpdated(object source, EventArgs e)
        {
            PopulateCollection();
        }

        public override void PopulateCollection()
        {
            base.PopulateCollection();
            enumerableCollection = ServiceManager.Instance.FilterService.
                FilteredSensorCollection.Select(sensor => SensorModel.FactoryMethod(sensor));
            FetchCollection();
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