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
        private HealthListViewModel healthListVM = null;

        public HealthPage()
        {
            InitializeComponent();
            cameraNameHeader.Text = "no camera selected";
        }

        public HealthPage(Sensor i_sensor)
        {
            InitializeComponent();
            healthListVM = new HealthListViewModel(i_sensor);
            cameraNameHeader.Text = "Health for " + i_sensor.Name;
            Title = i_sensor.Name;

            listView.ItemsSource = healthListVM.healthList;
        }
    }
}