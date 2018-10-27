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
    }
}
