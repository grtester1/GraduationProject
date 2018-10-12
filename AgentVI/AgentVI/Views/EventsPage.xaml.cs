#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using AgentVI.ViewModels;
using Xamarin.Forms;
using AgentVI.Services;
using AgentVI.Interfaces;
using AgentVI.Utils;
using AgentVI.Models;
using System.Threading.Tasks;
using System.Collections;

namespace AgentVI.Views
{
    public partial class EventsPage : ContentPage, INotifyContentViewChanged
    {
        private EventsListViewModel SensorsEventsListVM = null;
        public event EventHandler<UpdatedContentEventArgs> RaiseContentViewUpdateEvent;

        public EventsPage()
        {
            InitializeComponent();
            SensorsEventsListVM = new EventsListViewModel();
            initOnFilterStateUpdatedEventHandler();
            SensorsEventsListVM.PopulateCollection();
            eventListView.BindingContext = SensorsEventsListVM;
        }

        private void initOnFilterStateUpdatedEventHandler()
        {
            ServiceManager.Instance.FilterService.FilterStateUpdated += SensorsEventsListVM.OnFilterStateUpdated;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SensorsEventsListVM.PopulateCollection();
        }

        private async void OnRefresh(object sender, EventArgs e)
        {
            try
            {
                await Task.Factory.StartNew(() => SensorsEventsListVM.PopulateCollection());
                ((ListView)sender).IsRefreshing = false;
            }catch(AggregateException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void onEventTapped(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                RaiseContentViewUpdateEvent?.Invoke(this, null);
                UpdatedContentEventArgs updatedContentEventArgs = null;
                EventDetailsPage eventDetailsPageBuf = null;
                EventModel selectedEvent = e.SelectedItem as EventModel;

                await Task.Factory.StartNew(() =>
                {
                    eventDetailsPageBuf = new EventDetailsPage(selectedEvent);
                    eventDetailsPageBuf.RaiseContentViewUpdateEvent += eventsRouter;
                });
                await Task.Factory.StartNew(() => updatedContentEventArgs = new UpdatedContentEventArgs(UpdatedContentEventArgs.EContentUpdateType.Push ,eventDetailsPageBuf));

                RaiseContentViewUpdateEvent?.Invoke(this, updatedContentEventArgs);
            }catch(ObjectDisposedException ex)
            {
                OnRefresh(sender, null);
            }
        }

        private void eventsRouter(object sender, UpdatedContentEventArgs e)
        {
            RaiseContentViewUpdateEvent?.Invoke(this, e);
        }
    }
}