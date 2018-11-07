using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Timers;

namespace InnoviApiProxy
{
    public class SensorEvent : InnoviObject
    {
        private bool m_IsClipAvailable = false;

        private bool m_IsClipAvailableFetchNeeded = true;
        private static int m_TimerInterval = 5000;
        Timer m_ClipAvailabilityCheckTimer = new Timer(m_TimerInterval);
        [JsonProperty("id")]
        private string eventId { get; set; }
        [JsonProperty]
        internal int SensorId { get; private set; }
        [JsonProperty]
        private int accountId { get; set; }
        public string SensorName { get; internal set; }
        [JsonProperty]
        public ulong StartTime { get; private set; }
        [JsonProperty("objectType")]
        public eObjectType ObjectType { get; private set; }
        [JsonProperty]
        public int RuleId { get; private set; }

        [JsonProperty]
        public Sensor.eSensorEventTag Tag { get; private set; }
        [JsonProperty("behaviorType")]
        public eBehaviorType RuleName { get; private set; }
        public bool IsClipAvailable
        {
            get
            {
                if (m_IsClipAvailableFetchNeeded)
                {
                    string path = ClipPath;
                    m_IsClipAvailable = HttpUtils.IsUrlFound(path);
                    m_ClipAvailabilityCheckTimer.Elapsed += onTimerElapsed;
                }
                
                return m_IsClipAvailable;
            }
        }

        public bool IsImageAvailable
        {
            get
            {
                string path = ImagePath;
                return HttpUtils.IsUrlFound(path);
            }
        }

        protected override int Id => eventId.GetHashCode();

        public Sensor EventSensor
        {
            get
            {
                return HttpUtils.GetSensorByID(SensorId);
            }
        }

        public string ImagePath
        {
            get
            {
                return Settings.InnoviApiEndpoint + "eventImage?accountId=" + accountId.ToString() + "&eventId=" + eventId.ToString();
            }
        
        }


        public string ClipPath
        {
            get
            {
                return Settings.InnoviApiEndpoint + "eventClip?accountId=" + accountId.ToString() + "&eventId=" + eventId.ToString();
            }
        }

        private void onTimerElapsed(object sender, ElapsedEventArgs e)
        {
            m_IsClipAvailableFetchNeeded = true;
            m_ClipAvailabilityCheckTimer.Elapsed -= onTimerElapsed;
        }
        internal SensorEvent()
        {
            
        }

        public enum eBehaviorType
        {
            Undefined,
            Moving,
            Crossing,
            Occupancy,
            Stopped,
            Mask,
            Vqm,
            Mrd,
            Grouping,
            Anomaly
        }

        public enum eObjectType
        {
            Undefined,
            Unknown,
            Person,
            Vehicle,
            Object,
            Bicycle,
            Motorcycle
        }
    }
}
