
using DummyProxy;

//using InnoviApiProxy;

using System;
using System.Collections.Generic;
using AgentVI.Services;
using Xamarin.Forms;
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

		public void UpdateCameras()
		{
			List<Sensor> filteredCamList = ServiceManager.Instance.FilterService.GetFilteredSensorCollection();
			if (filteredCamList != null)
			{
				filteredCamList.Reverse(); //for debug
				CamerasList.Clear();
				foreach (Sensor camera in filteredCamList)
				{
					CameraModel camModel = new CameraModel();
					camModel.Sensor = camera;
					camModel.CamName = camera.Name;

					if (camModel.CamHealth > 80)
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

					//camModel.CamImage = camera.ReferenceImage;
					if (String.IsNullOrWhiteSpace(camModel.CamImage))
					{
						camModel.CamImage = "https://i.ytimg.com/vi/CKgEmWL1YrQ/maxresdefault.jpg";
					}
					CamerasList.Add(camModel);
				}
			}
			else
			{
				CamerasList.Add(new CameraModel { CamName = "There is currently no camera in the selected folder.", CamHealth = 0, CamColorHealth = "Transparent", CamImage = "https://nondualityamerica.files.wordpress.com/2010/10/nothing-here-neon-300x200.jpg?w=375&h=175" });
			}
		}
	      
        public void InitializeList(User i_loggedInUser)
        {
            if (i_loggedInUser != null)
            {
				UpdateCameras();
            }
            else
            {
				throw new Exception("Method InitializeList was called with null param");
            }
        }
    }
}