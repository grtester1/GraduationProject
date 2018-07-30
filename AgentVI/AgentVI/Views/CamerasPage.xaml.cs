using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
//<debugWithMainProxy>using InnoviApiProxy;
using DummyProxy;
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
            var bananaSHIT = ServiceManager.Instance.FilterService.GetFilteredSensorCollection();           //Gil - see this example of using FilterService.GetFilteredSensorCollection()
            StringBuilder helloWorld;
            String helloWorld2;
            foreach (Sensor s in bananaSHIT)
            {
                foreach (SensorEvent se in s.SensorEvents)
                {
                    helloWorld = new StringBuilder();
                    helloWorld.Append(se.ClipPath).Append(" ").Append(se.ImagePath).Append(" ").Append(se.ObjectType).Append(" ").Append(se.RuleId).Append(" ").Append(se.RuleName).Append(" ").Append(se.SensorName);
                    helloWorld2 = helloWorld.ToString();
                }
            }

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

        private void onCameraNameTapped(object sender, EventArgs e)
        {            
            (App.Current.MainPage as NavigationPage).PushAsync(
                                                new CameraEventsPage(
                                                    allCameras.CamerasList.Where(cam => cam.CamName == (sender as Label).Text).First().Sensor
                                                                    )
                                                                );
        }
    }
}