
using DummyProxy;

//using InnoviApiProxy;

using System;
using System.ComponentModel;
using Xamarin.Forms;


namespace AgentVI.Models
{
	public class CameraModel : INotifyPropertyChanged
	{
		//public string CamStatus { get; set; }
        //public string CamColorStatus { get; set; }

		private Sensor m_sensor;
		public Sensor Sensor
		{
			get { return m_sensor; }
			set
			{
				m_sensor = value;
				OnPropertyChanged("Sensor");
			}
		}

		private string m_camName;
		public string CamName
        {
			get { return m_camName; }
            set
            {
				m_camName = value;
				OnPropertyChanged("CamName");
            }
        }
        
		private int m_camHealth;
		public int CamHealth
        {
			get { return m_camHealth; }
            set
            {
				m_camHealth = value;
				OnPropertyChanged("CamHealth");
            }
        }

		private string m_camColorHealth;
		public string CamColorHealth
        {
			get { return m_camColorHealth; }
            set
            {
				m_camColorHealth = value;
				OnPropertyChanged("CamColorHealth");
            }
        }

		private string m_camImage;
		public string CamImage
        {
			get { return m_camImage; }
            set
            {
				m_camImage = value;
				OnPropertyChanged("CamImage");
            }
        }
              
		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName)
		{
			if(PropertyChanged!=null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}