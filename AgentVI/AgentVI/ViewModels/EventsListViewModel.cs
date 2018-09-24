#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using System.Collections;
using AgentVI.Services;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using AgentVI.Models;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms.Extended;

namespace AgentVI.ViewModels
{
    public class EventsListViewModel : FilterDependentViewModel<EventModel>
    {

        private IEnumerator collectionEnumerator;
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public EventsListViewModel()
        {
            ObservableCollection = new InfiniteScrollCollection<EventModel>()
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

        private void downloadData()
        {
            for (int i = 0; i < 1 && collectionEnumerator.Current != null; i++)
            {
                try
                {
                    ObservableCollection.Add(EventModel.FactoryMethod(collectionEnumerator.Current as SensorEvent));
                    Console.WriteLine(ObservableCollection.Count);
                    collectionEnumerator.MoveNext();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public override void OnFilterStateUpdated(object source, EventArgs e)
        {
            UpdateEvents();
        }

        public void UpdateEvents()
        {
            collectionEnumerator = ServiceManager.Instance.LoginService.LoggedInUser.GetDefaultAccountEvents().GetEnumerator();
            collectionEnumerator.MoveNext();
            //List<Sensor> filteredSensors = ServiceManager.Instance.FilterService.GetFilteredSensorCollection();
            ObservableCollection.Clear();
            downloadData();
            //Task.Factory.StartNew(() => extractAndShowSensorsEvents(ServiceManager.Instance.LoginService.LoggedInUser.GetDefaultAccountEvents()));
            //Task.Factory.StartNew(() => extractAndShowSensorsEvents(filteredSensors));
            //filteredEvents.ForEach(se => ObservableCollection.Add(EventModel.FactoryMethod(se)));
        }

        //private void extractAndShowSensorsEvents(List<Sensor> filteredSensors)
        //{

        //    SortedDictionary<ulong, EventModel> res = new SortedDictionary<ulong, EventModel>(new ReverseComparer<ulong>(Comparer<ulong>.Default));
        //    object AddToDictionaryLock = new object();
        //    List<Task> sensorsEventsFetchingTasks = new List<Task>();

        //    foreach (Sensor s in filteredSensors)
        //    {
        //        sensorsEventsFetchingTasks.Add(new Task(() =>
        //        {
        //            foreach (SensorEvent se in s.SensorEvents)
        //            {
        //                lock (AddToDictionaryLock)
        //                {
        //                    res.Add(se.StartTime, EventModel.FactoryMethod(se));
        //                    Console.WriteLine(se.RuleName.ToString());
        //                }
        //            }
        //        }));
        //    }

        //    Parallel.ForEach(sensorsEventsFetchingTasks, task => task.Start());
        //    Task.WhenAll(sensorsEventsFetchingTasks);
        //    foreach(var v in res)
        //    {
        //        ObservableCollection.Add(v.Value);
        //    }
        //}

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
    }
}