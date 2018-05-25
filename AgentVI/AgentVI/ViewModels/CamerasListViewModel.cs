using System;
using System.Collections.Generic;
using AgentVI.Services;
using Xamarin.Forms;
using InnoviApiProxy;
//using DummyProxy;
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
                //List<Sensor> userProxyList = i_loggedInUser.GetDefaultAccountSensors().ToList();
				InnoviObjectCollection<Sensor> userProxyList = i_loggedInUser.GetDefaultAccountSensors();
				if (userProxyList != null)
                {
                    foreach (Sensor camera in userProxyList)
                    {
                        CameraModel camModel = new CameraModel();
                        camModel.CamName = camera.Name;
                        camModel.CamStatus = camera.Status.ToString();
						camModel.CamHealth = camera.Health;
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

						if(camera.Health > 80)
						{
							
						}
						else if (camera.Health > 60)
                        {

                        }
						else if (camera.Health > 40)
                        {

                        }
						else if (camera.Health > 20)
                        {

                        }
						else
						{
							
						}
                                          
						camModel.CamImage = camera.StreamUrl;
						if (camModel.CamImage == null || camModel.CamImage == "")
                        {
							camModel.CamImage = "https://i.ytimg.com/vi/CKgEmWL1YrQ/maxresdefault.jpg";
                        }
                        CamerasList.Add(camModel);
                    }
                }
                else
                {
					CamerasList.Add(new CameraModel { CamName = "There is currently no camera in the selected folder.", CamStatus = "", CamColorStatus = "Transparent", CamImage = "https://nondualityamerica.files.wordpress.com/2010/10/nothing-here-neon-300x200.jpg?w=375&h=175" });
                }
            }
            else
            {
                throw new Exception("Method InitializeFields/CamerasListVM was called with null param");
            }
        }
    }
}