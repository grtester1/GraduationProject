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

namespace AgentVI.Views
{
    public partial class EventsPage : ContentPage
    {
        private EventsListViewModel allEventsVM = null;

        public EventsPage()
        {
            InitializeComponent();
            User user = ServiceManager.Instance.LoginService.LoggedInUser;
            allEventsVM = new EventsListViewModel();
            allEventsVM.InitializeList(user);
            eventListView.ItemsSource = allEventsVM.EventsList;
            eventListView.BindingContext = allEventsVM.EventsList; //????????????????????????? temporary         
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            allEventsVM.UpdateEvents();
        }

        private void OnRefresh(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            allEventsVM.UpdateEvents();
            list.IsRefreshing = false; //end the refresh state
        }

        void cameraButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Message", "???", "Ok");
        }
    }
}