using System;
using Xamarin.Forms;

namespace AgentVI.Models
{
    public class CameraModel
    {
        public string CamName { get; set; }
        public string CamStatus { get; set; }
		public int CamHealth { get; set; }
        public string CamColorStatus { get; set; }
        public string CamImage { get; set; }
    }
}