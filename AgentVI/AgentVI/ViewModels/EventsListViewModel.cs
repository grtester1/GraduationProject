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
using System.Threading.Tasks;

namespace AgentVI.ViewModels
{
    public class EventsListViewModel : FilterDependentViewModel<EventModel>
    {
        public EventsListViewModel()
        {
            ObservableCollection = new ObservableCollection<EventModel>();
        }

        public override void OnFilterStateUpdated(object source, EventArgs e)
        {
            UpdateEvents();
        }

        public void UpdateEvents()
        {
            List<Sensor> filteredSensors = ServiceManager.Instance.FilterService.GetFilteredSensorCollection();
            ObservableCollection.Clear();
            Task.Factory.StartNew(() => extractAndShowSensorsEvents(filteredSensors));
            //filteredEvents.ForEach(se => ObservableCollection.Add(EventModel.FactoryMethod(se)));
        }

        private void extractAndShowSensorsEvents(List<Sensor> filteredSensors)
        {

            SortedDictionary<ulong, EventModel> res = new SortedDictionary<ulong, EventModel>(new ReverseComparer<ulong>(Comparer<ulong>.Default));
            object AddToDictionaryLock = new object();
            List<Task> sensorsEventsFetchingTasks = new List<Task>();

            foreach (Sensor s in filteredSensors)
            {
                sensorsEventsFetchingTasks.Add(new Task(() =>
                {
                    foreach (SensorEvent se in s.SensorEvents)
                    {
                        lock (AddToDictionaryLock)
                        {
                            res.Add(se.StartTime, EventModel.FactoryMethod(se));
                            Console.WriteLine(se.RuleName.ToString());
                        }
                    }
                }));
            }

            Parallel.ForEach(sensorsEventsFetchingTasks, task => task.Start());
            Task.WhenAll(sensorsEventsFetchingTasks);
            foreach(var v in res)
            {
                ObservableCollection.Add(v.Value);
            }
        }

        private class ReverseComparer<T> : IComparer<T>
        {
            private readonly IComparer<T> original;

            public ReverseComparer(IComparer<T> i_Original)
            {
                original = i_Original;
            }

            public int Compare(T i_Left, T i_Right)
            {
                return original.Compare(i_Right, i_Left);
            }
        }

        public void InitializeList(User i_loggedInUser)
        {
            if (i_loggedInUser != null)
            {
                UpdateEvents();
            }
            else
            {
                throw new Exception("Method InitializeList was called with null param");
            }
        }
    }
}