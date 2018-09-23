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
    public class SensorsListViewModel
    {
        public ObservableCollection<SensorModel> SensorsList { get; private set; }

        public SensorsListViewModel()
        {
            SensorsList = new ObservableCollection<SensorModel>();
        }

        public void OnFilterStateUpdated(object source, EventArgs e)
        {
            UpdateCameras();
        }

        public void UpdateCameras()
        {
            List<Sensor> filteredSensors = ServiceManager.Instance.FilterService.GetFilteredSensorCollection();
            SensorsList.Clear();
            filteredSensors.ForEach(sensor => SensorsList.Add(SensorModel.FactoryMethod(sensor)));
        }

        public void InitializeList(User i_loggedInUser)
        {
            if (i_loggedInUser != null)
            {
                UpdateCameras();
            }
            else
            {
                throw new Exception("Method InitializeList was called with null param");
            }
        }
    }
}