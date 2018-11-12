#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using AgentVI.ViewModels;
using Xamarin.Forms;
using System.Linq;
using AgentVI.Services;
using AgentVI.Interfaces;
using AgentVI.Models;
using AgentVI.Utils;
using System.Threading.Tasks;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;

namespace AgentVI.Views
{
    public partial class CamerasPage : ContentPage, INotifyContentViewChanged, IBindable
    {
        public IBindableVM BindableViewModel => SensorsListVM;
        public ContentPage ContentPage => this;
        private SensorsListViewModel SensorsListVM = null;
        public event EventHandler<UpdatedContentEventArgs> RaiseContentViewUpdateEvent;

        public CamerasPage()
        {
            InitializeComponent();

            SensorsListVM = new SensorsListViewModel();
            initOnFilterStateUpdatedEventHandler();
            SensorsListVM.PopulateCollection();
            cameraListView.BindingContext = SensorsListVM;
        }

        private void initOnFilterStateUpdatedEventHandler()
        {
            ServiceManager.Instance.FilterService.FilterStateUpdated += SensorsListVM.OnFilterStateUpdated;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SensorsListVM.PopulateCollection();
        }

        private async void OnRefresh(object sender, EventArgs e)
        {
            await Task.Factory.StartNew(() => SensorsListVM.PopulateCollection());
            ((ListView)sender).IsRefreshing = false;
        }

        private async void onSensorTapped(object sender, ItemTappedEventArgs e)
        {
            RaiseContentViewUpdateEvent?.Invoke(this, new UpdatedContentEventArgs(UpdatedContentEventArgs.EContentUpdateType.Buffering));
            UpdatedContentEventArgs updatedContentEventArgs = null;
            CameraEventsPage cameraEventsPageBuf = null;
            SensorModel sensorBuffer = e.Item as SensorModel;

            await Task.Factory.StartNew(() =>
            {
                cameraEventsPageBuf = new CameraEventsPage(sensorBuffer.Sensor);
                cameraEventsPageBuf.RaiseContentViewUpdateEvent += eventsRouter;
                updatedContentEventArgs = new UpdatedContentEventArgs(
                    UpdatedContentEventArgs.EContentUpdateType.Push, cameraEventsPageBuf,
                    cameraEventsPageBuf.BindableViewModel);
            });
            RaiseContentViewUpdateEvent?.Invoke(this, updatedContentEventArgs);
        }

        private void eventsRouter(object sender, UpdatedContentEventArgs e)
        {
            RaiseContentViewUpdateEvent?.Invoke(this, e);
        }
    }
}