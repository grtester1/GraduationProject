#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using System.Collections.Generic;
using AgentVI.Services;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using AgentVI.Models;

namespace AgentVI.ViewModels
{
    public class SensorsListViewModel : FilterDependentViewModel<SensorModel>
    {
        public SensorsListViewModel()
        {
            ObservableCollection = new ObservableCollection<SensorModel>();
        }

        public override void OnFilterStateUpdated(object source, EventArgs e)
        {
            UpdateCameras();
        }

        public void UpdateCameras()
        {
            List<Sensor> filteredSensors = ServiceManager.Instance.FilterService.GetFilteredSensorCollection();
            ObservableCollection.Clear();
            filteredSensors.ForEach(sensor => ObservableCollection.Add(SensorModel.FactoryMethod(sensor)));
        }
    }
}