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
                if (userProxyList.Count > 0)
                {
                    foreach (Sensor camera in userProxyList)
                    {
                        CameraViewModel camViewModel = new CameraViewModel();
                        camViewModel.CamName = camera.Name;
                        camViewModel.CamStatus = camera.Status.ToString();

                        switch (camera.Status)
                        {
                            case Sensor.eSensorStatus.Undefined:
                                camViewModel.CamColorStatus = "White";
                                break;
                            case Sensor.eSensorStatus.Active:
                                camViewModel.CamColorStatus = "Green";
                                break;
                            case Sensor.eSensorStatus.Warning:
                                camViewModel.CamColorStatus = "Yellow";
                                break;
                            case Sensor.eSensorStatus.Error:
                                camViewModel.CamColorStatus = "Red";
                                break;
                            case Sensor.eSensorStatus.Inactive:
                                camViewModel.CamColorStatus = "Silver";
                                break;
                            default:
                                camViewModel.CamColorStatus = "Transparent";
                                break;
                        }

                        camViewModel.CamImage = camera.StreamUrl;
                        if (camViewModel.CamImage == null)
                        {
                            camViewModel.CamImage = "https://picsum.photos/201";
                        }
                        CamerasList.Add(camViewModel);
                    }
                }
                else
                {
                    CamerasList.Add(new CameraViewModel { CamName = "There is currently no camera in the selected folder.", CamStatus = "", CamColorStatus = "Transparent", CamImage = "https://picsum.photos/201" });
                }
            }
            else
            {
                throw new Exception("Method InitializeFields/CamerasListVM was called with null param");
            }
        }
    }
}