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
using AgentVI.Services;
using AgentVI.Interfaces;
using AgentVI.Utils;
using System.Threading.Tasks;
using System.Collections;


namespace AgentVI.Views
{
    public partial class HealthPage : ContentPage
    {
        private HealthListViewModel HealthPageVM = null;
        private bool isHealthForSpecificSensor = false;
        private Sensor sensor = null;

        public HealthPage(Sensor i_sensor)
        {
            InitializeComponent();
            HealthPageVM = new HealthListViewModel();
            sensor = i_sensor;

            if (i_sensor != null)
            {
                isHealthForSpecificSensor = true;
                HealthPageVM.PopulateCollection(i_sensor);
                string cameraName = i_sensor.Name;
                cameraNameHeader.Text = "Health for " + cameraName;
                Title = cameraName;
            }
            else
            {
                isHealthForSpecificSensor = false;
                initOnFilterStateUpdatedEventHandler();
                HealthPageVM.PopulateCollection();
            }

            healthListView.BindingContext = HealthPageVM;
        }

        private void initOnFilterStateUpdatedEventHandler()
        {
            ServiceManager.Instance.FilterService.FilterStateUpdated += HealthPageVM.OnFilterStateUpdated;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(isHealthForSpecificSensor)
            {
                HealthPageVM.PopulateCollection(sensor);
            }
            else
            {
                HealthPageVM.PopulateCollection();
            }
        }

        private async void OnRefresh(object sender, EventArgs e)
        {
            if (isHealthForSpecificSensor)
            {
                try
                {
                    await Task.Factory.StartNew(() => HealthPageVM.PopulateCollection(sensor));
                    ((ListView)sender).IsRefreshing = false; //end the refresh state
                }
                catch (AggregateException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                try
                {
                    await Task.Factory.StartNew(() => HealthPageVM.PopulateCollection());
                    ((ListView)sender).IsRefreshing = false; //end the refresh state
                }
                catch (AggregateException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
    }
}
