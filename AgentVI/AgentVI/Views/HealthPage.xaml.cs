#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using AgentVI.ViewModels;
using AgentVI.Services;
using AgentVI.Models;
using System.Threading.Tasks;
using AgentVI.Utils;

namespace AgentVI.Views
{
    public partial class HealthPage : ContentPage
    {

        private HealthListViewModel HealthListVM = null;
        public event EventHandler<UpdatedContentEventArgs> RaiseContentViewUpdateEvent;



        public HealthPage() // for filterd folder of cameras
        {
            InitializeComponent();

            HealthListVM = new HealthListViewModel();
            initOnFilterStateUpdatedEventHandler();
            HealthListVM.PopulateCollection();
            healthListView.BindingContext = HealthListVM;
        }
        /*
        public HealthPage(SensorModel i_sensor) // for specific camera
        {
            InitializeComponent();

            HealthListVM = new HealthListViewModel(i_sensor);
            cameraNameHeader.Text = "Health for " + i_sensor.SensorName;
            Title = i_sensor.SensorName;
            healthListView.ItemsSource = HealthListVM.HealthsList;
        }*/

        private void initOnFilterStateUpdatedEventHandler()
        {
            ServiceManager.Instance.FilterService.FilterStateUpdated += HealthListVM.OnFilterStateUpdated;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            HealthListVM.PopulateCollection();
        }

        private async void OnRefresh(object sender, EventArgs e)
        {
            await Task.Factory.StartNew(() => HealthListVM.PopulateCollection());
            ((ListView)sender).IsRefreshing = false;
        }



        /*
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
            healthListView.ItemsSource = HealthPageVM.HealthsList;
        }

*/
    }
}