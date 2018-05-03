using System;
using System.Collections.Generic;
using AgentVI.Services;
using Xamarin.Forms;
using InnoviApiProxy;
using System.Collections.ObjectModel;
using AgentVI.Models;

namespace AgentVI.ViewModels
{
    public class CamerasListViewModel
    {
        public ObservableCollection<CameraModel> CamerasList { get; set; }

        public CamerasListViewModel()
        {
            CamerasList = new ObservableCollection<CameraModel>();
        }

        public void InitializeList(User i_loggedInUser)
        {
            if (i_loggedInUser != null)
            {
                List<Sensor> userProxyList = i_loggedInUser.GetDefaultAccountSensors();
                if (userProxyList.Count > 0)
                {
                    foreach (Sensor camera in userProxyList)
                    {
                        CameraModel camModel = new CameraModel();
                        camModel.CamName = camera.Name;
                        camModel.CamStatus = camera.Status.ToString();

                        switch (camera.Status)
                        {
                            case Sensor.eSensorStatus.Undefined:
                                camModel.CamColorStatus = "White";
                                break;
                            case Sensor.eSensorStatus.Active:
                                camModel.CamColorStatus = "Green";
                                break;
                            case Sensor.eSensorStatus.Warning:
                                camModel.CamColorStatus = "Yellow";
                                break;
                            case Sensor.eSensorStatus.Error:
                                camModel.CamColorStatus = "Red";
                                break;
                            case Sensor.eSensorStatus.Inactive:
                                camModel.CamColorStatus = "Gray";
                                break;
                            default:
                                camModel.CamColorStatus = "Transparent";
                                break;
                        }

                        camModel.CamImage = camera.StreamUrl;
                        if (camModel.CamImage == null)
                        {
                            camModel.CamImage = "http://3.bp.blogspot.com/-FYjlw5lYV_Q/VCaXoNp-PTI/AAAAAAAAHmk/cLuCv4Ruq_U/s1600/37.jpg";
                        }
                        CamerasList.Add(camModel);
                    }
                }
                else
                {
                    CamerasList.Add(new CameraModel { CamName = "There is currently no camera in the selected folder.", CamStatus = "", CamColorStatus = "Transparent", CamImage = "https://picsum.photos/201" });
                }
            }
            else
            {
                throw new Exception("Method InitializeFields/CamerasListVM was called with null param");
            }
        }
    }
}