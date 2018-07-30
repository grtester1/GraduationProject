using System;
using Xamarin.Forms;
using InnoviApiProxy;

namespace AgentVI.Models
{
    public class CameraModel
    {
        public Sensor Sensor { get; set; }
        public string CamName { get; set; }
        public string CamStatus { get; set; }
		public int CamHealth { get; set; }
		public string CamColorHealth { get; set; }
        public string CamColorStatus { get; set; }
        public string CamImage { get; set; }
    }
}