#if DPROXY
using DummyProxy;
using Sensor = DummyProxy.Sensor;
using eSensorStatus = DummyProxy.Sensor.eSensorStatus;
using Health = DummyProxy.Sensor.Health;
#else
using InnoviApiProxy;
using Sensor = InnoviApiProxy.Sensor;
using eSensorStatus = InnoviApiProxy.Sensor.eSensorStatus;
using Health = InnoviApiProxy.Sensor.Health;
#endif
using AgentVI.Converters;
using System.Collections.Generic;
using System;

namespace AgentVI.Models
{
    public class SensorModel
    {
        public Sensor Sensor { get; private set; }
        public string SensorName { get; private set; }
        public eSensorStatus SensorHealth { get; private set; }
        private Lazy<List<Health>> SensorHealthHistoryLazyHelper { get; set; }
        public List<Health> SensorHealthHistory => SensorHealthHistoryLazyHelper.Value;
        public ESensorColorHealth SensorHealthStatus { get; private set; }
        public string SensorImage { get; private set; }

        private SensorModel()
        {

        }

        internal static SensorModel FactoryMethod(Sensor i_Sensor)
        {
            SensorModel res = new SensorModel()
            {
                Sensor = i_Sensor,
                SensorName = i_Sensor.Name,
                SensorHealth = i_Sensor.Status,
                SensorHealthHistoryLazyHelper = new Lazy<List<Health>>(() => i_Sensor.SensorHealthArray),
                SensorImage = i_Sensor.ReferenceImage
            };

            return res.UpdateSensorsHealthStatus();
        }

        private SensorModel UpdateSensorsHealthStatus()
        {
            SensorHealthStatus = GetEnumForValue(SensorHealth);
            return this;
        }

        public enum ESensorColorHealth
        {
            Black,
            Grey,
            Red,
            Yellow,
            Green
        };

        private ESensorColorHealth GetEnumForValue(eSensorStatus i_value)
        {
            ESensorColorHealth res;

            switch(i_value)
            {
                case eSensorStatus.Active:
                    res = ESensorColorHealth.Green;
                    break;
                case eSensorStatus.Warning:
                    res = ESensorColorHealth.Yellow;
                    break;
                case eSensorStatus.Error:
                    res = ESensorColorHealth.Red;
                    break;
                case eSensorStatus.Inactive:
                    res = ESensorColorHealth.Black;
                    break;
                case eSensorStatus.Undefined:
                    res = ESensorColorHealth.Grey;
                    break;
                default:
                    res = ESensorColorHealth.Grey;
                    break;
            }

            return res;
        }
    }
}