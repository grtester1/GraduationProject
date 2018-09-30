#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using AgentVI.Models;

namespace AgentVI.ViewModels
{
    public class HealthListViewModel
    {
        public List<HealthModel> healthList { get; set; }

        public HealthListViewModel(Sensor i_sensor)
        {
            UpdateHealthList(i_sensor);
        }

        public void UpdateHealthList(Sensor i_sensor)
        {
            healthList = new List<HealthModel>();

            List<Sensor.Health> healths = i_sensor.SensorHealthArray;
            foreach (Sensor.Health sh in healths)
            {
                HealthModel hm = new HealthModel();
                hm.HealthTime = sh.StatusTimeStamp;
                hm.HealthDescription = sh.DetailedDescription;
                hm.HealthDuration = "1:05";
                healthList.Add(hm);
            }
        }
    }
}
