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
        private EventsListViewModel allEvents = null;

        public EventsPage()
        {
            InitializeComponent();

            User user = ServiceManager.Instance.LoginService.LoggedInUser;
            allEvents = new EventsListViewModel();
            allEvents.InitializeList(user);
            eventListView.ItemsSource = allEvents.EventsList;
        }

        void OnRefresh(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            //put the refreshing logic here
            var itemList = allEvents.EventsList.Reverse().ToList();
            allEvents.EventsList.Clear();
            foreach (var s in itemList)
            {
                allEvents.EventsList.Add(s);
            }
            //make sure to end the refresh state
            list.IsRefreshing = false;
        }

        void cameraButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Message", "???", "Ok");
        }
    }
}