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
        public ObservableCollection<CameraViewModel> allCameras { get; set; }

        public CamerasPage()
        {
            InitializeComponent();

            allCameras = new ObservableCollection<CameraViewModel>();

            //copy all the cameras to allCameras list with nadav's fuction:
            //allCameras = getAllCamerasToList();
            //meantime:
            allCameras.Add(new CameraViewModel { CamName = "Tomato", CamStatus = "Fruit", CamImage = "tomato.png" });
            allCameras.Add(new CameraViewModel { CamName = "Romaine Lettuce", CamStatus = "Vegetable", CamImage = "lettuce.png" });
            allCameras.Add(new CameraViewModel { CamName = "Zucchini", CamStatus = "Vegetable", CamImage = "zucchini.png" });
            allCameras.Add(new CameraViewModel { CamName = "Zucchini", CamStatus = "Vegetable", CamImage = "zucchini.png" });
            allCameras.Add(new CameraViewModel { CamName = "Zucchini", CamStatus = "Vegetable", CamImage = "zucchini.png" });
            allCameras.Add(new CameraViewModel { CamName = "Zucchini", CamStatus = "Vegetable", CamImage = "zucchini.png" });

            if(allCameras == null)
            {
                allCameras.Add(new CameraViewModel { CamName = "There is currently no camera in the selected folder.", CamStatus = "", CamImage = "noPhoto.png" });
            }

            cameraListView.ItemsSource = allCameras;
        }

        void OnRefresh(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            //put the refreshing logic here
            var itemList = allCameras.Reverse().ToList();
            allCameras.Clear();
            foreach (var s in itemList)
            {
                allCameras.Add(s);
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