#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AgentVI.ViewModels;
using AgentVI.Services;
using AgentVI.Interfaces;
using AgentVI.Utils;
using AgentVI.Models;

namespace AgentVI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraEventsPage : ContentPage, INotifyContentViewChanged
    {
        private SensorEventsListViewModel SensorEventsListVM = null;
        public event EventHandler<UpdatedContentEventArgs> RaiseContentViewUpdateEvent;

        public CameraEventsPage()
        {
            InitializeComponent();
        }

        public CameraEventsPage(Sensor i_Sensor) : this()
        {
            SensorEventsListVM = new SensorEventsListViewModel(i_Sensor);
            SensorEventsListVM.PopulateCollection();
            cameraEventsListView.BindingContext = SensorEventsListVM;
            sensorNameLabel.Text = SensorEventsListVM.SensorSource.Name;
            IsEmptyFiller.IsVisible = SensorEventsListVM.IsEmptyFolder;
            IsEmptyText.IsVisible = SensorEventsListVM.IsEmptyFolder;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SensorEventsListVM.PopulateCollection();
        }

        private async void OnRefresh(object sender, EventArgs e)
        {
            try
            {
                await Task.Factory.StartNew(() => SensorEventsListVM.PopulateCollection());
                ((ListView)sender).IsRefreshing = false;
            }catch(AggregateException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void onCameraEventBackButtonTapped(object sender, EventArgs e)
        {
            RaiseContentViewUpdateEvent?.Invoke(this, new UpdatedContentEventArgs(UpdatedContentEventArgs.EContentUpdateType.Pop));
        }

        private async void cameraEventsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            RaiseContentViewUpdateEvent?.Invoke(this, null);
            EventModel selectedSensorEvent = e.SelectedItem as EventModel;
            UpdatedContentEventArgs updatedContentEventArgs = null;
            EventDetailsPage eventDetailsPageBuf = null;

            await Task.Factory.StartNew(() =>
            {
                eventDetailsPageBuf = new EventDetailsPage(selectedSensorEvent);
                eventDetailsPageBuf.RaiseContentViewUpdateEvent += eventsRouter;
            });
            await Task.Factory.StartNew(() => updatedContentEventArgs = new UpdatedContentEventArgs(UpdatedContentEventArgs.EContentUpdateType.Push, eventDetailsPageBuf));
            RaiseContentViewUpdateEvent?.Invoke(this, updatedContentEventArgs);
        }

        private void eventsRouter(object sender, UpdatedContentEventArgs e)
        {
            RaiseContentViewUpdateEvent?.Invoke(this, e);
        }
    }
}