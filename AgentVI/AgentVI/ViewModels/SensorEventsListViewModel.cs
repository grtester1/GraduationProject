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
    public class SensorEventsListViewModel
    {
        public ObservableCollection<EventModel> SensorEventList { get; set; }

        public SensorEventsListViewModel(Sensor i_Sensor) : this(i_Sensor.SensorEvents)
        {
        }

        public SensorEventsListViewModel(InnoviObjectCollection<SensorEvent> i_SensorEventCollection)
        {
            SensorEventList = new ObservableCollection<EventModel>();
            foreach(SensorEvent se in i_SensorEventCollection)
            {
                SensorEventList.Add(new EventModel()
                {
                    CamName = se.SensorName,
                    ClipPath = se.ClipPath,
                    DateTime = se.StartTime.ToString(),
                    ImagePath = se.ImagePath,
                    RuleName = se.RuleName.ToString(),
                    Tag = se.Tag.ToString()
                });
            }
        }

        public void OnFilterStateUpdated(object source, EventArgs e)
        {
            //UpdateSensorEvents();
        }
    }
}