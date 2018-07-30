using System;
using System.Collections.Generic;
using AgentVI.Services;
using Xamarin.Forms;
using InnoviApiProxy;
//<debugWithDummyProxy>using DummyProxy;
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
                //InnoviObjectCollection<Sensor> userProxyList = i_loggedInUser.GetDefaultAccountSensors();
                List<Sensor> userProxyList = ServiceManager.Instance.FilterService.GetFilteredSensorCollection();
                if (userProxyList != null)
                {
                    foreach (Sensor camera in userProxyList)
                    {
                        CameraModel camModel = new CameraModel();
						camModel.Sensor = camera;
                        camModel.CamName = camera.Name;
                        //camModel.CamStatus = camera.Status.ToString();
						camModel.CamHealth = camera.Health;
                        camModel.Sensor = camera;
                        
                        /*switch (camera.Status)
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
                        }*/

						if(camModel.CamHealth > 80)
						{
							camModel.CamColorHealth = "Green";
						}
						else if (camModel.CamHealth > 60)
                        {
							camModel.CamColorHealth = "Yellow";
                        }
						else if (camModel.CamHealth > 40)
                        {
							camModel.CamColorHealth = "Red";
                        }
						else if (camModel.CamHealth > 20)
                        {
							camModel.CamColorHealth = "Gray";
                        } 
						else
						{
							camModel.CamColorHealth = "Black";
						}

                        camModel.CamImage = camera.ReferenceImage;
                        if (String.IsNullOrWhiteSpace(camModel.CamImage))           // Gil - please use C# Functionality. Changed it from "(camModel.CamImage==null || camModel.CamImage=="")"
                        {
                            camModel.CamImage = "https://i.ytimg.com/vi/CKgEmWL1YrQ/maxresdefault.jpg";
                        }
                        CamerasList.Add(camModel);
                    }
                }
                else
                {
					CamerasList.Add(new CameraModel { CamName = "There is currently no camera in the selected folder.", CamHealth = 0, CamColorHealth= "Transparent", CamImage = "https://nondualityamerica.files.wordpress.com/2010/10/nothing-here-neon-300x200.jpg?w=375&h=175" });
                }
            }
            else
            {
                throw new Exception("Method InitializeFields/CamerasListVM was called with null param");
            }
        }
    }
}