using System;

using Xamarin.Forms;


namespace AgentVI.Models
{
    public class SensorHealthModel
    {
        public ulong HealthTime { get; set; }
        public string HealthDescription { get; set; }
        public string HealthDuration { get; set; }
    }
}