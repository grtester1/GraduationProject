using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using InnoviApiProxy;
using AgentVI.ViewModels;
using Xamarin.Forms;
using System.Linq;

namespace AgentVI.Views
{
    public partial class CamerasPage : ContentPage
    {
        private CamerasListViewModel allCameras = null;

        public CamerasPage()
        {
            InitializeComponent();

            User user = Services.LoginService.Instance.LoggedInUser;
            allCameras = new CamerasListViewModel();
            allCameras.InitializeList(user);

            /*
            //copy all the cameras to allCameras list with nadav's fuction:
            //allCameras = getAllCamerasToList();
            //meantime:
            allCameras = new CamerasListViewModel();
            allCameras.CamerasList.Add(new CameraViewModel { CamName = "Tomato", CamStatus = "Fruit", CamImage = "https://picsum.photos/201" });
            allCameras.CamerasList.Add(new CameraViewModel { CamName = "Romaine Lettuce", CamStatus = "Vegetable", CamImage = "https://picsum.photos/201" });
            allCameras.CamerasList.Add(new CameraViewModel { CamName = "Zucchini", CamStatus = "Vegetable", CamImage = "https://picsum.photos/201" });
            allCameras.CamerasList.Add(new CameraViewModel { CamName = "Zucchini", CamStatus = "Vegetable", CamImage = "https://picsum.photos/201" });
            allCameras.CamerasList.Add(new CameraViewModel { CamName = "Zucchini", CamStatus = "Vegetable", CamImage = "https://picsum.photos/201" });
            allCameras.CamerasList.Add(new CameraViewModel { CamName = "Zucchini", CamStatus = "Vegetable", CamImage = "https://picsum.photos/201" });
            */
            if (allCameras == null)
            {
                allCameras.CamerasList.Add(new CameraViewModel { CamName = "There is currently no camera in the selected folder.", CamStatus = "", CamImage = "https://picsum.photos/201" });
            }

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