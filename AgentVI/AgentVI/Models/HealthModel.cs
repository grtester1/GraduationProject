using System;
using System.ComponentModel;
using InnoviApiProxy;
using Xamarin.Forms;

namespace AgentVI.Models
{
    public class HealthModel
    {
        private HealthModel()
        {

        }

        internal static HealthModel FactoryMethod(Sensor.Health i_Health)
        {
            HealthModel res = new HealthModel()
            {
                HealthTime = (ulong)i_Health.StatusTimeStamp,
                HealthDescription = i_Health.DetailedDescription,
                HealthDuration = "00:00" // nadav needs to implement duration calculation function 
            };

            return res;
        }

        public ulong HealthTime { get; set; }
        public string HealthDescription { get; set; }
        public string HealthDuration { get; set; }


        //The old HealthModel.cs:
        //---------------------------
        /*
        public long HealthTime { get; set; }
        public string HealthDescription { get; set; }
        public string HealthDuration { get; set; }
        */
    }
}