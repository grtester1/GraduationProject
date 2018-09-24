﻿#if DPROXY
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

namespace AgentVI.Views
{
    public partial class EventsPage : ContentPage
    {
        private EventsListViewModel SensorsEventsListVM = null;

        public EventsPage()
        {
            InitializeComponent();
            SensorsEventsListVM = new EventsListViewModel();
            initOnFilterStateUpdatedEventHandler();
            SensorsEventsListVM.InitializeList(ServiceManager.Instance.LoginService.LoggedInUser);
            eventListView.BindingContext = SensorsEventsListVM;
        }

        public void initOnFilterStateUpdatedEventHandler()
        {
            ServiceManager.Instance.FilterService.FilterStateUpdated += SensorsEventsListVM.OnFilterStateUpdated;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SensorsEventsListVM.UpdateEvents();
        }

        private void OnRefresh(object sender, EventArgs e)
        {
            SensorsEventsListVM.UpdateEvents();
            ((ListView)sender).IsRefreshing = false; //end the refresh state
        }

        private void OnSensorEvent_Tapped(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}