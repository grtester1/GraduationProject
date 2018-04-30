using System;
using System.Collections.Generic;
using AgentVI.Services;
using Xamarin.Forms;
using InnoviApiProxy;
using System.Collections.ObjectModel;

namespace AgentVI.ViewModels
{
    public class CamerasListViewModel
    {
        public ObservableCollection<CameraViewModel> CamerasList { get; set; }

        public CamerasListViewModel()
        {
            CamerasList = new ObservableCollection<CameraViewModel>();
        }

        public void InitializeList(User i_loggedInUser)
        {
            if (i_loggedInUser != null)
            {
                List<Sensor> userProxyList= i_loggedInUser.GetDefaultAccountSensors();

                foreach(Sensor camera in userProxyList)
                {
                    CameraViewModel camViewModel = new CameraViewModel();
                    camViewModel.CamName = camera.Name;
                    camViewModel.CamStatus = camera.Status.ToString();
                    camViewModel.CamImage = camera.StreamUrl;
                    if(camViewModel.CamImage == null)
                    {
                        camViewModel.CamImage = "https://picsum.photos/201";
                    }
                    CamerasList.Add(camViewModel);
                }
            }
            else
            {
                throw new Exception("Method InitializeFields/CamerasListVM was called with null param");
            }
        }
    }
}

