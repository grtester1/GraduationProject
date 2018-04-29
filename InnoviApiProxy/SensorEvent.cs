using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace InnoviApiProxy
{
    public class SensorEvent : InnoviObject
    {
        [JsonProperty("id")]
        private string eventId { get; set; }
        [JsonProperty]
        private int sensorId { get; set; }
        [JsonProperty]
        private int accountId { get; set; }
        [JsonProperty]
        public ulong StartTime { get; private set; }
        [JsonProperty]
        public string ObjectType { get; private set; }
        [JsonProperty]
        public int RuleId { get; private set; }
        [JsonProperty]
        public string Name { get; private set; }
        [JsonProperty]
        public string ImagePath { get; private set; }
        [JsonProperty]
        public string ClipPath { get; private set; }
        [JsonProperty]
        public Sensor.eSensorEventTag Tag { get; private set; }

        internal SensorEvent() { }
    }
}
