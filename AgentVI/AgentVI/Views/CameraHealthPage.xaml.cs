#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using AgentVI.ViewModels;
using AgentVI.Models;

namespace AgentVI.Views
{
    public partial class CameraHealthPage : ContentPage
    {
        private SensorHealthListViewModel SensorHealthPageVM = null;

        public CameraHealthPage()
        {
            InitializeComponent();
        }

        public CameraHealthPage(SensorModel i_sensor) : this()
        {
            SensorHealthPageVM = new SensorHealthListViewModel(i_sensor);
            cameraNameHeader.Text = "Health for " + i_sensor.SensorName;
            Title = i_sensor.SensorName;
            sensorHealthListView.ItemsSource = SensorHealthPageVM.HealthsList;
        }

        private async void OnRefresh(object sender, EventArgs e)
        {
            await System.Threading.Tasks.Task.Factory.StartNew(() => SensorHealthPageVM.UpdateHealthList());
            ((ListView)sender).IsRefreshing = false; //end the refresh state
        }
    }
}