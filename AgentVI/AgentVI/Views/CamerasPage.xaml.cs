
using DummyProxy;

//using InnoviApiProxy;

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
        private CamerasListViewModel allCamerasVM = null;

        public CamerasPage()
        {
            InitializeComponent();

            User user = ServiceManager.Instance.LoginService.LoggedInUser;
            allCamerasVM = new CamerasListViewModel();
			allCamerasVM.InitializeList(user);
			cameraListView.ItemsSource = allCamerasVM.CamerasList;
			cameraListView.BindingContext = allCamerasVM.CamerasList; //????????????????????????? temporary
            
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();
			allCamerasVM.UpdateCameras();
		}
              
		private void OnRefresh(object sender, EventArgs e)
        {
            var list = (ListView)sender;
			allCamerasVM.UpdateCameras();
			list.IsRefreshing = false; //end the refresh state
        }      

        /*void OnTap(object sender, ItemTappedEventArgs e)
        {
            DisplayAlert("Camera Tapped", e.Item.ToString(), "Ok");
        }*/
        
        private void onCameraNameTapped(object sender, EventArgs e)
        {            
            (App.Current.MainPage as NavigationPage).PushAsync(new CameraEventsPage(allCamerasVM.CamerasList.Where(cam => cam.CamName == (sender as Label).Text).First().Sensor));
        }
    }
}