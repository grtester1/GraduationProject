#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using System.Linq;
using AgentVI.Models;
using System.Collections.ObjectModel;

namespace AgentVI.ViewModels
{
    public class SensorEventsListViewModel : FilterDependentViewModel<EventModel>
    {
        public Sensor SensorSource { get; private set; }
        public string SensorName => SensorSource.Name;

        public SensorEventsListViewModel(Sensor i_Sensor) :base()
        {
            SensorSource = i_Sensor;
        }

        public override void OnFilterStateUpdated(object source, EventArgs e)
        {
            return;
        }

        public override void PopulateCollection()
        {
            base.PopulateCollection();
            enumerableCollection = SensorSource.SensorEvents.Select(sensorEvent =>
            {
                return EventModel.FactoryMethod(sensorEvent);
            });
            FetchCollection();
        }
    }
}