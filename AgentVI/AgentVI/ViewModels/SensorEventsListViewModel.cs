#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using System.Linq;
using AgentVI.Models;

namespace AgentVI.ViewModels
{
    public class SensorEventsListViewModel : FilterDependentViewModel<EventModel>
    {
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

        public SensorEventsListViewModel(Sensor i_Sensor) :base()
        {
            SensorSource = i_Sensor;
        }

        public override void OnFilterStateUpdated(object source, EventArgs e)
        {
            return;
        }

        protected override void FetchCollection()
        {
            base.FetchCollection();
            updateFolderState();
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

        public override void PopulateCollection()
        {
            base.PopulateCollection();
            enumerableCollection = SensorSource.SensorEvents.
                Select(sensorEvent => EventModel.FactoryMethod(sensorEvent));
            FetchCollection();
        }
    }
}