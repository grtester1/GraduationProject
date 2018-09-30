#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using AgentVI.ViewModels;
using Xamarin.Forms;
using System.Linq;
using AgentVI.Services;
using AgentVI.Models;

namespace AgentVI.Views
{
    public partial class CamerasPage : ContentPage
    {
        private SensorsListViewModel SensorsListVM = null;

        public CamerasPage()
        {
            InitializeComponent();

            SensorsListVM = new SensorsListViewModel();
            initOnFilterStateUpdatedEventHandler();
            SensorsListVM.UpdateCameras();
            cameraListView.BindingContext = SensorsListVM;
        }

        public void initOnFilterStateUpdatedEventHandler()
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
            (Application.Current.MainPage as NavigationPage).PushAsync(new HealthPage(a.First().Sensor));
        }
    }
}