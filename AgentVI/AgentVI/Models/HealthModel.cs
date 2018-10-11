#if DPROXY
using DummyProxy;
#else
using AgentVI.Converters;
using InnoviApiProxy;
#endif
using System;

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
                HealthTime = i_Health.StatusTimeStamp,
                HealthDescription = i_Health.DetailedDescription,
                HealthDuration = ""
            };

            return res;
        }

        public long HealthTime { get; set; }
        public string HealthDescription { get; set; }
        public string HealthDuration { get; set; }
    }
}