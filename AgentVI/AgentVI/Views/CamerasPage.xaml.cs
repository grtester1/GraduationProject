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
using AgentVI.Views;
using AgentVI.Utils;

namespace AgentVI.Views
{
    public partial class CamerasPage : ContentPage, INotifyContentViewChanged
    {
        private SensorsListViewModel SensorsListVM = null;
        public event EventHandler<UpdatedContentEventArgs> RaiseContentViewUpdateEvent;

        public CamerasPage()
        {
            InitializeComponent();

            SensorsListVM = new SensorsListViewModel();
            initOnFilterStateUpdatedEventHandler();
            SensorsListVM.UpdateCameras();
            cameraListView.BindingContext = SensorsListVM;
        }

        private void initOnFilterStateUpdatedEventHandler()
        {
            ServiceManager.Instance.FilterService.FilterStateUpdated += SensorsListVM.OnFilterStateUpdated;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SensorsListVM.UpdateCameras();
        }

        private async void OnRefresh(object sender, EventArgs e)
        {
            await System.Threading.Tasks.Task.Factory.StartNew(() => SensorsListVM.UpdateCameras());
            ((ListView)sender).IsRefreshing = false; //end the refresh state
        }

        private void OnSensor_Tapped(object sender, EventArgs e)
        {
            var name = (sender as Grid).FindByName<Label>("SensorName");
            var a = SensorsListVM.ObservableCollection.Where(sensor => sensor.SensorName == name.Text);
            RaiseContentViewUpdateEvent?.Invoke(this, new UpdatedContentEventArgs(new CameraEventsPage(a.First().Sensor)));
        }
    }
}