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
    public partial class CamerasPage : ContentPage
    {
        private CamerasListViewModel CamerasPageVM = null;

        public CamerasPage()
        {
            InitializeComponent();

            CamerasPageVM = new CamerasListViewModel();
            initOnFilterStateUpdatedEventHandler();
            CamerasPageVM.InitializeList(ServiceManager.Instance.LoginService.LoggedInUser);
            cameraListView.ItemsSource = CamerasPageVM.CamerasList;
            cameraListView.BindingContext = CamerasPageVM.CamerasList; //????????????????????????? temporary
        }

        public void initOnFilterStateUpdatedEventHandler()
        {
            ServiceManager.Instance.FilterService.FilterStateUpdated += CamerasPageVM.OnFilterStateUpdated;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            CamerasPageVM.UpdateCameras();
        }

        private void OnRefresh(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            CamerasPageVM.UpdateCameras();
            list.IsRefreshing = false; //end the refresh state
        }

        /*void OnTap(object sender, ItemTappedEventArgs e)
        {
            DisplayAlert("Camera Tapped", e.Item.ToString(), "Ok");
        }*/

        private void onCameraNameTapped(object sender, EventArgs e)
        {
            (App.Current.MainPage as NavigationPage).PushAsync(new CameraEventsPage(CamerasPageVM.CamerasList.Where(cam => cam.CamName == (sender as Label).Text).First().Sensor));
        }
    }
}