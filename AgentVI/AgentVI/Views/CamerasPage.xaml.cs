using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using InnoviApiProxy;
using AgentVI.ViewModels;
using Xamarin.Forms;
using System.Linq;
using AgentVI.Services;

namespace AgentVI.Views
{
    public partial class CamerasPage : ContentPage
    {
        private CamerasListViewModel allCameras = null;

        public CamerasPage()
        {
            InitializeComponent();

            User user = ServiceManager.Instance.LoginService.LoggedInUser;
            allCameras = new CamerasListViewModel();
            allCameras.InitializeList(user);
            cameraListView.ItemsSource = allCameras.CamerasList;
        }

        void OnRefresh(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            //put the refreshing logic here
            var itemList = allCameras.CamerasList.Reverse().ToList();
            allCameras.CamerasList.Clear();
            foreach (var s in itemList)
            {
                allCameras.CamerasList.Add(s);
            }
            //make sure to end the refresh state
            list.IsRefreshing = false;
        }

        void OnTap(object sender, ItemTappedEventArgs e)
        {
            DisplayAlert("Camera Tapped", e.Item.ToString(), "Ok");
        }
    }
}