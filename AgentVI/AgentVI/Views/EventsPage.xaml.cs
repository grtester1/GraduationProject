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
    public partial class EventsPage : ContentPage, INotifyContentViewChanged, IBindable
    {
        public IBindableVM BindableViewModel => SensorsEventsListVM;
        public ContentPage ContentPage => this;
        private EventsListViewModel SensorsEventsListVM = null;
        public event EventHandler<UpdatedContentEventArgs> RaiseContentViewUpdateEvent;
        public Command<EventModel> Command => new Command<EventModel>(val =>
        {
            onSensorNameTapped(val);
        });

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

        private async void onEventTapped(object sender, ItemTappedEventArgs e)
        {
            Console.WriteLine("###Logger###   -   begin EventsPage.onEventTapped main thread ");
            RaiseContentViewUpdateEvent?.Invoke(this, null);
            UpdatedContentEventArgs updatedContentEventArgs = null;
            EventDetailsPage eventDetailsPageBuf = null;
            EventModel selectedEvent = e.Item as EventModel;

            await Task.Factory.StartNew(() =>
            {
                Console.WriteLine("###Logger###   -   begin EventsPage.onEventTapped new thread 1 ");
                eventDetailsPageBuf = new EventDetailsPage(selectedEvent);
                eventDetailsPageBuf.RaiseContentViewUpdateEvent += eventsRouter;
                updatedContentEventArgs = new UpdatedContentEventArgs(
                    UpdatedContentEventArgs.EContentUpdateType.Push, eventDetailsPageBuf,
                    eventDetailsPageBuf.BindableViewModel);
                Console.WriteLine("###Logger###   -   end EventsPage.onEventTapped new thread 1 ");
            });

            Console.WriteLine("###Logger###   -   in EventsPage.onEventTapped main thread @ start eventDetailsPage invoke");
            RaiseContentViewUpdateEvent?.Invoke(this, updatedContentEventArgs);
            Console.WriteLine("###Logger###   -   in EventsPage.onEventTapped main thread @ after eventDetailsPage invoke");
            Console.WriteLine("###Logger###   -   end EventsPage.onEventTapped main thread ");
        }

        private async void onSensorNameTapped(EventModel val)
        {
            RaiseContentViewUpdateEvent?.Invoke(this, new UpdatedContentEventArgs(UpdatedContentEventArgs.EContentUpdateType.Buffering));
            UpdatedContentEventArgs updatedContentEventArgs = null;
            CameraEventsPage cameraEventsPageBuf = null;
            Sensor sensorBuffer = val.Sensor;

            await Task.Factory.StartNew(() =>
            {
                cameraEventsPageBuf = new CameraEventsPage(sensorBuffer);
                cameraEventsPageBuf.RaiseContentViewUpdateEvent += eventsRouter;
            });
            await Task.Factory.StartNew(() => 
            updatedContentEventArgs = new UpdatedContentEventArgs(
                UpdatedContentEventArgs.EContentUpdateType.Push, cameraEventsPageBuf, cameraEventsPageBuf.BindableViewModel
                ));
            RaiseContentViewUpdateEvent?.Invoke(this, updatedContentEventArgs);
        }

        private void eventsRouter(object sender, UpdatedContentEventArgs e)
        {
            RaiseContentViewUpdateEvent?.Invoke(this, e);
        }
    }
}