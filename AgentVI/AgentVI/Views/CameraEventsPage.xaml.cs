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

namespace AgentVI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraEventsPage : ContentPage
    {
        private SensorEventsListViewModel SensorEventsListVM = null;

        public CameraEventsPage()
        {
            InitializeComponent();
        }

        public CameraEventsPage(Sensor i_Sensor) : this()
        {
            SensorEventsListVM = new SensorEventsListViewModel(i_Sensor);
            SensorEventsListVM.UpdateSensorEvents();
            cameraEventsListView.BindingContext = SensorEventsListVM;
            sensorNameLabel.Text = SensorEventsListVM.SensorSource.Name;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SensorEventsListVM.UpdateSensorEvents();
        }

        private async void OnRefresh(object sender, EventArgs e)
        {
            await Task.Factory.StartNew(() => SensorEventsListVM.UpdateSensorEvents());
            ((ListView)sender).IsRefreshing = false;
        }

        private void onCameraEventTapped(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}