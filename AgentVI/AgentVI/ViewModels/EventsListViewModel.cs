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
            ObservableCollection.Clear();
            downloadData();
        }
    }
}