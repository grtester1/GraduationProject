#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using AgentVI.ViewModels;
using Xamarin.Forms;
using AgentVI.Services;

namespace AgentVI.Views
{
    public partial class EventsPage : ContentPage
    {
        private EventsListViewModel SensorsEventsListVM = null;

        public EventsPage()
        {
            InitializeComponent();
            SensorsEventsListVM = new EventsListViewModel();
            initOnFilterStateUpdatedEventHandler();
            SensorsEventsListVM.UpdateEvents();
            eventListView.BindingContext = SensorsEventsListVM;
        }

        private void initOnFilterStateUpdatedEventHandler()
        {
            ServiceManager.Instance.FilterService.FilterStateUpdated += SensorsEventsListVM.OnFilterStateUpdated;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SensorsEventsListVM.UpdateEvents();
        }

        private async void OnRefresh(object sender, EventArgs e)
        {
            await System.Threading.Tasks.Task.Factory.StartNew(() => SensorsEventsListVM.UpdateEvents());
            ((ListView)sender).IsRefreshing = false;
        }

        private void OnSensorEvent_Tapped(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}