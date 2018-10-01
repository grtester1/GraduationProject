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
    public partial class HealthPage : ContentPage
    {
        private HealthListViewModel HealthPageVM = null;

        public HealthPage()
        {
            InitializeComponent();
        }

        public HealthPage(SensorModel i_sensor) : this()
        {
            HealthPageVM = new HealthListViewModel(i_sensor);
            cameraNameHeader.Text = "Health for " + i_sensor.SensorName;
            Title = i_sensor.SensorName;
            listView.ItemsSource = HealthPageVM.HealthsList;
        }
    }
}