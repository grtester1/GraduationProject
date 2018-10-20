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
    public partial class HealthPage : ContentPage, INotifyContentViewChanged
    {
        private HealthListViewModel HealthPageVM = null;
        public event EventHandler<UpdatedContentEventArgs> RaiseContentViewUpdateEvent;

        public HealthPage()
        {
            InitializeComponent();

            HealthPageVM = new HealthListViewModel();
            initOnFilterStateUpdatedEventHandler();
            HealthPageVM.PopulateCollection();
            healthListView.BindingContext = HealthPageVM;
        }

        private void initOnFilterStateUpdatedEventHandler()
        {
            ServiceManager.Instance.FilterService.FilterStateUpdated += HealthPageVM.OnFilterStateUpdated;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            HealthPageVM.PopulateCollection();
        }

        private async void OnRefresh(object sender, EventArgs e)
        {
            try
            {
                await Task.Factory.StartNew(() => HealthPageVM.PopulateCollection());
                ((ListView)sender).IsRefreshing = false;
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //  NOT WORKING!
        private void onHealthTapped(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            var healthSelected = e.SelectedItem as HealthModel;

            if (healthSelected == null)
                return;

            DisplayAlert("Item Selected", healthSelected.HealthDescription, "Ok");
            ((ListView)sender).SelectedItem = null; // disable the visual selection state.
        }


        //The old HealthPage.xaml.cs:
        //---------------------------
        /*
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

        private async void OnRefresh(object sender, EventArgs e)
        {
            await System.Threading.Tasks.Task.Factory.StartNew(() => HealthPageVM.UpdateHealthList());
            ((ListView)sender).IsRefreshing = false; //end the refresh state
        }
        */
    }
}
